using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManagerForDataOut_Multiple : MonoBehaviour
{

    RenderToModel rtm;

    public Button Btn_Switch;
    public GameObject Image_FaceDetect;

    public StdController[] stcs;    //如果有更多模型，可以提高MAXFACE值
    public EyeController[][] eyecs;

    private bool[] marks;
    private string text;

    void Awake()
    {
        rtm = GetComponent<RenderToModel>();
        FaceunityWorker.OnInitOK += InitApplication;
    }

    void Start()
    {
        marks = new bool[stcs.Length];
        eyecs = new EyeController[stcs.Length][];
        for (int i = 0; i < stcs.Length; i++)
        {
            stcs[i].gameObject.SetActive(false);
            eyecs[i] = stcs[i].GetComponentsInChildren<EyeController>();
        }
        rtm.ifTrackPos = true;
    }

    void InitApplication(object source, EventArgs e)
    {
        RegisterUIFunc();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (FaceunityWorker.instance.m_need_update_headnum > 0)
            Image_FaceDetect.SetActive(false);
        else
            Image_FaceDetect.SetActive(true);

        for (int i = 0; i < stcs.Length; i++)
        {
            marks[i] = false;
        }
        string tmps = "trueid:";
        string tmps2 = "indexid:";
        var face_true_id = FaceunityWorker.instance.face_true_id;
        for (int i = 0; i < face_true_id.Count; i++)
        {
            int trueid = face_true_id[i];
            if (trueid < stcs.Length && trueid >= 0)
            {
                tmps += trueid + ",";
                tmps2 += i + ",";
                stcs[trueid].faceid = i;
                for (int j = 0; j < eyecs[trueid].Length; j++)
                {
                    eyecs[trueid][j].faceid = i;
                }
                stcs[trueid].gameObject.SetActive(true);
                marks[trueid] = true;
            }
        }
        for (int i = 0; i < marks.Length; i++)
        {
            if (marks[i] == false)
                stcs[i].gameObject.SetActive(false);
        }

        text = "faceNum=" + FaceunityWorker.instance.m_need_update_headnum + "\n" + tmps + "\n" + tmps2;
    }

    void RegisterUIFunc()
    {
        Btn_Switch.onClick.AddListener(delegate
        {
            rtm.SwitchCamera();
        });
    }

    void UnRegisterUIFunc()
    {
        Btn_Switch.onClick.RemoveAllListeners();
    }

    void OnApplicationQuit()
    {
        UnRegisterUIFunc();
    }

    void OnGUI()
    {
        if (text != null)
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, h * 2 / 100, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            GUI.Label(rect, text, style);
        }
    }
}
