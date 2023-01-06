using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(FaceunityWorker))]
[RequireComponent(typeof(CameraManager))]
public class BaseRenerder : MonoBehaviour
{
    //这个数组存着需要渲染的道具的ID，ID是加载（fu_CreateItemFromPackage）完道具后调用fu_getItemIdxFromPackage返回的值（大于0的整数），这个数组的长度没有被限制，但是
    //由于机器性能限制，一般不会超过10个。这个数组的值可以轮空，如[1,4,0,3,0,2],这里0即没有道具，SDK内部会自动理解成按照[1,4,3,2]的顺序渲染道具，因此为了业务逻辑上的方便
    //可以在此主动声明第N个位置用于某一类道具，如第0位只用于美颜，第1位只用于美妆，第2位只用于滤镜等等。这些位置被称为slot。
    protected int[] itemid_tosdk;
    protected int slot_length = 10;                                               //最大slot长度
    public slot_item[] slot_items;                                                //关联起item和slotid的一个数组，长度为SLOTLENGTH

    //输入类型
    public InputSource input_source = InputSource.Camera;

    //输入的图片纹理
    public Texture2D input_tex;
    //输入的视频纹理
    public RenderTexture input_rendertexture;

    //输出的纹理
    private Texture2D _m_rendered_tex;                                          //用SDK返回的纹理ID新建的纹理
    protected bool _m_tex_created;                                                //m_rendered_tex是否已被创建，这个不需要每帧创建，纹理ID不变就不要重新创建
    [HideInInspector]
    public float camerafov;                                                       //摄像机视角

    //渲染显示UI
    public RawImage rawimg_backgroud;
    private Vector2 canvasReferenceResolution;

    public RawImage rawimg_ori;

    public Text debugText;
    public bool is_debug_text = false;

    public BaseRenerder(int slot_length = FuConst.SLOTLENGTH_TEN)
    {
        this.slot_length = slot_length;
        itemid_tosdk = new int[slot_length];
        slot_items = new slot_item[slot_length];
        for (int i = 0; i < slot_length; i++) slot_items[i].Reset();
    }

    /// <summary>
    /// 等待SDK初始化完毕后再执行其他初始化事件，如初始化相机
    /// </summary>
    public virtual void Awake()
    {
        FaceunityWorker.Instance.OnInitOK += _InitApplication;

        if (rawimg_backgroud != null)
            canvasReferenceResolution = rawimg_backgroud.canvas.GetComponent<CanvasScaler>().referenceResolution;
    }

    /// <summary>
    /// 其他初始化事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void _InitApplication(object sender, EventArgs e) { }


    /// <summary>
    /// 初始化相机或者其他输入源
    /// </summary>
    public IEnumerator _InitBaseCoroutine()
    {
        if (input_source == InputSource.Camera)
        {
            yield return CameraManager.Instance.InitCamera(null, OnCameraStart);
        }
    }

    public void OnCameraStart()
    {
        _m_tex_created = false;
        FaceunityWorker.SetCameraChange(true);
    }

