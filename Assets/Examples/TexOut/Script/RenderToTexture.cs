using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using NatCamU.Core;
using NatCamU.Dispatch;
using System.Text;

public class RenderToTexture : MonoBehaviour
{
    //Camera参数
    public Facing facing = Facing.Rear;
    public ResolutionPreset previewResolution = ResolutionPreset.HD;
    public ResolutionPreset photoResolution = ResolutionPreset.HighestResolution;
    public FrameratePreset framerate = FrameratePreset.Default;

    //Debugging
    public Switch verbose;

    //Camera_TO_SDK
    /*这个数组存着需要渲染的道具的ID，ID是加载（fu_CreateItemFromPackage）完道具后调用fu_getItemIdxFromPackage返回的值（大于0的整数），这个数组的长度没有被限制，但是
     * 由于机器性能限制，一般不会超过10个。这个数组的值可以轮空，如[1,4,0,3,0,2],这里0即没有道具，SDK内部会自动理解成按照[1,4,3,2]的顺序渲染道具，因此为了业务逻辑上的方便
     * 可以在此主动声明第N个位置用于某一类道具，如第0位只用于美颜，第1位只用于美妆，第2位只用于滤镜等等。这些位置被称为slot。
     * */
    int[] itemid_tosdk;

    public const int SLOTLENGTH = 10;      //最大slot长度
    private struct slot_item
    {
        public string name;
        public int id;
        public Item item;

        public void Reset()
        {
            id = 0;
            name = "";
            item = Item.Empty;
        }
    };
    private slot_item[] slot_items; //关联起item和slotid的一个数组，长度为SLOTLENGTH

#if UNITY_EDITOR||UNITY_STANDALONE
    //以下参数仅在PC或MAC上生效，因为这两个平台上NatCam实际上调用的是Unity自带的WebCam,无法在底层直接向SDK输入数据，因此需要在这里输入数据

    //byte[] img_bytes;
    Color32[] webtexdata;   //用于保存每帧从相机类获取的数据
    GCHandle img_handle;    //webtexdata的GCHandle
    IntPtr p_img_ptr;    //webtexdata的指针

    //SDK返回(OUTPUT)
    private int m_fu_texid = 0;      //SDK返回的纹理ID
    private Texture2D m_rendered_tex;   //用SDK返回的纹理ID新建的纹理

    //标记参数
    private bool m_tex_created; //m_rendered_tex是否已被创建，这个不需要每帧创建，纹理ID不变就不要重新创建
#endif
    private bool LoadingItem = false;   //是否正在加载道具，道具的加载是一个协程，不是瞬间完成的，因此为了防止调用混乱，用这个变量主动控制

    //渲染显示UI
    public RawImage RawImg_BackGroud;   //用来显示相机结果的UI控件

    [HideInInspector]
    public bool isCameraChanging = false;


