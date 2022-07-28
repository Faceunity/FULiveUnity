using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

public class RenderToModel : BaseRenerder
{
    public Camera camera3d;                                                       //渲染3D物体的相机
    public bool if_track_pos = false;                                             //是否跟踪人脸位置，选择true时只跟踪人脸旋转
    public RenderToModel() : base(FuConst.SLOTLENGTH) {}

    public override void Awake()
    {
        base.Awake();
        camerafov = camera3d.fieldOfView;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected override void _InitApplication(object source, EventArgs e)
    {
        base._InitApplication(source, e);
        StartCoroutine(_InitCoroutine());
    }
    /// <summary>
    /// 加载默认道具和初始化相机
    /// </summary>
    /// <returns></returns>
    private IEnumerator _InitCoroutine()
    {
        yield return _LoadDefaultItem();
        yield return Util.end_of_frame;
        yield return _InitBaseCoroutine();
    }

    /// <summary>
    /// 加载默认道具
    /// </summary>
    /// <returns></returns>
    private IEnumerator _LoadDefaultItem()
    {
        yield return LoadItem(ItemConfig.defaultItem[0], Util.GetStreamingAssetsPath() + FuConst.AITYPE_PATH);
        SetItemParamd(0, FuConst.AITYPE, (double)(FUAITYPE.FUAITYPE_FACEPROCESSOR_FACECAPTURE |FUAITYPE.FUAITYPE_FACEPROCESSOR_FACECAPTURE_TONGUETRACKING));
    }

    public override void Start()
    {
        base.Start();
    }
    
    public override void Update()
    {
        base.Update();
    }

    public override void OnApplicationPause(bool is_pause)
    {
        base.OnApplicationPause(is_pause);
    }
    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }
}