    public virtual void Start()
    {

    }
    public virtual void Update()
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0) return;

        if (input_source == InputSource.Video && input_rendertexture != null)
        {
            UpdateData(IntPtr.Zero, (int)input_rendertexture.GetNativeTexturePtr(), input_rendertexture.width, input_rendertexture.height, UpdateDataMode.TexID);
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT180);
            SelfAdJustRotationMode(FU_ROTATION_MODE.ROT_0);
        }
        else if (input_source == InputSource.Image && input_tex != null)
        {
            UpdateData(IntPtr.Zero, (int)input_tex.GetNativeTexturePtr(), input_tex.width, input_tex.height, UpdateDataMode.TexID);
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT180);
            SelfAdJustRotationMode(FU_ROTATION_MODE.ROT_0);
        }
        else if (input_source == InputSource.Camera && CameraManager.Instance.IsInitialize)
        {
            if (!CameraManager.Instance.UpdateCamera()) return;    //更新摄像机数据
            var cameraOutput = CameraManager.Instance.CameraOutput;
            UpdateData(cameraOutput.bufferPtr, cameraOutput.texID, CameraManager.Instance.Width, CameraManager.Instance.Height, cameraOutput.updateDataMode);

            SelfAdJustNamaMatrix();
            SelfAdJustOutputTexUISize();
            SelfAdJustNamaFov();
            var text = SelfAdJustRotationModeWithAcceleration();
            if (debugText&& is_debug_text) debugText.text = string.Format("{0} | W:{1}, H:{2}, currentMatrix:{3}, IsFrontFacing:{4}, RMode:{5}, Fov:{6}",
                CameraManager.Instance.Type.ToString(), CameraManager.Instance.Width, CameraManager.Instance.Height, currentMatrix.ToString(),
                CameraManager.Instance.IsFrontFacing, text, FaceunityWorker.fuGetFaceProcessorFov());
        }
    }
    /// <summary>
    /// 往SDK输入数据并根据返回的纹理ID新建一个纹理，绑定在UI上，这个返回不是即时的，首次输入数据后真正执行是在GL.IssuePluginEvent执行的时候，因此纹理ID会在下一帧返回
    /// </summary>
    /// <param name="ptr">输入数据buffer的指针</param>
    /// <param name="tex_id">输入数据的纹理ID</param>
    /// <param name="w">该帧图片的宽</param>
    /// <param name="h">该帧图片的高</param>
    /// <param name="mode"></param>
    public void UpdateData(IntPtr ptr, int tex_id, int w, int h, UpdateDataMode mode)
    {
        if (mode == UpdateDataMode.None || (ptr == IntPtr.Zero && tex_id == 0) || (w == 0 && h == 0)) return;
        if (mode == UpdateDataMode.RGBABuffer) FaceunityWorker.SetImage(ptr, 0, false, w, h);
        else if (mode == UpdateDataMode.BGRABuffer) FaceunityWorker.SetImage(ptr, 0, true, w, h);
        else if (mode == UpdateDataMode.TexID) FaceunityWorker.SetImageTexId(tex_id, 0, w, h);
        else if (mode == UpdateDataMode.NV21Buffer) FaceunityWorker.SetNV21Input(ptr, 0, w, h);
        else if (mode == UpdateDataMode.NV21BufferAndTexID) FaceunityWorker.SetDualInput(ptr, tex_id, 0, w, h);
        else if (mode == UpdateDataMode.YUV420Buffer) FaceunityWorker.SetYUVInput(ptr, 0, w, h);
        if (_m_tex_created == false)
        {
            var fu_texid = FaceunityWorker.fu_GetNamaTextureId();
            if (fu_texid > 0)
            {
                if (NeedSwitchWidthHeight())
                    _m_rendered_tex = Texture2D.CreateExternalTexture(h, w, TextureFormat.RGBA32, false, true, (IntPtr)fu_texid);
                else
                    _m_rendered_tex = Texture2D.CreateExternalTexture(w, h, TextureFormat.RGBA32, false, true, (IntPtr)fu_texid);
                if (rawimg_backgroud != null)
                {
                    rawimg_backgroud.texture = _m_rendered_tex;
                }
                _m_tex_created = true;
            }
        }
    }


    public virtual void OnApplicationPause(bool is_paused)
    {
        Debug.LogWarning($"app 是否切入后台：{is_paused}");
        if (is_paused)
        {
            _m_tex_created = false;
        }
        else
        {
            if (CameraManager.Instance.IsInitialize)
            {
                CameraManager.Instance.SelectCamera();
                CameraManager.Instance.StartCamera();
            }
        }
    }
    public virtual void OnApplicationQuit()
    {
        if (CameraManager.Instance.IsInitialize)
        {
            CameraManager.Instance.StopCamera();
            CameraManager.Instance.UnInitCamera();
        }
        UnLoadAllItems();
    }



    #region SelfAdJust

    [HideInInspector]
    public TRANSFORM_MATRIX currentMatrix = TRANSFORM_MATRIX.CCROT0;
    public void SetTransformMatrix(TRANSFORM_MATRIX mat)
    {
        currentMatrix = mat;
        FaceunityWorker.fuSetInputCameraTextureMatrix(mat);
        FaceunityWorker.fuSetInputCameraBufferMatrix(mat);
    }
    public bool NeedSwitchWidthHeight()
    {
        if (currentMatrix == TRANSFORM_MATRIX.CCROT90 ||
            currentMatrix == TRANSFORM_MATRIX.CCROT90_FLIPVERTICAL ||
            currentMatrix == TRANSFORM_MATRIX.CCROT90_FLIPHORIZONTAL ||
            currentMatrix == TRANSFORM_MATRIX.CCROT90_FLIPVERTICAL_FLIPHORIZONTAL ||
            currentMatrix == TRANSFORM_MATRIX.CCROT270 ||
            currentMatrix == TRANSFORM_MATRIX.CCROT270_FLIPVERTICAL ||
            currentMatrix == TRANSFORM_MATRIX.CCROT270_FLIPHORIZONTAL ||
            currentMatrix == TRANSFORM_MATRIX.CCROT270_FLIPVERTICAL_FLIPHORIZONTAL)
            return true;
        return false;
    }

    /// <summary>
    /// 根据运行环境告知Nama输入纹理相对于输出纹理的旋转镜像 OnCameraStart
    /// </summary>
    public void SelfAdJustNamaMatrix()
    {
        if (!CameraManager.Instance.IsInitialize)
        {
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT0);
        }
        else if (CameraManager.Instance.Type == FaceUnity.ICameraType.Legacy)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT180);