    public Shader bgShader; //可以用来控制纹理的旋转和镜像
    private Material bgMaterial;//bgShader对应的材质
    public Material bgMat
    {
        get
        {
            if (bgMaterial == null)
            {
                bgMaterial = new Material(bgShader);
                bgMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return bgMaterial;
        }
    }

    public Text txt;

    /**\brief 调整输入的纹理的旋转和镜像，用以下三个参数可以表达一个纹理任意方向的旋转和镜像的结果（2^3=8种）\param tex_origin 原始的纹理\param SwichXY  是否调换纹理的UV的X轴和Y轴，即沿着对角线翻转\param flipx    是否翻转X轴\param flipy    是否翻转Y轴\return 计算好的纹理    */
    public Texture2D AdjustTex(Texture2D tex_origin, int SwichXY, int flipx, int flipy)
    {
        int w = 0;
        int h = 0;

        w = tex_origin.width;
        h = tex_origin.height;

        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture bufferdst = RenderTexture.GetTemporary(w, h, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        RenderTexture.active = bufferdst;

        bgMat.SetFloat("_FlipX", flipx);
        bgMat.SetFloat("_FlipY", flipy);
        bgMat.SetFloat("_SwichXY", SwichXY);
        bgMat.SetFloat("_sRGBToLinear", 0);
        bgMat.SetFloat("_LinearTosRGB", 0);

        Graphics.Blit(tex_origin, bufferdst, bgMat);

        Texture2D tex_new = new Texture2D(w, h, TextureFormat.ARGB32, false);
        tex_new.ReadPixels(new Rect(0, 0, w, h), 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        tex_new.Apply();

        RenderTexture.ReleaseTemporary(bufferdst);
        RenderTexture.active = currentActiveRT;

        //Debug.Log("AdjustTex");

        return tex_new;
    }

    //初始化相机后，执行这个回调，用于调整相机旋转镜像缩放
    public void OnStart()
    {
#if !(UNITY_EDITOR || UNITY_STANDALONE)
        // Set the preview RawImage texture once the preview starts
        if (RawImg_BackGroud != null)
        {
            RawImg_BackGroud.texture = NatCam.Preview;
            if (FaceunityWorker.NeedSwitchWidthHeight())
            {
                var tex = ((Texture2D)RawImg_BackGroud.texture);
                tex.Resize(tex.height, tex.width);
            }
            RawImg_BackGroud.gameObject.SetActive(true);
        }
        else Debug.Log("Preview RawImage has not been set");
        Debug.Log("Preview started with dimensions: " + NatCam.Camera.PreviewResolution.x+","+ NatCam.Camera.PreviewResolution.y);
#else
        m_tex_created = false;
#endif
        SelfAdjusUISize();
        //SetItemMirror();

        FaceunityWorker.SetCameraChange(true);
    }

    //切换相机，newCamera为相机ID
    public void SwitchCamera(int newCamera = -1)
    {
        // Select the new camera ID // If no argument is given, switch to the next camera
        newCamera = newCamera < 0 ? (NatCam.Camera + 1) % DeviceCamera.Cameras.Count : newCamera;
        // Set the new active camera
        NatCam.Camera = newCamera;

        SelfAdjusTexOutPut();

        StartCoroutine(delaySetItemMirror());
    }

    //延迟设置道具的镜像参数，因为相机切换不是瞬间完成的
    IEnumerator delaySetItemMirror()
    {
        yield return Util._endOfFrame;
        for (int i = 0; i < SLOTLENGTH; i++)
        {
            SetItemMirror(i);
        }
    }

    //根据运行环境调整Nama输出纹理的旋转镜像
    public void SelfAdjusTexOutPut()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        FaceunityWorker.SetTransformMatrix(FaceunityWorker.TRANSFORM_MATRIX.CCROT180);
#elif UNITY_ANDROID
        if (NatCam.Camera.Facing == Facing.Front)
        {
            FaceunityWorker.SetTransformMatrix(FaceunityWorker.TRANSFORM_MATRIX.CCROT90_FLIPHORIZONTAL);
        }
        else
        {
            FaceunityWorker.SetTransformMatrix(FaceunityWorker.TRANSFORM_MATRIX.CCROT270);
        }
#elif UNITY_IOS
        FaceunityWorker.SetTransformMatrix(FaceunityWorker.TRANSFORM_MATRIX.CCROT180_FLIPHORIZONTAL);
#endif
    }

    //根据运行环境调整相机UI的旋转镜像缩放
    public void SelfAdjusUISize()
    {
        Vector2 targetResolution = RawImg_BackGroud.canvas.GetComponent<CanvasScaler>().referenceResolution;
        Vector2 resolution = NatCam.Camera.PreviewResolution;
#if UNITY_IOS && !UNITY_EDITOR
        //ios相机的宽高是反的，为啥？？？
        resolution = new Vector2(resolution.y, resolution.x);
#else
        if (FaceunityWorker.NeedSwitchWidthHeight())
            resolution = new Vector2(resolution.y, resolution.x);
#endif
        RawImg_BackGroud.rectTransform.sizeDelta = new Vector2(targetResolution.y * resolution.x / resolution.y, targetResolution.y);
    }

    // 初始化相机
    public IEnumerator InitCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            // Set verbose mode
            NatCam.Verbose = verbose;
            // Set the active camera
            NatCam.Camera = facing == Facing.Front ? DeviceCamera.FrontCamera : DeviceCamera.RearCamera;
            if (!NatCam.Camera)
            {
                NatCam.Camera = DeviceCamera.RearCamera;
            }
            //Null checking
            if (!NatCam.Camera)
            {
                Debug.LogError("No camera detected!");
                StopCoroutine("InitCamera");
                yield return null;
            }
            // Set the camera's preview resolution
            NatCam.Camera.SetPreviewResolution(previewResolution);
            // Set the camera's photo resolution
            NatCam.Camera.SetPhotoResolution(photoResolution);
            // Set the camera's framerate
            NatCam.Camera.SetFramerate(framerate);
            // Play
            NatCam.Play();
            // Register callback for when the preview starts //Note that this is a MUST when assigning the preview texture to anything
            NatCam.OnStart += OnStart;

            SelfAdjusTexOutPut();

#if UNITY_EDITOR || UNITY_STANDALONE
            if (img_handle.IsAllocated)
                img_handle.Free();
            webtexdata = new Color32[(int)NatCam.Camera.PreviewResolution.x * (int)NatCam.Camera.PreviewResolution.y];
            img_handle = GCHandle.Alloc(webtexdata, GCHandleType.Pinned);
            p_img_ptr = img_handle.AddrOfPinnedObject();
#endif
        }
    }

