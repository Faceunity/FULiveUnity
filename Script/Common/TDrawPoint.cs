using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个组件用来画出人脸跟踪点，但是限制很多，仅用于测试
public class TDrawPoint : MonoBehaviour
{
    Material markmat;

    void Start()
    {
        markmat = new Material(Shader.Find("Particles/Alpha Blended"))
        {
            hideFlags = HideFlags.HideAndDontSave,
            shader = { hideFlags = HideFlags.HideAndDontSave }
        };
    }
    //Recognized为FaceunityWorker.m_landmarks[i].m_data
    public void rendmark(float[] Recognized)
    {
        if (!markmat)
        {
            Debug.LogError("Material is null");
            return;
        }

        if (Recognized == null)
            return;
        if (Recognized.Length < 75 * 2)
            return;

        markmat.SetPass(0);                  
        GL.LoadOrtho();                       
        GL.Begin(GL.QUADS);                   
        for (int j = 0; j < 75; j++)
        {
            GL.Color(Color.green);
            DrawOnePoint(Recognized[j * 2]-1, Recognized[j * 2 + 1]+1);
            DrawOnePoint(Recognized[j * 2]+1, Recognized[j * 2 + 1]+1);
            DrawOnePoint(Recognized[j * 2]+1, Recognized[j * 2 + 1]-1);
            DrawOnePoint(Recognized[j * 2]-1, Recognized[j * 2 + 1]-1);
        }
        GL.End();
    }

    //最好视频能全屏显示，否则很有可能对不上点
    private void DrawOnePoint(float x, float y)
    {
        GL.Vertex(new Vector3(1-x / 1280, y / 960, 0));     //1280为视频宽度，960为高度，1- 为镜像
    }
}