#elif UNITY_ANDROID
        if (CameraManager.Instance.IsFrontFacing)
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT90);
        else
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT270_FLIPHORIZONTAL);
#elif UNITY_IOS
        if (CameraManager.Instance.IsFrontFacing)
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT270);
        else
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT270_FLIPHORIZONTAL);
#endif
        }
        else if (CameraManager.Instance.Type == FaceUnity.ICameraType.Native)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT0_FLIPHORIZONTAL);
#elif UNITY_ANDROID
        if (CameraManager.Instance.IsFrontFacing)
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT90_FLIPHORIZONTAL);
        else
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT270);
#elif UNITY_IOS
        if (CameraManager.Instance.IsFrontFacing)
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT270_FLIPHORIZONTAL);
        else
            SetTransformMatrix(TRANSFORM_MATRIX.CCROT270);
#endif
        }

    }

    /// <summary>
    /// 根据运行环境调整输出纹理UI的尺寸 OnCameraStart
    /// </summary>
    public void SelfAdJustOutputTexUISize()
    {
        int inputTexWidth = CameraManager.Instance.Width;
        int inputTexHeight = CameraManager.Instance.Height;
        if (NeedSwitchWidthHeight())
            rawimg_backgroud.rectTransform.sizeDelta = new Vector2(canvasReferenceResolution.y * inputTexHeight / inputTexWidth, canvasReferenceResolution.y);
        else
            rawimg_backgroud.rectTransform.sizeDelta = new Vector2(canvasReferenceResolution.y * inputTexWidth / inputTexHeight, canvasReferenceResolution.y);
        //Debug.LogFormat("inputTexWidth:{0}, inputTexHeight:{1}, sizeDelta:{2}", inputTexWidth, inputTexHeight, rawimg_backgroud.rectTransform.sizeDelta);

        if(rawimg_ori != null)
        {
            if (NeedSwitchWidthHeight())
                rawimg_ori.rectTransform.sizeDelta = new Vector2(canvasReferenceResolution.y * inputTexHeight / inputTexWidth, canvasReferenceResolution.y);
            else
                rawimg_ori.rectTransform.sizeDelta = new Vector2(canvasReferenceResolution.y * inputTexWidth / inputTexHeight, canvasReferenceResolution.y);
        }
    }


    /// <summary>
    /// 根据运行环境调整Nama的Fov OnCameraStart
    /// </summary>
    public void SelfAdJustNamaFov()
    {
        int inputTexWidth = CameraManager.Instance.Width;
        int inputTexHeight = CameraManager.Instance.Height;
        float _fov = camerafov;
        if (!NeedSwitchWidthHeight() && inputTexWidth < inputTexHeight)
        {
            float t = Mathf.Tan(_fov / 2.0f * Mathf.Deg2Rad);
            _fov = 2.0f * Mathf.Atan2(inputTexWidth * t, inputTexHeight) * Mathf.Rad2Deg;
        }
        else if (NeedSwitchWidthHeight() && inputTexWidth > inputTexHeight)
        {
            float t = Mathf.Tan(_fov / 2.0f * Mathf.Deg2Rad);
            _fov = 2.0f * Mathf.Atan2(inputTexHeight * t, inputTexWidth) * Mathf.Rad2Deg;
        }
        FaceunityWorker.fuSetFaceProcessorFov(_fov);

        if (FaceunityWorker.fuGetFaceProcessorFov() == _fov)
        {
            //Debug.LogFormat("fuSetFaceProcessorFov:{0}", _fov);
            //Debug.LogFormat("fuGetFaceProcessorFov:{0}", FaceunityWorker.fuGetFaceProcessorFov());
            //Debug.LogFormat("camerafov: {0}, inputTexWidth:{1}, inputTexHeight:{2}", camerafov, inputTexWidth, inputTexHeight);
        }
        else
        {
            Debug.LogErrorFormat("fuSetFaceProcessorFov:{0} !=  fuGetFaceProcessorFov:{1}", _fov, FaceunityWorker.fuGetFaceProcessorFov());
        }
    }

    //将重力感应转化成ROTATION_MODE的物理值
    public string SelfAdJustRotationModeWithAcceleration()
    {
        var g = Input.acceleration;
        FU_ROTATION_MODE v = FU_ROTATION_MODE.ROT_0;
        string s = "";
        if (Mathf.Abs(g.x) > Mathf.Abs(g.y))
        {
            if (g.x > 0.5f)
            {
                s = "90";
                v = FU_ROTATION_MODE.ROT_90;
            }
            else if (g.x < -0.5f)
            {
                s = "270";
                v = FU_ROTATION_MODE.ROT_270;
            }
            else
            {
                s = "Default 0";
                v = FU_ROTATION_MODE.ROT_0;
            }
        }
        else
        {
            if (g.y > 0.5f)
            {
                s = "180";
                v = FU_ROTATION_MODE.ROT_180;
            }
            else if (g.y < -0.5f)
            {
                s = "0";
                v = FU_ROTATION_MODE.ROT_0;
            }
            else
            {
                s = "Default 0";
                v = FU_ROTATION_MODE.ROT_0;
            }
        }
        s += " | Set " + SelfAdJustRotationMode(v);
        return s;
    }

    //设置AI跟踪方向
    public string SelfAdJustRotationMode(FU_ROTATION_MODE eyeViewRot = FU_ROTATION_MODE.ROT_0)
    {
        var rot = eyeViewRot;

        if (CameraManager.Instance.IsInitialize)
        {
            if (CameraManager.Instance.Type == FaceUnity.ICameraType.Legacy)
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_180 - (int)eyeViewRot + 4) % 4);
#elif UNITY_ANDROID
                if (CameraManager.Instance.IsFrontFacing)
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_90 - (int)eyeViewRot + 4) % 4);
                else
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_270 + (int)eyeViewRot) % 4);
#elif UNITY_IOS
                if (CameraManager.Instance.IsFrontFacing)
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_270 - (int)eyeViewRot + 4) % 4); 
                else
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_270 + (int)eyeViewRot) % 4);
#endif
            }
            else
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_0 + (int)eyeViewRot) % 4);
#elif UNITY_ANDROID
                if (CameraManager.Instance.IsFrontFacing)
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_90 + (int)eyeViewRot) % 4);
                else
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_270 - (int)eyeViewRot + 4) % 4);
#elif UNITY_IOS
                if (CameraManager.Instance.IsFrontFacing)
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_270 + (int)eyeViewRot) % 4);
                else
                    rot = (FU_ROTATION_MODE)(((int)FU_ROTATION_MODE.ROT_270 - (int)eyeViewRot + 4) % 4);