    /**\brief 截图\param cameras 要参与截图的unity相机\param rect  要截图的范围，一般是全屏\return 计算好的纹理    */
    public Texture2D CaptureCamera(Camera[] cameras, Rect rect)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        foreach (Camera cam in cameras)
        {
            cam.targetTexture = rt;
            cam.Render();
        }
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        foreach (Camera cam in cameras)
        {
            cam.targetTexture = null;
        }
        RenderTexture.active = null; // JC: added to avoid errors  
        Destroy(rt);
        Debug.Log("截屏了一张照片");

        return screenShot;
    }

    //仅仅保存图片成png到固定路径，并不通知图库刷新，因此请用文件浏览器在对应路径打开图片
    public void SaveTex2D(Texture2D tex)
    {
        byte[] bytes = tex.EncodeToPNG();
        if (Directory.Exists(Application.persistentDataPath + "/Photoes/") == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Photoes/");
        }
        string name = Application.persistentDataPath + "/Photoes/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";
        File.WriteAllBytes(name, bytes);
        Debug.Log("保存了一张照片:" + name);
    }

    void Awake()
    {
        FaceunityWorker.OnInitOK += InitApplication;
    }

    //初始化slot信息
    void Start()
    {
        if (itemid_tosdk == null)
        {
            //默认slot槽长度为SLOTLENGTH=10
            itemid_tosdk = new int[SLOTLENGTH];

            slot_items = new slot_item[SLOTLENGTH];
            for (int i = 0; i < SLOTLENGTH; i++)
            {
                slot_items[i].Reset();
            }
        }
    }

    //SDK初始化完成后会执行这个回调，记录相机UI原始旋转信息，开启相机初始化协程
    void InitApplication(object source, EventArgs e)
    {
        FaceunityWorker.SetRunningMode(FaceunityWorker.FU_RUNNING_MODE.FU_RUNNING_MODE_RENDERITEMS);
        StartCoroutine("InitCamera");
    }

    //当前环境是PC或者MAC的时候，在这里向SDK输入数据并获取SDK输出的纹理
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (NatCam.Camera == null)
            return;

        WebCamTexture tex = (WebCamTexture)NatCam.Preview;

        if (tex != null && tex.isPlaying)
        {

            // pass data by byte buf, 

            if (tex.didUpdateThisFrame)
            {
                if (webtexdata.Length != tex.width * tex.height)
                {
                    if (img_handle.IsAllocated)
                        img_handle.Free();
                    webtexdata = new Color32[tex.width * tex.height];
                    img_handle = GCHandle.Alloc(webtexdata, GCHandleType.Pinned);
                    p_img_ptr = img_handle.AddrOfPinnedObject();
                }
                tex.GetPixels32(webtexdata);
                //Debug.LogFormat("data pixels:{0},img_btyes:{1}",data.Length,img_bytes.Length/4);
                //for (int i = 0; i < webtexdata.Length; i++)
                //{
                //    img_bytes[4 * i] = webtexdata[i].b;
                //    img_bytes[4 * i + 1] = webtexdata[i].g;
                //    img_bytes[4 * i + 2] = webtexdata[i].r;
                //    img_bytes[4 * i + 3] = 1;
                //}
                FaceunityWorker.SetImage(p_img_ptr, 0, false, (int)NatCam.Camera.PreviewResolution.x, (int)NatCam.Camera.PreviewResolution.y);   //传输数据方法之一
                //FaceunityWorker.SetImageTexId((int)tex.GetNativeTexturePtr(), 0, (int)NatCam.Camera.PreviewResolution.x, (int)NatCam.Camera.PreviewResolution.y);
            }
        }


        if (m_tex_created == false)
        {
            m_fu_texid = FaceunityWorker.fu_GetNamaTextureId();
            if (m_fu_texid > 0)
            {
                m_tex_created = true;
                if (FaceunityWorker.NeedSwitchWidthHeight())
                    m_rendered_tex = Texture2D.CreateExternalTexture((int)NatCam.Camera.PreviewResolution.y, (int)NatCam.Camera.PreviewResolution.x, TextureFormat.RGBA32, false, true, (IntPtr)m_fu_texid);
                else
                    m_rendered_tex = Texture2D.CreateExternalTexture((int)NatCam.Camera.PreviewResolution.x, (int)NatCam.Camera.PreviewResolution.y, TextureFormat.RGBA32, false, true, (IntPtr)m_fu_texid);
                Debug.LogFormat("Texture2D.CreateExternalTexture:{0}\n", m_fu_texid);
                if (RawImg_BackGroud != null)
                {
                    RawImg_BackGroud.texture = m_rendered_tex;
                    RawImg_BackGroud.gameObject.SetActive(true);
                    Debug.Log("m_rendered_tex: " + m_rendered_tex.GetNativeTexturePtr());
                }
            }
        }
