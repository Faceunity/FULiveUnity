using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class RenderToTexture : BaseRenerder
{
    public delegate void LoadItemCallback(Item item);                             //加载道具完毕的委托
    private bool LoadingItem = false;                                             //是否正在加载道具，道具的加载是一个协程，不是瞬间完成的，因此为了防止调用混乱，用这个变量主动控制

    public RenderToTexture() : base(FuConst.SLOTLENGTH_TEN) { }

    /// <summary>
    /// 延迟设置道具的镜像参数，因为相机切换不是瞬间完成的
    /// </summary>
    /// <returns></returns>
    public void OnCameraStart_RTT()
    {
        for (int i = 0; i < slot_length; i++)
        {
            SelfAdJustItemMirror(i);
        }
    }
    public override void Awake()
    {
        base.Awake();
        camerafov = FuConst.RENERDER_TO_TEXTURE_FOV;
    }

    /// <summary>
    /// SDK初始化完成后会执行这个回调，开启相机初始化协程
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected override void _InitApplication(object source, EventArgs e)
    {
        base._InitApplication(source, e);
        FaceunityWorker.fu_SetRuningMode(FU_RUNNING_MODE.FU_RUNNING_MODE_RENDERITEMS);
        StartCoroutine(_InitCoroutine());
    }

    public IEnumerator _InitCoroutine()
    {
        yield return _InitBaseCoroutine();
        if (CameraManager.Instance.IsInitialize)
        {
            CameraManager.Instance.OnStart += OnCameraStart_RTT;
        }
    }

    public override void Start()
    {
        base.Start();
    }

    public IEnumerator ResetRawImage_Ori()
    {
        if (rawimg_ori == null) yield break;
        rawimg_ori.enabled = false;

        yield return new WaitForSeconds(0.5f);
        rawimg_ori.enabled = true;
        rawimg_ori.texture = CameraManager.Instance.GetWebTex();
        SelfAdjusUISize(rawimg_ori, 1f, CameraManager.Instance.Width, CameraManager.Instance.Height);
    }

    public override void Update()
    {
        updateOriData();
        base.Update();
    }
    //输出的纹理
    //private Texture2D _m_rendered_tex; //未经SDK处理的纹理
    private void updateOriData()
    {
        if (!rawimg_ori.gameObject.activeSelf) return;

        if(!isInitRawimg_Ori)
        {
            SelfAdjusUISize(rawimg_ori, 1f, CameraManager.Instance.Width, CameraManager.Instance.Height);
            Vector2 targetResolution = rawimg_ori.canvas.GetComponent<CanvasScaler>().referenceResolution;
            Vector2 currentResolution = new Vector2(CameraManager.Instance.Width, CameraManager.Instance.Height);
            if (NeedSwitchWidthHeight())
                rawimg_ori.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.y / currentResolution.x, targetResolution.y);
            else
                rawimg_ori.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.x / currentResolution.y, targetResolution.y);
            isInitRawimg_Ori = true;
            rawimg_ori.texture = CameraManager.Instance.GetWebTex();
        }

    }

    private bool isInitRawimg_Ori = false;
    public void SelfAdjusUISize(RawImage rawimg, float sizeadjust, int texwidth, int texheight,
        bool sRGBToLinear = false, bool LinearTosRGB = false, bool adjustflip = true, bool fixedSizeDelta = false)
    {
        Vector2 targetResolution = rawimg.canvas.GetComponent<CanvasScaler>().referenceResolution;
        Vector2 currentResolution = new Vector2(texwidth, texheight);
        var mat = new Material(rawimg.material);
        mat.SetFloat("_sRGBToLinear", sRGBToLinear ? 1.0f : 0.0f);
        mat.SetFloat("_LinearTosRGB", LinearTosRGB ? 1.0f : 0.0f);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
#if UNITY_EDITOR_WIN
        rawimg.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.x / currentResolution.y, targetResolution.y) * sizeadjust;
#else
        rawimg.rectTransform.sizeDelta = new Vector2(3413.333f,1920f) * sizeadjust;
#endif
        if (adjustflip)
        {
            mat.SetFloat("_SwichXY", 0.0f);   //Rotation processing
            mat.SetFloat("_FlipX", 1.0f);
            mat.SetFloat("_FlipY", 0.0f);
        }
        FaceunityWorker.fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX.CCROT180);
#elif UNITY_STANDALONE_OSX &&!UNITY_EDITOR                                     
        //rawimg.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.x / currentResolution.y, targetResolution.y) * sizeadjust;
        rawimg.rectTransform.sizeDelta = new Vector2(3413.333f,1920f) * sizeadjust;
        if (adjustflip)
        {
            mat.SetFloat("_SwichXY", 0.0f);   //Rotation processing
            mat.SetFloat("_FlipX", 1.0f);
            mat.SetFloat("_FlipY", 0.0f);
        }
        FaceunityWorker.fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX.CCROT180);