#endif
            }
        }


        FaceunityWorker.fuSetDefaultRotationMode(rot);

        string s = "";
        switch (rot)
        {
            case FU_ROTATION_MODE.ROT_0:
                s = "0";
                break;
            case FU_ROTATION_MODE.ROT_90:
                s = "90";
                break;
            case FU_ROTATION_MODE.ROT_180:
                s = "180";
                break;
            case FU_ROTATION_MODE.ROT_270:
                s = "270";
                break;
        }
        return s;
    }
    #endregion



    #region Item Related 

    public bool curItemLoaded = false;

    /// <summary>
    /// 加载道具
    /// </summary>
    /// <param name="path"></param>
    /// <param name="itemid_tosdk"></param>
    /// <param name="solt_length"></param>
    /// <param name="slot_id"></param>
    /// <returns></returns>
    private IEnumerator LoadItem(Item item, slot_item[] slot_items, string path, int[] itemid_tosdk, int solt_length, int slot_id = 0)
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0) yield break;
        yield return DownLoadBuff(path, (webRquest) =>
        {
            byte[] bundle_bytes = webRquest.downloadHandler.data;
            int itemid = FaceunityWorker.fuCreateItemFromPackage(bundle_bytes, bundle_bytes.Length);
            var errorcode = FaceunityWorker.fuGetSystemError();
            if (errorcode != 0) Debug.LogErrorFormat("errorcode:{0}, {1}", errorcode, Marshal.PtrToStringAnsi(FaceunityWorker.fuGetSystemErrorString(errorcode)));
            if (itemid_tosdk[slot_id] > 0) UnLoadItem(slot_id);
            itemid_tosdk[slot_id] = itemid;
            slot_items[slot_id].id = itemid;
            slot_items[slot_id].name = item.name;
            slot_items[slot_id].item = item;
            FaceunityWorker.fu_SetItemIds(itemid_tosdk, itemid_tosdk.Length, null);
            curItemLoaded = true;
        });
    }
    public IEnumerator LoadItem(Item item, string path, int slot_id = 0)
    {
        yield return LoadItem(item, slot_items, path, itemid_tosdk, itemid_tosdk.Length, slot_id);
    }
    /// <summary>
    /// 加载Bundle
    /// </summary>
    /// <param name="path"></param>
    /// <param name="call_back"></param>
    /// <returns></returns>
    public IEnumerator DownLoadBuff(string path, Action<UnityWebRequest> call_back)
    {
        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(path))
        {
            DownloadHandlerBuffer down_load_buff = new DownloadHandlerBuffer();
            webRequest.downloadHandler = down_load_buff;
            yield return webRequest.SendWebRequest();
            call_back?.Invoke(webRequest);
        }

    }
    /// <summary>
    /// 卸载道具
    /// </summary>
    /// <param name="itemid_tosdk"></param>
    /// <param name="solt_length"></param>
    /// <param name="slot_id"></param>
    /// <returns></returns>
    private bool UnLoadItem(slot_item[] slot_items, int[] itemid_tosdk, int solt_length, int slot_id = 0)
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0)
            return false;
        //if (slot_id >= 0 && slot_id < solt_length && slot_items[slot_id].id > 0)
        if (slot_id >= 0 && slot_id < solt_length)
        {
            FaceunityWorker.fuDestroyItem(itemid_tosdk[slot_id]);
            itemid_tosdk[slot_id] = 0;
            slot_items[slot_id].Reset();
            Debug.LogFormat("UnLoadItem slotid = {0}", slot_id);
            curItemLoaded = false;
            return true;
        }
        Debug.LogError("UnLoadItem Faild!!!");
        return false;
    }
    /// <summary>
    /// 卸载所有道具
    /// </summary>
    private void UnLoadAllItems(slot_item[] slot_items, int[] itemid_tosdk)
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0)
            return;
        Debug.Log("UnLoadAllItems");
        GL.IssuePluginEvent(FaceunityWorker.fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.FuDestroyAllItems);

        for (int i = 0; i < itemid_tosdk.Length; i++)
        {
            itemid_tosdk[i] = 0;
            slot_items[i].Reset();
        }
    }
    public void UnLoadAllItems()
    {
        UnLoadAllItems(slot_items, itemid_tosdk);
    }
    /// <summary>
    /// 输入道具名字返回道具ID
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetItemIDbyName(string name)
    {
        for (int i = 0; i < slot_length; i++)
        {
            if (string.Equals(slot_items[i].name, name))
                return slot_items[i].id;
        }
        return 0;
    }
    /// <summary>
    /// 输入slotid返回道具名字
    /// </summary>
    /// <param name="slotid"></param>
    /// <returns></returns>
    public string GetItemNamebySlotID(int slotid)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            return slot_items[slotid].name;
        }
        return "";
    }

    /// <summary>
    /// 输入道具名字返回道具在slot数组第几个（即slotid）
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetSlotIDbyName(string name)
    {
        for (int i = 0; i < slot_length; i++)
        {
            if (string.Equals(slot_items[i].name, name))
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 输入道具名字卸载道具
    /// </summary>
    /// <param name="itemname"></param>
    /// <returns></returns>
    public bool UnLoadItem(string itemname)
    {
        return UnLoadItem(GetSlotIDbyName(itemname));
    }

    /// <summary>
    /// 输入slotid卸载在该位置的道具
    /// </summary>
    /// <param name="slotid"></param>
    /// <returns></returns>
    public bool UnLoadItem(int slotid)
    {
        return UnLoadItem(slot_items, itemid_tosdk, slot_length, slotid);
    }
    /// <summary>
    /// 输入一张图片给道具当成纹理用
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联图片的关键词</param>
    /// <param name="value">图片的buffer的指针</param>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    public void CreateTexForItem(string itemname, string paramdname, IntPtr value, int width, int height)
    {
        CreateTexForItem(GetSlotIDbyName(itemname), paramdname, value, width, height);
    }
    /// <summary>
    /// 输入一张图片给道具当成纹理用
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联图片的关键词</param>
    /// <param name="value">图片的buffer的指针</param>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    public void CreateTexForItem(int slotid, string paramdname, IntPtr value, int width, int height)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            FaceunityWorker.fuCreateTexForItem(slot_items[slotid].id, paramdname, value, width, height);
        }
    }
    /// <summary>
    /// 删除关联道具的纹理
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联图片的关键词</param>
    public void DeleteTexForItem(string itemname, string paramdname)
    {
        DeleteTexForItem(GetSlotIDbyName(itemname), paramdname);
    }

    /// <summary>
    /// 删除关联道具的纹理
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联图片的关键词</param>
    public void DeleteTexForItem(int slotid, string paramdname)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            FaceunityWorker.fuDeleteTexForItem(slot_items[slotid].id, paramdname);
        }
    }

    public void BindItem(int slotid_dst, int slotid_src)
    {
        if (slotid_dst >= 0 && slotid_dst < slot_length && slotid_src >= 0 && slotid_src < slot_length)
        {
            int[] value = { slot_items[slotid_src].id };
            FaceunityWorker.fuBindItems(slot_items[slotid_dst].id, value, value.Length);
            Debug.LogFormat("BindItem: slotid_dst={0}, slotid_src={1}", slotid_dst, slotid_src);
        }
    }

    public void UnBindItem(int slotid_dst, int slotid_src)
    {
        if (slotid_dst >= 0 && slotid_dst < slot_length && slotid_src >= 0 && slotid_src < slot_length)
        {
            int[] value = { slot_items[slotid_src].id };
            FaceunityWorker.fuUnbindItems(slot_items[slotid_dst].id, value, value.Length);
            Debug.LogFormat("UnBindItem: slotid_dst={0}, slotid_src={1}", slotid_dst, slotid_src);
        }
    }

    public void UnBindItemAll(int slotid_dst)
    {
        if (slotid_dst >= 0 && slotid_dst < slot_length)
        {
            FaceunityWorker.fuUnbindAllItems(slot_items[slotid_dst].id);
            Debug.LogFormat("UnBindItemAll: slotid_dst = {0}", slotid_dst);
        }
    }
    /// <summary>
    /// 给道具设置一个数组
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联数组的关键词</param>
    /// <param name="value">要设置的数组</param>
    public void SetItemParamdv(string itemname, string paramdname, double[] value)
    {
        SetItemParamdv(GetSlotIDbyName(itemname), paramdname, value);
    }

    /// <summary>
    /// 给道具设置一个数组
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联数组的关键词</param>
    /// <param name="value">要设置的数组</param>
    public void SetItemParamdv(int slotid, string paramdname, double[] value)
    {
        if (slotid >= 0 && slotid < slot_length && value != null && value.Length > 0)
        {
            FaceunityWorker.fuItemSetParamdv(slot_items[slotid].id, paramdname, value, value.Length);
        }
    }

    /// <summary>
    /// 给道具设置一个double参数
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <param name="value">要设置的参数</param>
    public void SetItemParamd(string itemname, string paramdname, double value)
    {
        SetItemParamd(GetSlotIDbyName(itemname), paramdname, value);
    }

    /// <summary>
    /// 给道具设置一个double参数
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <param name="value">要设置的参数</param>
    public void SetItemParamd(int slotid, string paramdname, double value)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            FaceunityWorker.fuItemSetParamd(slot_items[slotid].id, paramdname, value);
        }
    }

    /// <summary>
    /// 获取道具中的某个double参数值
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <returns></returns>
    public double GetItemParamd(string itemname, string paramdname)
    {

        return GetItemParamd(GetSlotIDbyName(itemname), paramdname);
    }
    /// <summary>
    /// 获取道具中的某个double参数值
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <returns></returns>
    public double GetItemParamd(int slotid, string paramdname)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            return FaceunityWorker.fuItemGetParamd(slot_items[slotid].id, paramdname);
        }
        return 0;
    }

    /// <summary>
    /// 给道具设置一个string参数
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <param name="value">要设置的参数</param>
    public void SetItemParams(string itemname, string paramdname, string value)
    {
        SetItemParams(GetSlotIDbyName(itemname), paramdname, value);
    }

    /// <summary>
    /// 给道具设置一个string参数
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <param name="value">要设置的参数</param>
    public void SetItemParams(int slotid, string paramdname, string value)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            FaceunityWorker.fuItemSetParams(slot_items[slotid].id, paramdname, value);
        }
    }

    /// <summary>
    /// 获取道具中的某个string参数值
    /// </summary>
    /// <param name="itemname">道具的名字</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <returns>参数值</returns>
    public string GetItemParams(string itemname, string paramdname)
    {
        return GetItemParams(GetSlotIDbyName(itemname), paramdname);
    }

    /// <summary>
    /// 获取道具中的某个string参数值
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    /// <param name="paramdname">关联参数的关键词</param>
    /// <returns>参数值</returns>
    public string GetItemParams(int slotid, string paramdname)
    {
        if (slotid >= 0 && slotid < slot_length)
        {
            byte[] bytes = new byte[1024];
            int i = FaceunityWorker.fuItemGetParams(slot_items[slotid].id, paramdname, bytes, bytes.Length);
            return System.Text.Encoding.Default.GetString(bytes).Replace("\0", "");
        }
        return "";
    }
    #endregion
}
