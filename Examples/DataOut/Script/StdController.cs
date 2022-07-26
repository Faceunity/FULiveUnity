using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class StdController : MonoBehaviour
{
    //这个组件用于人脸的跟踪，包括位移，旋转和表情系数（BlengShape），放在人脸的mesh上
    //！！！仅供参考，没有考虑效率问题和易用性问题！！！

   public RenderToModel rtm;

   private Quaternion m_rotation0;                                                       //人脸初始旋转
   private Vector3 m_position0;                                                          //人脸初始位置

    /////////////////////////////////////
    //unity blendshape
    public SkinnedMeshRenderer[] skinnedMeshRenderers;                                   //人脸的Render，用来设置表情系数
    bool pauseUpdate = false;                                                            //暂停更新

    public int faceid = 0;                                                               //人脸ID

    public int limitBSNum = -1;


    //初始化时记录原始信息
    void Awake()
    {
        m_rotation0 = transform.localRotation;
        m_position0 = transform.position;
        //rtm.on_switch_camera += OnSwitchCamera;
    }

    //相机切换完成回调
    void Start()
    {
        //skinnedMeshRenderer.enabled = false;
    }

    //切换相机时暂停更新，防止乱跑
    void OnSwitchCamera(bool isSwitching)
    {
        pauseUpdate = isSwitching;
    }

    //每帧更新人脸信息
    void Update()
    {
        if (pauseUpdate)
            return;
        if (FaceunityWorker.Instance == null || FaceunityWorker.fuIsLibraryInit() == 0)
            return;
        if (faceid >= FaceunityWorker.Instance.m_need_update_headnum)
        {
            return;
        }
        if (FaceunityWorker.Instance.m_need_update_headnum > 0)    //仅在跟踪到人脸的情况下更新
        {
            //skinnedMeshRenderer.enabled = true;
        }
        else
        {
            //skinnedMeshRenderer.enabled = false;
            return;
        }

        float[] R = FaceunityWorker.Instance.m_rotation[faceid].m_data; //人脸旋转数据
        float[] P = FaceunityWorker.Instance.m_translation[faceid].m_data;  //人脸位移数据
        float[] E = FaceunityWorker.Instance.m_expression_with_tongue[faceid].m_data; //人脸表情数据
        var pE = PostProcessExpression(E);

        for (int j = 0; j < skinnedMeshRenderers.Length; j++)
        {
            int l = limitBSNum > 0 ? Math.Min(limitBSNum, skinnedMeshRenderers[j].sharedMesh.blendShapeCount) :
                skinnedMeshRenderers[j].sharedMesh.blendShapeCount;
            for (int i = 0; i < l; i++)
            {
                skinnedMeshRenderers[j].SetBlendShapeWeight(i, pE[i] * 100);
            }
        }
        transform.localRotation = m_rotation0 * PostProcessRotation(new Quaternion(R[0], R[1], R[2], R[3]));   //坐标系转换
        if (rtm.if_track_pos == true)
            transform.position = PostProcessPositon(new Vector3(P[0], P[1], P[2]));
        else
            transform.position = m_position0;

        //Debug.Log("STDUpdate:localRotation="+ transform.localEulerAngles.x+","+ transform.localEulerAngles.y + "," + transform.localEulerAngles.z);
    }

    float[] PostProcessExpression(float[] E)
    {
        return E;
    }
    Quaternion PostProcessRotation(Quaternion r)
    {

        return new Quaternion(-r.x, -r.y, r.z, r.w);
    }
    Vector3 PostProcessPositon(Vector3 p)
    {

        return new Vector3(-p.x, p.y, p.z);

    }

    //重置人脸的位置旋转
    public void ResetTransform()
    {
        transform.localRotation = m_rotation0;
        transform.position = m_position0;
        //Debug.Log("ResetTransform:localRotation=" + transform.localEulerAngles.x + "," + transform.localEulerAngles.y + "," + transform.localEulerAngles.z);
    }

    public void ResetBlendShape()
    {
        for (int j = 0; j < skinnedMeshRenderers.Length; j++)
        {
            int l = skinnedMeshRenderers[j].sharedMesh.blendShapeCount;
            for (int i = 0; i < l; i++)
            {
                skinnedMeshRenderers[j].SetBlendShapeWeight(i, 0);
            }
        }
    }
}