#elif UNITY_ANDROID              
        if(fixedSizeDelta)
            rawimg.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.x / currentResolution.y, targetResolution.y) * sizeadjust;
        else
            rawimg.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.y / currentResolution.x, targetResolution.y) * sizeadjust;
            
        if (adjustflip)
        {
            if (CameraManager.Instance.IsFrontFacing)
            {
                mat.SetFloat("_SwichXY",1.0f);
                mat.SetFloat("_FlipX",0.0f);
                mat.SetFloat("_FlipY",0.0f);
                FaceunityWorker.fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX.CCROT90);
            }
            else
            {
                mat.SetFloat("_SwichXY",1.0f);
                mat.SetFloat("_FlipX",1.0f);
                mat.SetFloat("_FlipY",0.0f);
                FaceunityWorker.fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX.CCROT270_FLIPHORIZONTAL);
            }
        }
#elif UNITY_IOS          
        if(fixedSizeDelta)
            rawimg.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.x / currentResolution.y, targetResolution.y) * sizeadjust;
        else
            rawimg.rectTransform.sizeDelta = new Vector2(targetResolution.y * currentResolution.y / currentResolution.x, targetResolution.y) * sizeadjust;
        if (adjustflip)
        {
            if (CameraManager.Instance.IsFrontFacing)
            {
                mat.SetFloat("_SwichXY",1.0f);
                mat.SetFloat("_FlipX",1.0f);
                mat.SetFloat("_FlipY",0.0f);
                FaceunityWorker.fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX.CCROT270);
            }
            else
            {
                mat.SetFloat("_SwichXY",1.0f);
                mat.SetFloat("_FlipX",1.0f);
                mat.SetFloat("_FlipY",1.0f);
                FaceunityWorker.fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX.CCROT90_FLIPVERTICAL);
            }
        }
#endif

        rawimg.material = mat;
    }

    public override void OnApplicationPause(bool isPause)
    {
        base.OnApplicationPause(isPause);
        if (isPause)
        {
            rawimg_ori.enabled = false;
            rawimg_ori.texture = null;
        }
        else
            StartCoroutine(ApplicationPause());
    }

    IEnumerator ApplicationPause()
    {
        yield return new WaitForSeconds(0.5f);
        rawimg_ori.enabled = true;
        rawimg_ori.texture = (Texture)CameraManager.Instance.GetWebTex();
    }

    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

    /// <summary>
    /// 封装过的加载道具用的接口，配合slot的概念和item这个struct控制多个不同类型的道具的加载卸载
    /// </summary>
    /// <param name="item">要加载的道具的item，封装过的道具信息集合，方便业务逻辑，详见itemconfig</param>
    /// <param name="slotid">道具要加载的位置（slot），默认值为0，即slot数组的第0位</param>
    /// <param name="cb">加载道具完毕后会自动执行的回调，可以为空</param>
    /// <returns>无</returns>
    public IEnumerator LoadItem(Item item, int slotid = 0, LoadItemCallback cb = null)
    {
        if (FaceunityWorker.fuIsLibraryInit() == 0)
            yield break;
        if (LoadingItem == false && item.fullname != null && item.fullname.Length != 0 && slotid >= 0 && slotid < slot_length)
        {
            LoadingItem = true;
            int tempslot = GetSlotIDbyName(item.name);
            if (tempslot < 0)                                                     //如果尚未载入道具数据
            {
                string bundle = Util.GetStreamingAssetsPath() + FuConst.AITYPE_PATH_ROOT + item.fullname + ".bundle";
                yield return LoadItem(item, bundle, slotid);
            }
            else if (tempslot != slotid)                                          //道具已载入，但是不在请求的slot槽内
            {
                UnLoadItem(slotid);
                itemid_tosdk[slotid] = slot_items[tempslot].id;
                slot_items[slotid] = slot_items[tempslot];
                itemid_tosdk[tempslot] = 0;
                slot_items[tempslot].Reset();
                FaceunityWorker.fu_SetItemIds(itemid_tosdk, itemid_tosdk.Length, null);
            }
            else                                                                  //tempslot == slotid 即重复载入同一个道具进同一个slot槽，直接跳过
            {
                Debug.Log("重复载入Item：" + item.name + "  slotid=" + slotid);
            }
            SelfAdJustItemMirror(slotid);
            if (cb != null) cb(item);                                             //触发载入道具完成事件
            LoadingItem = false;
        }
    }
    /// <summary>
    /// 给指定slotid位置的道具设置镜像参数，详见文档
    /// </summary>
    /// <param name="slotid">道具在slot数组中的index</param>
    public void SelfAdJustItemMirror(int slotid)
    {
        if (slotid < 0 || slotid >= slot_length) return;
        int itemid = slot_items[slotid].id;
        if (itemid <= 0) return;
        var item = slot_items[slotid].item;

        if (CameraManager.Instance.IsInitialize)
        {
            if (CameraManager.Instance.Type == FaceUnity.ICameraType.Legacy)
            {
                if (item.type == ItemType.Makeup)
                    FaceunityWorker.fuItemSetParamd(itemid, "is_flip_points", CameraManager.Instance.IsFrontFacing ? 1 : 0);
            }
            else
            {
                if (item.type == ItemType.Makeup)
                    FaceunityWorker.fuItemSetParamd(itemid, "is_flip_points", CameraManager.Instance.IsFrontFacing ? 0 : 1);
            }
        }

        if (item.type == ItemType.Animoji)
            FaceunityWorker.fuItemSetParamd(itemid, "isFlipExpr", 1);
    }
}
