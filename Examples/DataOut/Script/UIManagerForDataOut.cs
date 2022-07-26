using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIManagerForDataOut : MonoBehaviour
{
    private RenderToModel _render_to_model;
    public Button btn_switch;                                                     //切换相机
    public Toggle enable_track;                                                   //开启跟踪位置
    public Toggle[] toggle_faces;                                                 //选择要同步人脸的3D模型
    public GameObject image_face_detect;                                          //显示是否检测到人脸
    public List<StdController> std_controller_list = new List<StdController>();   //同步人脸控制器
    [HideInInspector]
    public List<EyeController> eye_controller_list = new List<EyeController>();

    /// <summary>
    /// Awake时添加SDK初始化完成回调
    /// </summary>
    void Awake()
    {
        _render_to_model = GetComponent<RenderToModel>();
        FaceunityWorker.Instance.OnInitOK += _InitApplication;

        foreach (StdController stc in std_controller_list)
        {
            stc.gameObject.SetActive(false);
            eye_controller_list.AddRange(stc.GetComponentsInChildren<EyeController>());
        }
        _render_to_model.if_track_pos = enable_track.isOn;
    }

    /// <summary>
    /// SDK初始化完成后执行UI注册，并加载舌头跟踪需要的文件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    private void _InitApplication(object source, EventArgs e)
    {
        _SetHeadActiveByToggle();
        _RegisterUIFunc();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (FaceunityWorker.Instance.m_need_update_headnum > 0) image_face_detect.SetActive(false);
        else image_face_detect.SetActive(true);

    }

    private void _RegisterUIFunc()
    {
        btn_switch.onClick.AddListener(delegate
        {
            CameraManager.Instance.SwitchCamera();
        });

        //不同SDK模式下舌头跟踪所需要的条件，详见文档
        enable_track.onValueChanged.AddListener(delegate
        {
            _render_to_model.if_track_pos = enable_track.isOn;
            foreach (StdController stc in std_controller_list)
            {
                stc.ResetTransform();
                stc.ResetBlendShape();
            }
            foreach (var eyec in eye_controller_list)
            {
                eyec.ResetTransform();
            }
        });

        for (int i = 0; i < toggle_faces.Length; i++)
        {
            int id = i;
            toggle_faces[i].onValueChanged.AddListener(delegate
            {
                if (toggle_faces[id].isOn)
                {
                    if (id < std_controller_list.Count)
                    {
                        std_controller_list[id].gameObject.SetActive(true);
                        std_controller_list[id].ResetTransform();
                        std_controller_list[id].ResetBlendShape();
                    }
                }
                else
                {
                    if (id < std_controller_list.Count)
                    {
                        std_controller_list[id].gameObject.SetActive(false);
                        std_controller_list[id].ResetTransform();
                        std_controller_list[id].ResetBlendShape();
                    }
                }
                foreach (var eyec in eye_controller_list)
                {
                    eyec.ResetTransform();
                }
            });
        }

    }

    private void _UnRegisterUIFunc()
    {
        btn_switch.onClick.RemoveAllListeners();
        enable_track.onValueChanged.RemoveAllListeners();
    }

    /// <summary>
    /// 切换同步人脸的模型
    /// </summary>
    private void _SetHeadActiveByToggle()
    {
        for (int i = 0; i < toggle_faces.Length; i++)
        {
            if (toggle_faces[i].isOn)
            {
                if (i < std_controller_list.Count)
                {
                    std_controller_list[i].gameObject.SetActive(true);
                    std_controller_list[i].ResetTransform();
                    std_controller_list[i].ResetBlendShape();
                }
            }
            else
            {
                if (i < std_controller_list.Count)
                {
                    std_controller_list[i].gameObject.SetActive(false);
                    std_controller_list[i].ResetTransform();
                    std_controller_list[i].ResetBlendShape();
                }
            }
            foreach (var eyec in eye_controller_list)
            {
                eyec.ResetTransform();
            }
        }
    }

    private void _OnApplicationQuit()
    {
        _UnRegisterUIFunc();
    }
}
