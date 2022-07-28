using UnityEngine;
using System.Collections;
using System;
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

    public override void Update()
    {
        base.Update();
    }

    public override void OnApplicationPause(bool isPause)
    {
        base.OnApplicationPause(isPause);
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
