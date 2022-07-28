using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class RenderSimple : BaseRenerder
{
    public string load_bundlename;

    public RenderSimple() : base(FuConst.SLOTLENGTH) {}

    public override void Awake()
    {
        base.Awake();
        camerafov = FuConst.SIMPLE_RENERDER_FOV;
    }

    protected override void _InitApplication(object source, EventArgs e)
    {
        base._InitApplication(source, e);
        StartCoroutine(_InitCoroutine());
    }
    private IEnumerator _InitCoroutine()
    {
        yield return _LoadDefaultItem();
        yield return Util.end_of_frame;
        yield return _InitBaseCoroutine();
    }
    private IEnumerator _LoadDefaultItem()
    {
        if (string.IsNullOrEmpty(load_bundlename))
        {
            yield return LoadItem(ItemConfig.defaultItem[0], Util.GetStreamingAssetsPath() + FuConst.AITYPE_PATH);
            SetItemParamd(0, FuConst.AITYPE, (double)FUAITYPE.FUAITYPE_FACEPROCESSOR);
        }
        else
        {
            yield return LoadItem(ItemConfig.defaultItem[0], Util.GetStreamingAssetsPath() + FuConst.AITYPE_PATH_ROOT + load_bundlename + ".bundle");
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

    public override void OnApplicationPause(bool is_pause)
    {
        base.OnApplicationPause(is_pause);
    }

    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }
}
