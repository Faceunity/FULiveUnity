using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using NatCamU.Core;
using System.Runtime.InteropServices;

public class UIManagerForDataOut : MonoBehaviour
{

    RenderToModel rtm;

    public Button Btn_Switch;   //切换相机
    public Toggle enableTrack;      //开启跟踪位置
    public Toggle[] togglefaces;    //选择要同步人脸的3D模型
    public GameObject Image_FaceDetect; //显示是否检测到人脸

    public StdController[] stcs;        //同步人脸控制器

    //Awake时添加SDK初始化完成回调
    void Awake()
    {
        rtm = GetComponent<RenderToModel>();
        FaceunityWorker.OnInitOK += InitApplication;
    }

    //默认跟踪人脸位置
    void Start()
    {
        foreach (StdController stc in stcs)
        {
            stc.gameObject.SetActive(false);
        }
        rtm.ifTrackPos = enableTrack.isOn;
    }

    //SDK初始化完成后执行UI注册，并加载舌头跟踪需要的文件
    void InitApplication(object source, EventArgs e)
    {
        NatCam.OnStart += OnStart;
        RegisterUIFunc();
    }

    public void OnStart()
    {
        SetHeadActiveByToggle();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (FaceunityWorker.instance.m_need_update_headnum > 0)
            Image_FaceDetect.SetActive(false);
        else
            Image_FaceDetect.SetActive(true);
    }


    void RegisterUIFunc()
    {
        Btn_Switch.onClick.AddListener(delegate
        {
            rtm.SwitchCamera();
        });

        //不同SDK模式下舌头跟踪所需要的条件，详见文档
        enableTrack.onValueChanged.AddListener(delegate
        {
            if (enableTrack.isOn)
            {
                rtm.ifTrackPos = true;
                FaceunityWorker.SetRunningMode(FaceunityWorker.FU_RUNNING_MODE.FU_RUNNING_MODE_RENDERITEMS);
                StartCoroutine(rtm.delaySet());
            }
            else
            {
                rtm.ifTrackPos = false;
                FaceunityWorker.SetRunningMode(FaceunityWorker.FU_RUNNING_MODE.FU_RUNNING_MODE_TRACK);
                FaceunityWorker.fu_SetTongueTracking(1);
                rtm.ReSetBackGroud();
                FaceunityWorker.fuSetTrackFaceAIType(FaceunityWorker.FUAITYPE.FUAITYPE_FACEPROCESSOR_FACECAPTURE | FaceunityWorker.FUAITYPE.FUAITYPE_FACEPROCESSOR_FACECAPTURE_TONGUETRACKING);
            }
        });

        for (int i = 0; i < togglefaces.Length; i++)
        {
            int id = i;
            togglefaces[i].onValueChanged.AddListener(delegate
            {
                if (togglefaces[id].isOn)
                {
                    if (id < stcs.Length)
                    {
                        stcs[id].gameObject.SetActive(true);
                        stcs[id].ResetTransform();
                    }
                }
                else
                {
                    if (id < stcs.Length)
                    {
                        stcs[id].gameObject.SetActive(false);
                        stcs[id].ResetTransform();
                    }
                }
            });
        }

    }

    void UnRegisterUIFunc()
    {
        Btn_Switch.onClick.RemoveAllListeners();
        enableTrack.onValueChanged.RemoveAllListeners();
    }

    //切换同步人脸的模型
    void SetHeadActiveByToggle()
    {
        Debug.Log("SetHeadActiveByToggle!!!!!!!!!!");
        for (int i = 0; i < togglefaces.Length; i++)
        {
            if (togglefaces[i].isOn)
            {
                if (i < stcs.Length)
                {
                    stcs[i].gameObject.SetActive(true);
                    stcs[i].ResetTransform();
                }
            }
            else
            {
                if (i < stcs.Length)
                {
                    stcs[i].gameObject.SetActive(false);
                    stcs[i].ResetTransform();
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        UnRegisterUIFunc();
    }
}
