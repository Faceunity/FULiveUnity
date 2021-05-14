using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManagerSimple : MonoBehaviour {

    RenderSimple rs;

    public Button Btn_Switch;//切换相机按钮
    public GameObject Image_FaceDetect; //Active时表示没有检测到人脸
    


    //Awake时注册SDK初始化完成事件
    void Awake()
    {
        rs = GetComponent<RenderSimple>();
        FaceunityWorker.OnInitOK += InitApplication;
    }

    void Start()
    {

    }

    //应用初始化时注册UI事件
    void InitApplication(object source, EventArgs e)
    {
        RegisterUIFunc();
    }

    //每帧检测应用退出的按键事件
    //每帧检测当前是否有人脸，如果没有人脸就显示相关UI
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (FaceunityWorker.instance.m_need_update_headnum > 0)
            Image_FaceDetect.SetActive(false);
        else
            Image_FaceDetect.SetActive(true);
    }


    //注册切换相机事件的到相关UI
    void RegisterUIFunc()
    {
        Btn_Switch.onClick.AddListener(delegate { rs.SwitchCamera(); });
    }

    //反注册切换相机事件的到相关UI
    void UnRegisterUIFunc()
    {
        Btn_Switch.onClick.RemoveAllListeners();
    }

    //应用退出时反注册UI
    void OnApplicationQuit()
    {
        UnRegisterUIFunc();
    }
}
