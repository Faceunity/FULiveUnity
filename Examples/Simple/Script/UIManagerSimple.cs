using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManagerSimple :MonoBehaviour {

    private RenderSimple _render_simple;
    public Button btn_switch;                                                     //切换相机按钮
    public GameObject image_face_detect;                                           //Active时表示没有检测到人脸

    /// <summary>
    /// Awake时注册SDK初始化完成事件
    /// </summary>
    void Awake()
    {
        _render_simple = GetComponent<RenderSimple>();
        FaceunityWorker.Instance.OnInitOK += _InitApplication;
    }

    void Start()
    {
    }
    /// <summary>
    ///  应用初始化时注册UI事件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
   private void _InitApplication(object source, EventArgs e)
    {
        _RegisterUIFunc();
    }
    /// <summary>
    /// 每帧检测应用退出的按键事件 检测当前是否有人脸，如果没有人脸就显示相关UI
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (FaceunityWorker.Instance.m_need_update_headnum > 0)
        {
            image_face_detect.SetActive(false);
        }
        else
        {
            image_face_detect.SetActive(true);
        }
    }
    /// <summary>
    /// 注册切换相机事件的到相关UI
    /// </summary>
    private void _RegisterUIFunc()
    {
        btn_switch.onClick.AddListener(()=> {
            CameraManager.Instance.SwitchCamera();
        });
    }

    /// <summary>
    /// 反注册切换相机事件的到相关UI
    /// </summary>
   private void _UnRegisterUIFunc()
    {
        btn_switch.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 应用退出时反注册UI
    /// </summary>
   private void _OnApplicationQuit()
    {
        _UnRegisterUIFunc();
    }
}
