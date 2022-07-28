using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManagerForDataOut_Multiple : MonoBehaviour
{

    private RenderToModel _render_to_model;

    public Button btn_switch;
    public GameObject image_face_detect;

    public StdController[] std_controller_array;                                                  //如果有更多模型，可以提高MAXFACE值
    public EyeController[][] eye_controller_array;

    private bool[] _marks;
    private string _text;

    void Awake()
    {
        _render_to_model = GetComponent<RenderToModel>();
        FaceunityWorker.Instance.OnInitOK += _InitApplication;

        _marks = new bool[std_controller_array.Length];
        eye_controller_array = new EyeController[std_controller_array.Length][];
        for (int i = 0; i < std_controller_array.Length; i++)
        {
            std_controller_array[i].gameObject.SetActive(false);
            eye_controller_array[i] = std_controller_array[i].GetComponentsInChildren<EyeController>();
        }
        _render_to_model.if_track_pos = true;
    }

   private void _InitApplication(object source, EventArgs e)
    {
        _RegisterUIFunc();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (FaceunityWorker.Instance.m_need_update_headnum > 0) image_face_detect.SetActive(false);
        else image_face_detect.SetActive(true);
        for (int i = 0; i < std_controller_array.Length; i++)
        {
            _marks[i] = false;
        }
        string tmps = "trueid:";
        string tmps2 = "indexid:";
        var face_true_id = FaceunityWorker.Instance.face_true_id;
        for (int i = 0; i < face_true_id.Count; i++)
        {
            int trueid = face_true_id[i];
            if (trueid < std_controller_array.Length && trueid >= 0)
            {
                tmps += trueid + ",";
                tmps2 += i + ",";
                std_controller_array[trueid].faceid = i;
                for (int j = 0; j < eye_controller_array[trueid].Length; j++)
                {
                    eye_controller_array[trueid][j].faceid = i;
                }
                std_controller_array[trueid].gameObject.SetActive(true);
                _marks[trueid] = true;
            }
        }
        for (int i = 0; i < _marks.Length; i++)
        {
            if (_marks[i] == false)
                std_controller_array[i].gameObject.SetActive(false);
        }

        _text = "faceNum=" + FaceunityWorker.Instance.m_need_update_headnum + "\n" + tmps + "\n" + tmps2;
    }

    private void _RegisterUIFunc()
    {
        btn_switch.onClick.AddListener(delegate
        {
            CameraManager.Instance.SwitchCamera();
        });
    }

    private void _UnRegisterUIFunc()
    {
        btn_switch.onClick.RemoveAllListeners();
    }

    private void _OnApplicationQuit()
    {
        _UnRegisterUIFunc();
    }

    void OnGUI()
    {
        if (_text != null)
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, h * 2 / 100, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            GUI.Label(rect, _text, style);
        }
    }
}