#endif

        //txt.text = Input.acceleration.ToString();
        if (NatCam.Camera)
            FaceunityWorker.FixRotationWithAcceleration(Input.acceleration, NatCam.Camera.Facing != Facing.Front);
    }

    void OnApplicationPause(bool isPause)
    {

        if (isPause)
        {
            Debug.Log("Pause");
#if UNITY_EDITOR || UNITY_STANDALONE
            m_tex_created = false;
#endif
        }
        else
        {
            Debug.Log("Start");
        }
    }

    //加载道具完毕的委托
    public delegate void LoadItemCallback(Item item);
    /**\brief 封装过的加载道具用的接口，配合slot的概念和item这个struct控制多个不同类型的道具的加载卸载\param item 要加载的道具的item，封装过的道具信息集合，方便业务逻辑，详见itemconfig\param slotid  道具要加载的位置（slot），默认值为0，即slot数组的第0位\param cb  加载道具完毕后会自动执行的回调，可以为空\return 无*/
    public IEnumerator LoadItem(Item item, int slotid = 0, LoadItemCallback cb = null)
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0)
            yield break;
        if (LoadingItem == false && item.fullname != null && item.fullname.Length != 0 && slotid >= 0 && slotid < SLOTLENGTH)
        {
            LoadingItem = true;
            int tempslot = GetSlotIDbyName(item.name);
            if (tempslot < 0)   //如果尚未载入道具数据
            {
                string bundle = Util.GetStreamingAssetsPath() + "/faceunity/" + item.fullname + ".bytes";
                WWW bundledata = new WWW(bundle);
                yield return bundledata;
                byte[] bundle_bytes = bundledata.bytes;
                Debug.LogFormat("bundledata name:{0}, size:{1}", item.name, bundle_bytes.Length);

                var itemid = 0;
                //多线程载入，可以防止主线程被卡住，但是会引起一些UI逻辑相关的问题，暂时关闭
                //yield return Loom.RunAsync_Coroutine(() =>
                //{
                itemid = FaceunityWorker.fuCreateItemFromPackage(bundle_bytes, bundle_bytes.Length);
                var errorcode = FaceunityWorker.fuGetSystemError();
                if (errorcode != 0)
                    Debug.LogErrorFormat("errorcode:{0}, {1}", errorcode, Marshal.PtrToStringAnsi(FaceunityWorker.fuGetSystemErrorString(errorcode)));
                //});
                

                UnLoadItem(slotid); //卸载上一个在这个slot槽内的道具

                itemid_tosdk[slotid] = itemid;
                slot_items[slotid].id = itemid;
                slot_items[slotid].name = item.name;
                slot_items[slotid].item = item;

                FaceunityWorker.fu_SetItemIds(itemid_tosdk, itemid_tosdk.Length, null);
                Debug.Log("载入Item：" + item.name + " @slotid=" + slotid);
            }
            else if (tempslot != slotid)    //道具已载入，但是不在请求的slot槽内
            {
                UnLoadItem(slotid);

                itemid_tosdk[slotid] = slot_items[tempslot].id;
                slot_items[slotid] = slot_items[tempslot];

                itemid_tosdk[tempslot] = 0;
                slot_items[tempslot].Reset();

                FaceunityWorker.fu_SetItemIds(itemid_tosdk, itemid_tosdk.Length, null);
                Debug.Log("移动Item：" + item.name + " from tempslot=" + tempslot + " to slotid=" + slotid);
            }
            else    //tempslot == slotid 即重复载入同一个道具进同一个slot槽，直接跳过
            {
                Debug.Log("重复载入Item：" + item.name + "  slotid=" + slotid);
            }

            SetItemMirror(slotid);

            if (cb != null)
                cb(item);//触发载入道具完成事件

            LoadingItem = false;
        }
    }

    //输入道具名字返回道具ID
    public int GetItemIDbyName(string name)
    {
        for (int i = 0; i < SLOTLENGTH; i++)
        {
            if (string.Equals(slot_items[i].name, name))
                return slot_items[i].id;
        }
        return 0;
    }

    //输入slotid返回道具名字
    public string GetItemNamebySlotID(int slotid)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            return slot_items[slotid].name;
        }
        return "";
    }

    //输入道具名字返回道具在slot数组第几个（即slotid）
    public int GetSlotIDbyName(string name)
    {
        for (int i = 0; i < SLOTLENGTH; i++)
        {
            if (string.Equals(slot_items[i].name, name))
                return i;
        }
        return -1;
    }

    //输入道具名字卸载道具
    public bool UnLoadItem(string itemname)
    {
        return UnLoadItem(GetSlotIDbyName(itemname));
    }

    //输入slotid卸载在该位置的道具
    public bool UnLoadItem(int slotid)
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0)
            return false;
        if (slotid >= 0 && slotid < SLOTLENGTH && slot_items[slotid].id > 0)
        {
            Debug.Log("UnLoadItem name=" + slot_items[slotid].name + " slotid=" + slotid);
            FaceunityWorker.fuDestroyItem(slot_items[slotid].id);
            itemid_tosdk[slotid] = 0;
            slot_items[slotid].id = 0;
            slot_items[slotid].name = "";
            return true;
        }
        return false;
    }

    //卸载所有道具
    public void UnLoadAllItems()
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0)
            return;
        Debug.Log("UnLoadAllItems");
        GL.IssuePluginEvent(FaceunityWorker.fu_GetRenderEventFunc(), (int)FaceunityWorker.Nama_GL_Event_ID.FuDestroyAllItems);

        for (int i = 0; i < SLOTLENGTH; i++)
        {
            itemid_tosdk[i] = 0;
            slot_items[i].Reset();
        }
    }

    /**\brief 输入一张图片给道具当成纹理用\param itemname 道具的名字\param paramdname  关联图片的关键词\param value  图片的buffer的指针\param width  图片宽\param height  图片高\return 无*/
    public void CreateTexForItem(string itemname, string paramdname, IntPtr value, int width, int height)
    {
        CreateTexForItem(GetSlotIDbyName(itemname), paramdname, value, width, height);
    }

    /**\brief 输入一张图片给道具当成纹理用\param slotid 道具在slot数组中的index\param paramdname  关联图片的关键词\param value  图片的buffer的指针\param width  图片宽\param height  图片高\return 无*/
    public void CreateTexForItem(int slotid, string paramdname, IntPtr value, int width, int height)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            FaceunityWorker.fuCreateTexForItem(slot_items[slotid].id, paramdname, value, width, height);
        }
    }

    /**\brief 删除关联道具的纹理\param itemname 道具的名字\param paramdname  关联图片的关键词\return 无*/
    public void DeleteTexForItem(string itemname, string paramdname)
    {
        DeleteTexForItem(GetSlotIDbyName(itemname), paramdname);
    }

    /**\brief 删除关联道具的纹理\param slotid 道具在slot数组中的index\param paramdname  关联图片的关键词\return 无*/
    public void DeleteTexForItem(int slotid, string paramdname)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            FaceunityWorker.fuDeleteTexForItem(slot_items[slotid].id, paramdname);
        }
    }

    public void BindItem(int slotid_dst, int slotid_src)
    {
        if (slotid_dst >= 0 && slotid_dst < SLOTLENGTH && slotid_src >= 0 && slotid_src < SLOTLENGTH)
        {
            int[] value = { slot_items[slotid_src].id };
            FaceunityWorker.fuBindItems(slot_items[slotid_dst].id, value, value.Length);
            Debug.LogFormat("BindItem: slotid_dst={0}, slotid_src={1}", slotid_dst, slotid_src);
        }
    }

    public void UnBindItem(int slotid_dst, int slotid_src)
    {
        if (slotid_dst >= 0 && slotid_dst < SLOTLENGTH && slotid_src >= 0 && slotid_src < SLOTLENGTH)
        {
            int[] value = { slot_items[slotid_src].id };
            FaceunityWorker.fuUnbindItems(slot_items[slotid_dst].id, value, value.Length);
            Debug.LogFormat("UnBindItem: slotid_dst={0}, slotid_src={1}", slotid_dst, slotid_src);
        }
    }

    public void UnBindItemAll(int slotid_dst)
    {
        if (slotid_dst >= 0 && slotid_dst < SLOTLENGTH)
        {
            FaceunityWorker.fuUnbindAllItems(slot_items[slotid_dst].id);
            Debug.LogFormat("UnBindItemAll: slotid_dst = {0}", slotid_dst);
        }
    }

    /**\brief 给道具设置一个数组\param itemname 道具的名字\param paramdname  关联数组的关键词\param value  要设置的数组\return 无*/
    public void SetItemParamdv(string itemname, string paramdname, double[] value)
    {
        SetItemParamdv(GetSlotIDbyName(itemname), paramdname, value);
    }

    /**\brief 给道具设置一个数组\param slotid 道具在slot数组中的index\param paramdname  关联数组的关键词\param value  要设置的数组\return 无*/
    public void SetItemParamdv(int slotid, string paramdname, double[] value)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH && value != null && value.Length > 0)
        {
            FaceunityWorker.fuItemSetParamdv(slot_items[slotid].id, paramdname, value, value.Length);
        }
    }

    /**\brief 给道具设置一个double参数\param itemname 道具的名字\param paramdname  关联参数的关键词\param value  要设置的参数\return 无*/
    public void SetItemParamd(string itemname, string paramdname, double value)
    {
        SetItemParamd(GetSlotIDbyName(itemname), paramdname, value);
    }

    /**\brief 给道具设置一个double参数\param slotid 道具在slot数组中的index\param paramdname  关联参数的关键词\param value  要设置的参数\return 无*/
    public void SetItemParamd(int slotid, string paramdname, double value)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            FaceunityWorker.fuItemSetParamd(slot_items[slotid].id, paramdname, value);
        }
    }


    /**\brief 获取道具中的某个double参数值\param itemname 道具的名字\param paramdname  关联参数的关键词\return double参数值*/
    public double GetItemParamd(string itemname, string paramdname)
    {

        return GetItemParamd(GetSlotIDbyName(itemname), paramdname);
    }

    /**\brief 获取道具中的某个double参数值\param slotid 道具在slot数组中的index\param paramdname  关联参数的关键词\return double参数值*/
    public double GetItemParamd(int slotid, string paramdname)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            return FaceunityWorker.fuItemGetParamd(slot_items[slotid].id, paramdname);
        }
        return 0;
    }

    /**\brief 给道具设置一个string参数\param itemname 道具的名字\param paramdname  关联参数的关键词\param value  要设置的参数\return 无*/
    public void SetItemParams(string itemname, string paramdname, string value)
    {
        SetItemParams(GetSlotIDbyName(itemname), paramdname, value);
    }

    /**\brief 给道具设置一个string参数\param slotid 道具在slot数组中的index\param paramdname  关联参数的关键词\param value  要设置的参数\return 无*/
    public void SetItemParams(int slotid, string paramdname, string value)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            FaceunityWorker.fuItemSetParams(slot_items[slotid].id, paramdname, value);
        }
    }

    /**\brief 获取道具中的某个string参数值\param itemname 道具的名字\param paramdname  关联参数的关键词\return string参数值*/
    public string GetItemParams(string itemname, string paramdname)
    {
        return GetItemParams(GetSlotIDbyName(itemname), paramdname);
    }

    /**\brief 获取道具中的某个string参数值\param slotid 道具在slot数组中的index\param paramdname  关联参数的关键词\return string参数值*/
    public string GetItemParams(int slotid, string paramdname)
    {
        if (slotid >= 0 && slotid < SLOTLENGTH)
        {
            byte[] bytes = new byte[1024];
            int i = FaceunityWorker.fuItemGetParams(slot_items[slotid].id, paramdname, bytes, bytes.Length);
            return System.Text.Encoding.Default.GetString(bytes).Replace("\0", "");
        }
        return "";
    }

    /**\brief 给指定slotid位置的道具设置镜像参数，详见文档\param slotid 道具在slot数组中的index\return 无*/
    public void SetItemMirror(int slotid)
    {
        if (slotid < 0 || slotid >= SLOTLENGTH)
        {
            return;
        }
        int itemid = slot_items[slotid].id;
        if (itemid <= 0)
            return;
        var item = slot_items[slotid].item;

        bool isFront = NatCam.Camera.Facing == Facing.Front;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if (item.type == ItemType.Makeup)
        {
            FaceunityWorker.fuItemSetParamd(itemid, "is_flip_points", 1);
        }
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        if (item.type == ItemType.Makeup)
        {
            FaceunityWorker.fuItemSetParamd(itemid, "is_flip_points", 1);
        }
#elif UNITY_ANDROID
        if (item.type == ItemType.Makeup)
        {
            FaceunityWorker.fuItemSetParamd(itemid, "is_flip_points", isFront ? 0 : 1);
        }
#elif UNITY_IOS
        isFront = true;
        if (item.type == ItemType.Makeup)
        {
            FaceunityWorker.fuItemSetParamd(itemid, "is_flip_points", 0);
        }
#endif

        if (item.type == ItemType.Animoji)
        {
            //以下参数只对老的animoji道具起效！
            int needflip = isFront ? 1 : 0;
            //is3DFlipH 参数是用于对3D道具的顶点镜像
            FaceunityWorker.fuItemSetParamd(itemid, "is3DFlipH", needflip);
            //isFlipExpr 参数是用于对道具内部的表情系数的镜像
            FaceunityWorker.fuItemSetParamd(itemid, "isFlipExpr", isFront ? 0 : 1);
            //isFlipTrack 参数是用于对道具的人脸跟踪位置旋转的镜像
            FaceunityWorker.fuItemSetParamd(itemid, "isFlipTrack", needflip);
            //isFlipLight 参数是用于对道具内部的灯光的镜像
            FaceunityWorker.fuItemSetParamd(itemid, "isFlipLight", needflip);
        }
    }

    private void OnApplicationQuit()
    {
        UnLoadAllItems();
        //这些数据必须常驻，直到应用结束才能释放
#if UNITY_EDITOR || UNITY_STANDALONE
        if (img_handle != null && img_handle.IsAllocated)
        {
            img_handle.Free();
        }
#endif
    }
}
