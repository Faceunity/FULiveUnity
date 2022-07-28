/*
这个组件用于眼球的跟踪，仅旋转，放在眼球的mesh上，SDK输出Quaternion应用在眼球上
！！！仅供参考，没有考虑效率问题和易用性问题！！！
*/
using UnityEngine;

public class EyeController : MonoBehaviour
{
    private Quaternion m_rotation0;                                                       //眼球初始旋转
    private Vector3 m_position0;                                                          //眼球初始位置
    private SkinnedMeshRenderer smr;                                                      //眼球的Render
    private Vector3 m_center = Vector3.zero;
    public int faceid = 0;                                                                //人脸ID

    /// <summary>
    /// 初始化时记录原始信息
    /// </summary>
    void Awake()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        if (smr) m_center = smr.localBounds.center;
        m_rotation0 = transform.localRotation;
        m_position0 = transform.localPosition;
    }

    void Start()
    {
        //skinnedMeshRenderer.enabled = false;
    }

    /// <summary>
    /// 每帧更新眼球transform
    /// </summary>
    void Update()
    {
        if (FaceunityWorker.Instance == null || FaceunityWorker.fuIsLibraryInit() == 0)
            return;
        if (faceid >= FaceunityWorker.Instance.m_need_update_headnum)
            return;
        if (FaceunityWorker.Instance.m_need_update_headnum > 0)                   //仅在跟踪到人脸的情况下更新
        {
            //skinnedMeshRenderer.enabled = true;
        }
        else
        {
            //skinnedMeshRenderer.enabled = false;
            return;
        }

        transform.localRotation = m_rotation0;
        transform.localPosition = m_position0;

        var R = FaceunityWorker.Instance.m_eye_rotation[faceid].m_data;
        RotateAround(transform, m_center, PostProcessRotation(new Quaternion(R[0], R[1], R[2], R[3])));
    }

    private Quaternion PostProcessRotation(Quaternion r)
    {
        return new Quaternion(r.x, -r.y, r.z, r.w);
    }

    private void RotateAround(Transform t, Vector3 center, Quaternion rot)
    {
        Vector3 pos = t.localPosition;
        Vector3 dir = pos - center;                                               // find current direction relative to center
        dir = rot * dir;                                                          // rotate the direction
        t.localPosition = center + dir;                                           // define new position
                                                                                  // rotate object to keep looking at the center:
        Quaternion myRot = t.localRotation;
        t.localRotation *= Quaternion.Inverse(myRot) * rot * myRot;
    }

    public void ResetTransform()
    {
        transform.localRotation= m_rotation0;
        transform.localPosition= m_position0;
    }
}
