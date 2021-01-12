//#define DebugLib

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class FaceunityWorker : MonoBehaviour
{
#if DebugLib
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    public const string nama_name = "CNamaSDK_UNITY_PLUGINd";
    public const string unity_plugin_name = "CNamaSDK_UNITY_PLUGINd";
#elif UNITY_IOS
    public const string nama_name = "__Internal";
    public const string unity_plugin_name = "__Internal";
#else
    public const string nama_name = "CNamaSDKd";
    public const string unity_plugin_name = "CNamaSDK_UNITY_PLUGINd";
#endif
#else
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    public const string nama_name = "CNamaSDK_UNITY_PLUGIN";
    public const string unity_plugin_name = "CNamaSDK_UNITY_PLUGIN";
#elif UNITY_IOS
    public const string nama_name = "__Internal";
    public const string unity_plugin_name = "__Internal";
#else
    public const string nama_name = "CNamaSDK";
    public const string unity_plugin_name = "CNamaSDK_UNITY_PLUGIN";
#endif
#endif

    public enum FaceUnityCoefficientDataType
    {
        _float = 0,
        _int = 1
    }

    //封装好的用于获取当前跟踪信息的类
    public class CFaceUnityCoefficientSet
    {
        public float[] m_data;  //跟踪数据，float类型，一旦实例化就不要改变
        public int[] m_data_int;    //跟踪数据，int类型，一旦实例化就不要改变
        public GCHandle m_handle;   //跟踪数据句柄，通过这个类向SDK内部传输指针
        public IntPtr m_addr;   //跟踪数据的指针
        public string m_name;   //跟踪数据的名字，SDK通过这个来判断返回哪种数据
        public int m_addr_size; //跟踪数据的长度，可变
        public int m_faceId = 0;    //跟踪的人脸ID
        public FaceUnityCoefficientDataType m_datatype = FaceUnityCoefficientDataType._float;  //0为float，1为int

        /**
   \brief 构造函数
   \param name 跟踪数据的名字，SDK通过这个来判断返回哪种数据
   \param size 跟踪数据的长度
   \param faceId 跟踪的人脸ID，默认值为0，第一个值为
   \param datatype 跟踪数据类型，有些为int有些为float，请参照相关文档设置，否则会出错，默认值为float
   \return 类实例
            */
        public CFaceUnityCoefficientSet(string name, int size, int faceId = 0, FaceUnityCoefficientDataType datatype = FaceUnityCoefficientDataType._float)
        {
            m_name = name;
            m_addr_size = size;
            m_faceId = faceId;
            m_datatype = datatype;

            if (m_datatype == FaceUnityCoefficientDataType._float)
            {
                m_data = new float[m_addr_size];
                m_handle = GCHandle.Alloc(m_data, GCHandleType.Pinned);
                m_addr = m_handle.AddrOfPinnedObject();
            }
            else if (m_datatype == FaceUnityCoefficientDataType._int)
            {
                m_data_int = new int[m_addr_size];
                m_handle = GCHandle.Alloc(m_data_int, GCHandleType.Pinned);
                m_addr = m_handle.AddrOfPinnedObject();
            }
            else
            {
                Debug.LogError("CFaceUnityCoefficientSet Error! Unknown datatype");
                return;
            }
        }
        /**
\brief 析构函数，GCHandle钉住的变量需要手动解除GC限制
\return 无
    */
        ~CFaceUnityCoefficientSet()
        {
            if (m_handle != null && m_handle.IsAllocated)
            {
                m_handle.Free();
                m_data = default(float[]);
                m_data_int = default(int[]);
            }
        }
        /**
\brief 需要逐帧，逐个跟踪信息调用，从而更新对应的数据
\return 无
    */
        public void Update()
        {
            fuGetFaceInfo(m_faceId, m_name, m_addr, m_addr_size);
        }
        /**
\brief 如果数据长度发生变化，需要调用一下这个函数
\param num 跟踪数据的长度
\return 无
    */
        public void Update(int num)
        {
            if (num != m_addr_size)
            {
                if (m_handle != null && m_handle.IsAllocated)
                {
                    m_handle.Free();
                }
                m_addr_size = num;
                if (m_datatype == FaceUnityCoefficientDataType._float)
                {
                    m_data = new float[m_addr_size];
                    m_handle = GCHandle.Alloc(m_data, GCHandleType.Pinned);
                    m_addr = m_handle.AddrOfPinnedObject();
                }
                else if (m_datatype == FaceUnityCoefficientDataType._int)
                {
                    m_data_int = new int[m_addr_size];
                    m_handle = GCHandle.Alloc(m_data_int, GCHandleType.Pinned);
                    m_addr = m_handle.AddrOfPinnedObject();
                }
                else
                    return;
            }
            Update();
        }
    }

    // Unity editor doesn't unload dlls after 'preview'

    #region UnityPlugin DllImport

    /////////////////////////////////////
    //native interfaces

    //详细的接口描述请查看API文档！！！

    /**
    \brief Initialize and authenticate your SDK instance to the FaceUnity server, must be called exactly once before all other functions.
        The buffers should NEVER be freed while the other functions are still being called.
        You can call this function multiple times to "switch pointers".
    \param v3buf should point to contents of the "v3.bin" we provide
    \param v3buf_sz should point to num-of-bytes of the "v3.bin" we provide
    \param licbuf is the pointer to the authentication data pack we provide. You must avoid storing the data in a file.
        Normally you can just `#include "authpack.h"` and put `g_auth_package` here.
    \param licbuf_sz is the authenticafu_Cleartion data size, we use plain int to avoid cross-language compilation issues.
        Normally you can just `#include "authpack.h"` and put `sizeof(g_auth_package)` here.
    \return non-zero for success, zero for failure
    */
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fu_Setup(IntPtr v3buf, int v3buf_sz, IntPtr licbuf, int licbuf_sz);


    /**
    \brief offline authentication
	    Initialize and authenticate your SDK instance to the FaceUnity server, must be called exactly once before all other functions.
	    The buffers should NEVER be freed while the other functions are still being called.
	    You can call this function multiple times to "switch pointers".
    \param v3buf should point to contents of the "v3.bin" we provide
    \param v3buf_sz should point to num-of-bytes of the "v3.bin" we provide
    \param licbuf is the pointer to the authentication data pack we provide. You must avoid storing the data in a file.
	    Normally you can just `#include "authpack.h"` and put `g_auth_package` here.
    \param licbuf_sz is the authentication data size, we use plain int to avoid cross-language compilation issues.
	    Normally you can just `#include "authpack.h"` and put `sizeof(g_auth_package)` here.
    \param offline_bundle_ptr is the pointer to offline bundle from FaceUnity server
    \param offline_bundle_sz is size of offline bundle
    \return non-zero for success, zero for failure
    */
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fu_SetupLocal(IntPtr v3buf, int v3buf_sz, IntPtr licbuf, int licbuf_sz, IntPtr offline_bundle_ptr, int offline_bundle_sz);


    /**
    \brief 鉴权真正运行完毕后调用这个接口得到对应结果
         fu_SetupLocal并不是运行完就立即执行鉴权的，要等GL.IssuePluginEvent(fu_GetRenderEventFunc(), 1);注册后在GL线程真正执行相应代码
         具体跟离线鉴权相关的信息请询问技术支持
    \param 通过这个指针保存返回的签名完毕的离线bundle，后续用着bundle即可不联网鉴权
    \param 离线bundle长度
    \return 0为鉴权失败，1为成功
    */
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fu_GetOfflineBundle(ref IntPtr offline_bundle_ptr, IntPtr offline_bundle_sz);

    /**
\brief Render a list of items on top of a GLES texture or a memory buffer.
    This function needs a GLES 2.0+ context. 
    Render will do in PluginEvent fu_GetRenderEventFunc
\param idxbuf points to the list of items
\param idxbuf_sz is the number of items
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fu_SetItemIds(IntPtr idxbuf, int idxbuf_sz, IntPtr mask);//mask can be null

    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fu_SetRuningMode(FURuningMode runningMode);//refer to FURuningMode

    public enum Nama_GL_Event_ID
    {
        FuSetup = 0,
        NormalRender = 1,
        ReleaseGLResources = 2,
        FuDestroyAllItems = 3,
    }
    //搭配GL.IssuePluginEvent使用
    //eventid代表的操作见Nama_GL_Event_ID
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr fu_GetRenderEventFunc();

#if !UNITY_IOS
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    private static extern void fu_RegisterDebugCallback(DebugCallback callback);
#endif

    /**
\brief Enable internal Log
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fu_EnableLog(bool isenable);
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fu_EnableLogConsole(bool isenable);

    /**
 * FURuningMode为FU_Mode_RenderItems的时候，加载EmptyItem.bytes，才能开启人脸跟踪。
 * FURuningMode为FU_Mode_TrackFace的时候，调用fu_SetTongueTracking(1)，才能开启舌头跟踪。注意，每次切换到FU_Mode_TrackFace之后都需要设置一次！！！
\brief Turn on or turn off Tongue Tracking, used in trackface.
\param enable > 0 means turning on, enable <= 0 means turning off
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fu_SetTongueTracking(int enable);

    /**
     * 翻转输入的纹理，仅对使用了natcam的安卓平台有效
     * natcam的安卓平台使用了SetDualInput，有些安卓平台nv21buf和tex的方向不一致，可以用这个接口设置tex的镜像。
     */
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetFlipTexMarkX(bool mark);

    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetFlipTexMarkY(bool mark);

    /**
\brief provide camera frame data
        flags: FU_ADM_FLAG
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetImage(IntPtr imgbuf, int flags, bool isbgra, int w, int h);

    /**
\brief provide camera frame data android nv21 and texture id
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetDualInput(IntPtr nv21buf, int texid, int flags, int w, int h);

    /**
\brief provide camera frame data android nv21,only support Android.
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetNV21Input(IntPtr nv21buf, int flags, int w, int h);

    /**
\brief provide camera frame data via texid
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SetImageTexId(int texid, int flags, int w, int h);

    /**
\brief get Rendered texture id, can be recreated in unity
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fu_GetNamaTextureId();

    /**
* SetUseNatCam(1);
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetUseNatCam(int enable);

    /**
* if true,Pause the render pipeline
*/
    [DllImport(unity_plugin_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetPauseRender(bool ifpause);

    #endregion

    #region Nama DllImport

    //------------------------------From Nama-------------------------------------

    public enum FULOGLEVEL
    {
        FU_LOG_LEVEL_TRACE = 0,     //调试日志，每帧多次
        FU_LOG_LEVEL_DEBUG = 1,     //调试日志，每帧一次或多次信息
        FU_LOG_LEVEL_INFO = 2,      //正常信息日志，程序运行过程中出现一次的信息，系统信息等
        FU_LOG_LEVEL_WARN = 3,      //警告级日志
        FU_LOG_LEVEL_ERROR = 4,     //错误级日志
        FU_LOG_LEVEL_CRITICAL = 5,  //错误且影响程序正常运行日志
        FU_LOG_LEVEL_OFF = 6        //关闭日志输出
    }
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuSetLogLevel(FULOGLEVEL level);
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern FULOGLEVEL fuGetLogLevel();
    /**
\brief open file log
\param file_fullname - nullptr for default terminal, non-null for log into file. 
\param max_file_size, max file size in byte. 
\param max_files, max file num for rotating log. 
\return zero for failed, one for success.
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuOpenFileLog([MarshalAs(UnmanagedType.LPStr)]string file_pullname, int max_file_size, int max_files);


    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuIsLibraryInit();

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuLoadTongueModel(IntPtr databuf, int databuf_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuSetTrackFaceAIType(FUAITYPE ai_type);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuLoadAIModelFromPackage(IntPtr databuf, int databuf_sz, FUAITYPE ai_type);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuReleaseAIModel(FUAITYPE ai_type);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuIsAIModelLoaded(FUAITYPE ai_type);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuCreateItemFromPackage(IntPtr databuf, int databuf_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuDestroyItem(int itemid);

    /**
\brief Destroy all internal data, resources, threads, etc.
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuDestroyLibData();

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuSetCropState(int state);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuSetCropFreePixel(int x0, int y0, int x1, int y1);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuSetFaceProcessorFov(float fov);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern float fuGetFaceProcessorFov();

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuSetDefaultRotationMode(int rotate_mode);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuGetCurrentRotationMode();

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuSetOutputResolution(int w, int h);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuFaceProcessorSetMinFaceRatio(float ratio);

    /**
\brief Get certificate permission code for modules
\param i - get i-th code, currently available for 0 and 1
\return The permission code
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuGetModuleCode(int i);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuSetMultiSamples(int samples);

    /**
\brief Create Tex For Item
\param item specifies the item
\param name is the tex name
\param value is the tex rgba buffer to be set ,use GCHandle to get ptr
\param width is the tex width
\param height is the tex height
\return zero for failure, non-zero for success
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuCreateTexForItem(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int width, int height);

    /**
\brief Delete Tex For Item
\param item specifies the item
\param name is the parameter name
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuDeleteTexForItem(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name);


    /**
 \brief Bind items to target item, target item act as a controller,target item
 should has 'OnBind' function, already bound items won't be unbound
 \param obj_handle is the target item handle
 \param p_items points to a list of item handles to be bound to the  target item
 \param n_items is the number of item handles in p_items
 \return the number of items newly bound to the avatar
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuBindItems(int obj_handle, IntPtr p_items, int n_items);

    /**
     \brief Unbind items from the target item
     \param obj_hand is the target item handle
     \param p_items points to a list of item handles to be unbound from the target
     item
     \param n_items is the number of item handles in p_items
     \return the number of items unbound from the target item
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuUnbindItems(int obj_handle, IntPtr p_items, int n_items);

    /**
     \brief Unbind all items from the target item
     \param obj_handle is the target item handle
     \return the number of items unbound from the target item
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuUnbindAllItems(int obj_handle);


    /**
\brief Set an item parameter to a double value
\param item specifies the item
\param name is the parameter name
\param value is the parameter value to be set
\return zero for failure, non-zero for success
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemSetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, double value);

    /**
\brief Set an item parameter to a string value
\param item specifies the item
\param name is the parameter name
\param value is the parameter value to be set
\return zero for failure, non-zero for success
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemSetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string value);

    /**
\brief Set an item parameter to a double array
\param item specifies the item
\param name is the parameter name
\param value points to an array of doubles
\param n specifies the number of elements in value
\return zero for failure, non-zero for success
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemSetParamdv(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemSetParamu8v(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemSetParamu64(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);

    /**
\brief Set an item parameter to a string value
\param item specifies the item
\param name is the parameter name
\param value is the parameter value to be set
\return zero for failure, non-zero for success
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern double fuItemGetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name);

    /**
\brief Get an item parameter as a string
\param item specifies the item
\param name is the parameter name
\param buf receives the string value
\param sz is the number of bytes available at buf
\return the length of the string value, or -1 if the parameter is not a string.
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemGetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]byte[] buf, int buf_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemGetParamdv(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemGetParamu8v(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);

    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuItemGetParamfv(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);

    /**
\brief Get the face tracking status
\return The number of valid faces currently being tracked
        //USE FaceunityWorker.instance.m_need_update_facenum INSTEAD!!!
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    private static extern int fuIsTracking();

    /**
\brief Set the maximum number of faces we track. The default value is 1.
\param n is the new maximum number of faces to track
\return The previous maximum number of faces tracked
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuSetMaxFaces(int num);

    /**
\brief Get Nama version string
\return Nama version string in const char*
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr fuGetVersion(); // Marshal.PtrToStringAnsi(fuGetVersion());

    /**
\brief Get system error, which causes system shutting down
\return System error code represents one or more errors	
	Error code can be checked against following bitmasks, non-zero result means certain error
	This interface is not a callback, needs to be called on every frame and check result, no cost
	Inside authentication error (NAMA_ERROR_BITMASK_AUTHENTICATION), meanings for each error code are listed below:
	1 failed to seed the RNG
	2 failed to parse the CA cert
	3 failed to connect to the server
	4 failed to configure TLS
	5 failed to parse the client cert
	6 failed to parse the client key
	7 failed to setup TLS
	8 failed to setup the server hostname
	9 TLS handshake failed
	10 TLS verification failed
	11 failed to send the request
	12 failed to read the response
	13 bad authentication response
	14 incomplete authentication palette info
	15 not inited yet
	16 failed to create a thread
	17 authentication package rejected
	18 void authentication data
	19 bad authentication package
	20 certificate expired
	21 invalid certificate
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuGetSystemError();

    /**
\brief Interpret system error code
\param code - System error code returned by fuGetSystemError()
\return One error message from the code
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr fuGetSystemErrorString(int code); // Marshal.PtrToStringAnsi();

    /**
\brief Call this function to reset the face tracker on camera switches
*/
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern void fuOnCameraChange();

    /**
  \brief Get the unique identifier for each face during current tracking
    Lost face tracking will change the identifier, even for a quick retrack
  \param face_id is the id of face, index is smaller than which is set in fuSetMaxFaces
    If this face_id is x, it means x-th face currently tracking
  \return the unique identifier for each face
        //USE FaceunityWorker.instance.face_true_id INSTEAD!!!
  */
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    private static extern int fuGetFaceIdentifier(int face_id);



    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    private static extern int fuGetFaceInfo(int face_id, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr ret, int szret);



    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fuHumanProcessorGetNumResults();
    [DllImport(nama_name, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr fuHumanProcessorGetResultJoint3ds(int index, int[] size);



    #endregion

    public enum FU_ADM_FLAG
    {
        FU_ADM_FLAG_NONE = 0,
        FU_ADM_FLAG_EXTERNAL_OES_TEXTURE = 1,
        FU_ADM_FLAG_ENABLE_READBACK = 2,
        FU_ADM_FLAG_NV21_TEXTURE = 4,
        FU_ADM_FLAG_I420_TEXTURE = 8,
        FU_ADM_FLAG_I420_BUFFER = 16,
        FU_ADM_FLAG_FLIP_X = 32,    //翻转只翻转道具渲染，并不会翻转整个图像
        FU_ADM_FLAG_FLIP_Y = 64,
        FU_ADM_FLAG_RGBA_BUFFER = 128
    };

    public enum FURuningMode
    {
        FU_Mode_None = 0,
        FU_Mode_RenderItems, //face tracking and render item (beautify is one type of item) ,item means 'daoju'
        FU_Mode_Beautification,//non face tracking, beautification only.
        FU_Mode_Masked,//when tracking multi-people, different perple　can use different item, give mask in function fu_SetItemIds  
        FU_Mode_TrackFace//tracking face only, get face infomation, but do not render item.it's very fast.
    };

    public static FURuningMode currentMode = FURuningMode.FU_Mode_None;

    public static void SetRunningMode(FURuningMode mode)
    {
        currentMode = mode;
        fu_SetRuningMode(mode);
    }

    public enum FUAITYPE
    {
        FUAITYPE_NONE = 0,
        FUAITYPE_BACKGROUNDSEGMENTATION = 1 << 1,
        FUAITYPE_HAIRSEGMENTATION = 1 << 2,
        FUAITYPE_HANDGESTURE = 1 << 3,
        FUAITYPE_TONGUETRACKING = 1 << 4,
        FUAITYPE_FACELANDMARKS75 = 1 << 5,
        FUAITYPE_FACELANDMARKS209 = 1 << 6,
        FUAITYPE_FACELANDMARKS239 = 1 << 7,
        FUAITYPE_HUMANPOSE2D = 1 << 8,
        FUAITYPE_BACKGROUNDSEGMENTATION_GREEN = 1 << 9,
        FUAITYPE_FACEPROCESSOR = 1 << 10,
        FUAITYPE_FACEPROCESSOR_FACECAPTURE = 1 << 11,
        FUAITYPE_FACEPROCESSOR_FACECAPTURE_TONGUETRACKING = 1 << 12,
        FUAITYPE_FACEPROCESSOR_HAIRSEGMENTATION = 1 << 13,
        FUAITYPE_FACEPROCESSOR_HEADSEGMENTATION = 1 << 14,
        FUAITYPE_FACEPROCESSOR_EXPRESSION_RECOGNIZER = 1 << 15,
        FUAITYPE_HUMAN_PROCESSOR = 1 << 16,
        FUAITYPE_HUMAN_PROCESSOR_DETECT = 1 << 17,
        FUAITYPE_HUMAN_PROCESSOR_2D_SELFIE = 1 << 18,
        FUAITYPE_HUMAN_PROCESSOR_2D_DANCE = 1 << 19,
        FUAITYPE_HUMAN_PROCESSOR_2D_SLIM = 1 << 20,
        FUAITYPE_HUMAN_PROCESSOR_3D_SELFIE = 1 << 21,
        FUAITYPE_HUMAN_PROCESSOR_3D_DANCE = 1 << 22,
        FUAITYPE_HUMAN_PROCESSOR_SEGMENTATION = 1 << 23
    }

    public enum FUAI_CAMERA_VIEW
    {
        ROT_0 = 0,
        ROT_90 = 1,
        ROT_180 = 2,
        ROT_270 = 3,
    }

    public static string FixRotationWithAcceleration(Vector3 g, bool ifMirrored = false)
    {
        FUAI_CAMERA_VIEW v = FUAI_CAMERA_VIEW.ROT_0;
        string s = "";
        if (Mathf.Abs(g.x) > Mathf.Abs(g.y))
        {
            if (g.x > 0.5f)
            {
                s = "90";
                v = FUAI_CAMERA_VIEW.ROT_90;
            }
            else if (g.x < -0.5f)
            {
                s = "270";
                v = FUAI_CAMERA_VIEW.ROT_270;
            }
            else
            {
                s = "Default 0";
                v = FUAI_CAMERA_VIEW.ROT_0;
            }
        }
        else
        {
            if (g.y > 0.5f)
            {
                s = "180";
                v = FUAI_CAMERA_VIEW.ROT_180;
            }
            else if (g.y < -0.5f)
            {
                s = "0";
                v = FUAI_CAMERA_VIEW.ROT_0;
            }
            else
            {
                s = "Default 0";
                v = FUAI_CAMERA_VIEW.ROT_0;
            }
        }
        s += "\nSet " + FixRotation(ifMirrored, v);
        return s;
    }

    public static FUAI_CAMERA_VIEW currentEyeViewRot = FUAI_CAMERA_VIEW.ROT_0;

    public static string FixRotation(bool ifMirrored = false, FUAI_CAMERA_VIEW eyeViewRot = FUAI_CAMERA_VIEW.ROT_0)
    {
        currentEyeViewRot = eyeViewRot;
        var rot = FUAI_CAMERA_VIEW.ROT_0;
#if UNITY_EDITOR || UNITY_STANDALONE
        rot = FUAI_CAMERA_VIEW.ROT_180;
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
        if (ifMirrored)
        {
            switch (eyeViewRot)
            {
                case FUAI_CAMERA_VIEW.ROT_0:
                    rot = FUAI_CAMERA_VIEW.ROT_270;
                    break;
                case FUAI_CAMERA_VIEW.ROT_90:
                    rot = FUAI_CAMERA_VIEW.ROT_180;
                    break;
                case FUAI_CAMERA_VIEW.ROT_180:
                    rot = FUAI_CAMERA_VIEW.ROT_90;
                    break;
                case FUAI_CAMERA_VIEW.ROT_270:
                    rot = FUAI_CAMERA_VIEW.ROT_0;
                    break;
            }
        }
        else
        {
            switch (eyeViewRot)
            {
                case FUAI_CAMERA_VIEW.ROT_0:
                    rot = FUAI_CAMERA_VIEW.ROT_90;
                    break;
                case FUAI_CAMERA_VIEW.ROT_90:
                    rot = FUAI_CAMERA_VIEW.ROT_180;
                    break;
                case FUAI_CAMERA_VIEW.ROT_180:
                    rot = FUAI_CAMERA_VIEW.ROT_270;
                    break;
                case FUAI_CAMERA_VIEW.ROT_270:
                    rot = FUAI_CAMERA_VIEW.ROT_0;
                    break;
            }
        }
#endif
#if !UNITY_EDITOR && UNITY_IOS
        switch (eyeViewRot)
        {
            case FUAI_CAMERA_VIEW.ROT_0:
                rot = FUAI_CAMERA_VIEW.ROT_180;
                break;
            case FUAI_CAMERA_VIEW.ROT_90:
                rot = FUAI_CAMERA_VIEW.ROT_270;
                break;
            case FUAI_CAMERA_VIEW.ROT_180:
                rot = FUAI_CAMERA_VIEW.ROT_0;
                break;
            case FUAI_CAMERA_VIEW.ROT_270:
                rot = FUAI_CAMERA_VIEW.ROT_90;
                break;
        }
#endif
        fuSetDefaultRotationMode((int)rot);

        string s = "";
        switch (rot)
        {
            case FUAI_CAMERA_VIEW.ROT_0:
                s = "0";
                break;
            case FUAI_CAMERA_VIEW.ROT_90:
                s = "90";
                break;
            case FUAI_CAMERA_VIEW.ROT_180:
                s = "180";
                break;
            case FUAI_CAMERA_VIEW.ROT_270:
                s = "270";
                break;
        }
        return s;
    }

    ///////////////////////////////
    //public int m_camera_native_texid = 0;
    //singleton checks
    public static FaceunityWorker instance = null;
    void Awake()
    {
        //singleton checks
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }
    ///////////////////////////////
    //persistent data, DO NOT EVER FREE ANY OF THESE!
    //we must keep the GC handles to keep the arrays pinned to the same addresses
    [HideInInspector]
    public bool m_plugin_inited = false;

    public int MAXFACE = 1;
    public bool EnableExpressionLoop = true;
    int MaxExpression = 0;
    public string LICENSE = "";

    public string OfflineBundleName = "";

    public enum SETUPMODE
    {
        Normal,
        Local,
    }

    public SETUPMODE SetupMode = SETUPMODE.Normal;
    public bool EnableGetNamaSystemError = false;
    public bool EnableNamaLogToConsole = false;
    public bool EnableNamaLogToFile = false;
    public FULOGLEVEL loglevel = FULOGLEVEL.FU_LOG_LEVEL_OFF;

    [HideInInspector]
    public int m_need_update_facenum = 0;
    [HideInInspector]
    public List<int> face_true_id = new List<int>();
    public List<CFaceUnityCoefficientSet> m_translation = new List<CFaceUnityCoefficientSet>();//("translation", 3); //3D translation of face in camera space - 3 float
    public List<CFaceUnityCoefficientSet> m_rotation = new List<CFaceUnityCoefficientSet>();//("rotation", 4); //rotation quaternion - 4 float
    //public List<CFaceUnityCoefficientSet> m_expression = new List<CFaceUnityCoefficientSet>();//("expression", 46);
    public List<CFaceUnityCoefficientSet> m_expression_with_tongue = new List<CFaceUnityCoefficientSet>();//("expression_with_tongue", 56);
    //public List<CFaceUnityCoefficientSet> m_landmarks = new List<CFaceUnityCoefficientSet>();//("landmarks",75*2); //2D landmarks coordinates in image space - 75*2 float
    //public List<CFaceUnityCoefficientSet> m_landmarks_ar = new List<CFaceUnityCoefficientSet>();//("landmarks_ar",75*3); //3D landmarks coordinates in camera space - 75*3 float
    //public List<CFaceUnityCoefficientSet> m_face_rect = new List<CFaceUnityCoefficientSet>();//("face_rect",4); //the rectangle of tracked face in image space, (xmin,ymin,xmax,ymax) - 4 float
    public List<CFaceUnityCoefficientSet> m_pupil_pos = new List<CFaceUnityCoefficientSet>();//("pupil_pos", 2);
    public List<CFaceUnityCoefficientSet> m_eye_rotation = new List<CFaceUnityCoefficientSet>();//("eye_rotation", 4);

    //public List<CFaceUnityCoefficientSet> m_armesh_vertex_num = new List<CFaceUnityCoefficientSet>();
    //public List<CFaceUnityCoefficientSet> m_armesh_vertices = new List<CFaceUnityCoefficientSet>();
    //public List<CFaceUnityCoefficientSet> m_armesh_uvs = new List<CFaceUnityCoefficientSet>();
    //public List<CFaceUnityCoefficientSet> m_armesh_face_num = new List<CFaceUnityCoefficientSet>();
    //public List<CFaceUnityCoefficientSet> m_armesh_faces = new List<CFaceUnityCoefficientSet>();
    //public List<CFaceUnityCoefficientSet> m_armesh_trans_mat = new List<CFaceUnityCoefficientSet>();

    public static event EventHandler OnInitOK;
    private delegate void DebugCallback(string message);

    ///////////////////////////////
    /**
\brief 初始化所有跟踪信息
\param maxface 最多几张脸
\return 无
        */
    void InitCFaceUnityCoefficientSet(int maxface)
    {
        if (MaxExpression < maxface)
            for (int i = MaxExpression; i < maxface; i++)
            {
                m_translation.Add(new CFaceUnityCoefficientSet("translation_origin", 3, i));
                m_rotation.Add(new CFaceUnityCoefficientSet("rotation_origin", 4, i));
                //m_expression.Add(new CFaceUnityCoefficientSet("expression", 46, i));
                m_expression_with_tongue.Add(new CFaceUnityCoefficientSet("expression_with_tongue", 56, i));
                m_pupil_pos.Add(new CFaceUnityCoefficientSet("pupil_pos", 2, i));
                m_eye_rotation.Add(new CFaceUnityCoefficientSet("eye_rotation", 4, i));
                //m_landmarks.Add(new CFaceUnityCoefficientSet("landmarks", 75 * 2, i));

                //m_armesh_vertex_num.Add(new CFaceUnityCoefficientSet("armesh_vertex_num", 1, i, FaceUnityCoefficientDataType._int));
                //m_armesh_vertices.Add(new CFaceUnityCoefficientSet("armesh_vertices", 1, i));   //这个长度值需要动态更新
                //m_armesh_uvs.Add(new CFaceUnityCoefficientSet("armesh_uvs", 1, i));   //这个长度值需要动态更新
                //m_armesh_face_num.Add(new CFaceUnityCoefficientSet("armesh_face_num", 1, i, FaceUnityCoefficientDataType._int));
                //m_armesh_faces.Add(new CFaceUnityCoefficientSet("armesh_faces", 1, i, FaceUnityCoefficientDataType._int));    //这个长度值需要动态更新
                //m_armesh_trans_mat.Add(new CFaceUnityCoefficientSet("armesh_faces", 16, i));
            }
        else if (MaxExpression > maxface)
            for (int i = maxface; i < MaxExpression; i++)
            {
                m_translation.RemoveAt(i);
                m_rotation.RemoveAt(i);
                //m_expression.RemoveAt(i);
                m_expression_with_tongue.RemoveAt(i);
                m_pupil_pos.RemoveAt(i);
                m_eye_rotation.RemoveAt(i);
                //m_landmarks.RemoveAt(i);

                //m_armesh_vertex_num.RemoveAt(i);
                //m_armesh_vertices.RemoveAt(i);
                //m_armesh_uvs.RemoveAt(i);
                //m_armesh_face_num.RemoveAt(i);
                //m_armesh_faces.RemoveAt(i);
                //m_armesh_trans_mat.RemoveAt(i);
            }
        MaxExpression = maxface;
    }

    /**
\brief 初始化SDK并设置部分参数，同时开启驱动SDK渲染的GL循环协程
\return 无
    */
    IEnumerator Start()
    {
        if (EnvironmentCheck())
        {

//#if UNITY_EDITOR && !UNITY_IOS
//            fu_RegisterDebugCallback(new DebugCallback(DebugMethod));
//#endif
            fu_EnableLog(false);

            fuSetLogLevel(loglevel);
            if (EnableNamaLogToConsole)
            {
                fu_EnableLogConsole(true);
            }
            if (EnableNamaLogToFile)
            {
                //windows上用这个方法才能看到log
                var logpath = Application.persistentDataPath + "/Log/log.txt";
                fuOpenFileLog(logpath, 10000000, 10);
                Debug.LogFormat("fuOpenFileLog logpath = {0}", logpath);
            }

            Debug.Log("fuIsLibraryInit:   " + fuIsLibraryInit());
            if (m_plugin_inited == false)
            {
                Debug.LogFormat("FaceunityWorker Init");
                
                //fu_Setup init nama sdk
                if (fuIsLibraryInit() == 0)  //防止Editor下二次Play导致崩溃的bug
                {
                    //load license data
                    if (LICENSE == null || LICENSE == "")
                    {
                        Debug.LogError("LICENSE is null! please paste the license data to the TextField named \"LICENSE\" in FaceunityWorker");
                        yield break;
                    }
                    else
                    {
                        sbyte[] m_licdata_bytes;
                        string[] sbytes = LICENSE.Split(',');
                        if (sbytes.Length <= 7)
                        {
                            Debug.LogError("License Format Error");
                            yield break;
                        }
                        else
                        {
                            m_licdata_bytes = new sbyte[sbytes.Length];
                            Debug.LogFormat("length:{0}", sbytes.Length);
                            for (int i = 0; i < sbytes.Length; i++)
                            {
                                //Debug.Log(sbytes[i]);
                                m_licdata_bytes[i] = sbyte.Parse(sbytes[i]);
                                //Debug.Log(m_licdata_bytes[i]);
                            }
                            GCHandle m_licdata_handle = GCHandle.Alloc(m_licdata_bytes, GCHandleType.Pinned);
                            IntPtr licptr = m_licdata_handle.AddrOfPinnedObject();

                            /* Deprecated  */
                            //load nama sdk data
                            //string fnv3 = Util.GetStreamingAssetsPath() + "/faceunity/v3.bytes";
                            //WWW v3data = new WWW(fnv3);
                            //yield return v3data;
                            //byte[] m_v3data_bytes = v3data.bytes;
                            //GCHandle m_v3data_handle = GCHandle.Alloc(m_v3data_bytes, GCHandleType.Pinned); //pinned avoid GC
                            //IntPtr v3ptr = m_v3data_handle.AddrOfPinnedObject(); //pinned addr

                            if (SetupMode == SETUPMODE.Normal)
                            {
                                fu_Setup(IntPtr.Zero, 0, licptr, sbytes.Length); //要查看license是否有效请打开插件log（fu_EnableLog(true);）

                                GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.FuSetup);

                                while (fuIsLibraryInit() == 0)
                                {
                                    Debug.Log("(SetupMode Normal)Waiting for LibraryInit...");
                                    yield return Util._endOfFrame;
                                }

                                Debug.Log("(SetupMode Normal)fu_Setup Finished!");
                            }
                            else if (SetupMode == SETUPMODE.Local)
                            {
                                byte[] offlinebundledata_bytes = null;
                                bool loaclAuthMode = true;

                                string offlinebundle_path = Application.persistentDataPath + "/Bundles/"; //这个路径可能无法读取，请自行指定可以读写的路径！
                                string offlinebundle_name = "signed_" + OfflineBundleName;

                                offlinebundledata_bytes = Util.ReadBytesFile(offlinebundle_path, offlinebundle_name);

                                if (offlinebundledata_bytes == null || offlinebundledata_bytes.Length == 0)
                                {
                                    Debug.LogFormat("can not find offlinebundle_signed:{0}{1}", offlinebundle_path, offlinebundle_name);
                                    string offlinebundle_unsigned = Util.GetStreamingAssetsPath() + "/faceunity/" + OfflineBundleName;
                                    WWW offlinebundledata_unsigned = new WWW(offlinebundle_unsigned);
                                    yield return offlinebundledata_unsigned;
                                    offlinebundledata_bytes = offlinebundledata_unsigned.bytes;
                                    if (offlinebundledata_bytes == null || offlinebundledata_bytes.Length == 0)
                                    {
                                        Debug.LogErrorFormat("can not find offlinebundle_unsigned:{0}", offlinebundle_unsigned);
                                        yield break;
                                    }
                                    loaclAuthMode = false;
                                }

                                GCHandle m_offlinebundle_handle = GCHandle.Alloc(offlinebundledata_bytes, GCHandleType.Pinned);
                                IntPtr fptrset = m_offlinebundle_handle.AddrOfPinnedObject();

                                fu_SetupLocal(IntPtr.Zero, 0, licptr, sbytes.Length, fptrset, offlinebundledata_bytes.Length);

                                GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.FuSetup);

                                while (fuIsLibraryInit() == 0)
                                {
                                    Debug.Log("(SetupMode Local)Waiting for LibraryInit...");
                                    yield return Util._endOfFrame;
                                }

                                if (m_offlinebundle_handle.IsAllocated)
                                    m_offlinebundle_handle.Free();

                                int message = fuGetSystemError();
                                Debug.LogFormat("Init Message:{0},{1},loaclAuthMode={2}", message, Marshal.PtrToStringAnsi(fuGetSystemErrorString(message)), loaclAuthMode);

                                IntPtr fptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
                                IntPtr iptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));

                                int isAuthed = fu_GetOfflineBundle(ref fptr, iptr);

                                if (isAuthed == 0)
                                {
                                    Debug.LogError("Auth Faild!");
                                    yield break;
                                }

                                if (!loaclAuthMode)
                                {
                                    int arrSize = Marshal.ReadInt32(iptr);
                                    byte[] fpa = new byte[arrSize];
                                    Marshal.Copy(fptr, fpa, 0, fpa.Length);
                                    //不能释放fptr和iptr，由Nama自行管理，否则会崩溃
                                    Util.SaveBytesFile(fpa, offlinebundle_path, offlinebundle_name);
                                }
                                Debug.Log("(SetupMode Local)fu_Setup Finished!");
                            }

                            if (m_licdata_handle.IsAllocated)
                                m_licdata_handle.Free();
                            //if (m_v3data_handle.IsAllocated)
                            //    m_v3data_handle.Free();
                            
                            //yield return LoadAIBundle("ai_bgseg.bytes", FUAITYPE.FUAITYPE_BACKGROUNDSEGMENTATION);
                            //yield return LoadAIBundle("ai_bgseg_green.bytes", FUAITYPE.FUAITYPE_BACKGROUNDSEGMENTATION_GREEN);
                            //yield return LoadAIBundle("ai_hairseg.bytes", FUAITYPE.FUAITYPE_HAIRSEGMENTATION); 
#if UNITY_EDITOR || UNITY_STANDALONE
                            yield return LoadAIBundle("ai_face_processor_pc.bytes", FUAITYPE.FUAITYPE_FACEPROCESSOR);
                            yield return LoadAIBundle("ai_hand_processor_pc.bytes", FUAITYPE.FUAITYPE_HANDGESTURE);
                            yield return LoadAIBundle("ai_human_processor_pc.bytes", FUAITYPE.FUAITYPE_HUMAN_PROCESSOR);
#else
                            yield return LoadAIBundle("ai_face_processor.bytes", FUAITYPE.FUAITYPE_FACEPROCESSOR);
                            yield return LoadAIBundle("ai_hand_processor.bytes", FUAITYPE.FUAITYPE_HANDGESTURE);
                            yield return LoadAIBundle("ai_human_processor.bytes", FUAITYPE.FUAITYPE_HUMAN_PROCESSOR);
#endif
                            yield return LoadTongueBundle("tongue.bytes");

                            m_plugin_inited = true;
                        }
                    }
                }
                else
                {
                    GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.ReleaseGLResources);  //清理残余，防止崩溃
                    m_plugin_inited = true;
                }

                if (m_plugin_inited == true)
                {
                    //fu_SetMultiSamples(4);
                    SetRunningMode(FURuningMode.FU_Mode_RenderItems);   //默认模式，随时可以改
                    SetUseNatCam(1);  //默认选项，仅安卓有效

                    //Loom.Initialize();

                    if (OnInitOK != null)
                        OnInitOK(this, null);//触发初始化完成事件

                    var errorcode = fuGetSystemError();
                    if (errorcode != 0)
                        Debug.LogErrorFormat("errorcode:{0}, {1}", errorcode, Marshal.PtrToStringAnsi(fuGetSystemErrorString(errorcode)));
                    Debug.Log("Nama Version:" + Marshal.PtrToStringAnsi(fuGetVersion()));

                    yield return CallPluginAtEndOfFrames();
                }
            }
        }
        else
        {
            Debug.LogError("please check your Graphics API,this plugin only supports OpenGL!");
            yield break;
        }
    }

    /**\brief SDK渲染的GL循环协程，每一个Unity生命周期的末尾，调用GL.IssuePluginEvent使Unity执行SDK内部的渲染代码，同时获取并保存跟踪信息\return 无    */
    private IEnumerator CallPluginAtEndOfFrames()
    {
        while (true)
        {
            yield return Util._endOfFrame;
            ////////////////////////////////
            fuSetMaxFaces(MAXFACE);
            GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.NormalRender);// cal for sdk render

            if (EnableGetNamaSystemError)
            {
                var errorcode = fuGetSystemError();
                if (errorcode != 0)
                    Debug.LogErrorFormat("errorcode:{0}, {1}", errorcode, Marshal.PtrToStringAnsi(fuGetSystemErrorString(errorcode)));
            }

            int num = fuIsTracking();
            m_need_update_facenum = num < MAXFACE ? num : MAXFACE;

            if (EnableExpressionLoop)
            {
                if (MaxExpression != MAXFACE)
                    InitCFaceUnityCoefficientSet(MAXFACE);
                //only update other stuff when there is new data
                face_true_id.Clear();
                for (int i = 0; i < m_need_update_facenum; i++)
                {
                    //m_armesh_vertex_num[i].Update();
                    //m_armesh_vertices[i].Update(m_armesh_vertex_num[i].m_data_int[0] * 3);
                    //m_armesh_uvs[i].Update(m_armesh_vertex_num[i].m_data_int[0] * 2);
                    //m_armesh_face_num[i].Update();
                    //m_armesh_faces[i].Update(m_armesh_face_num[i].m_data_int[0] * 3);
                    //m_armesh_trans_mat[i].Update();

                    m_translation[i].Update();
                    m_rotation[i].Update();
                    //m_landmarks[i].Update();
                    m_pupil_pos[i].Update();
                    m_eye_rotation[i].Update();
                    m_expression_with_tongue[i].Update();
                    //m_expression[i].Update();

                    //后处理
                    float[] E = m_expression_with_tongue[i].m_data;
                    //float[] E = m_expression[i].m_data;
                    float[] PP = m_pupil_pos[i].m_data;
                    E[6] = E[7] = PP[0];
                    E[10] = E[11] = -PP[0];
                    E[12] = E[13] = PP[1];
                    E[4] = E[5] = -PP[1];

                    //trueid与faceid之分：faceid为0~currentMaxface，不会区分不同人脸，而trueid为真正的人脸ID，会区分不同人脸
                    //通过faceid获取trueid
                    face_true_id.Add((int)Mathf.Log(fuGetFaceIdentifier(i), 2));
                }
            }
        }
    }





    /**\brief 用来注册SDK的LOG回调，SDK中间层可以用这个来在Unity内部打log\param message 要打的log\return 无    */
    private void DebugMethod(string message)
    {
        Debug.Log("From Dll: " + message);
    }


    private IEnumerator LoadAIBundle(string name, FUAITYPE type)
    {
        if (fuIsAIModelLoaded(type) == 0)
        {
            string bundle = Util.GetStreamingAssetsPath() + "/faceunity/" + name;
            WWW bundledata = new WWW(bundle);
            yield return bundledata;
            byte[] bundle_bytes = bundledata.bytes;
            var bundle_handle = GCHandle.Alloc(bundle_bytes, GCHandleType.Pinned);
            IntPtr bundledataptr = bundle_handle.AddrOfPinnedObject();

            fuLoadAIModelFromPackage(bundledataptr, bundle_bytes.Length, type);

            bundle_handle.Free();
        }
    }

    private IEnumerator LoadTongueBundle(string name)
    {
        string bundle = Util.GetStreamingAssetsPath() + "/faceunity/" + name;
        WWW bundledata = new WWW(bundle);
        yield return bundledata;
        byte[] bundle_bytes = bundledata.bytes;
        var bundle_handle = GCHandle.Alloc(bundle_bytes, GCHandleType.Pinned);
        IntPtr bundledataptr = bundle_handle.AddrOfPinnedObject();

        fuLoadTongueModel(bundledataptr, bundle_bytes.Length);

        bundle_handle.Free();
    }


    /**\brief 应用退出是清理相关GCHandle和SDK相关数据\return 无*/
    private void OnApplicationQuit()
    {
        GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.ReleaseGLResources);
    }

    /**\brief 检测当前渲染环境是否符合要求，本SDK仅支持在OpenGL下运行\return true为检测通过，false为不通过*/
    bool EnvironmentCheck()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore
            || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGL2)
            return true;
        else
            return false;
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore
            || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGL2)
            return true;
        else
            return false;
#elif UNITY_ANDROID
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3
            || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2)
            return true;
        else
            return false;
#elif UNITY_IOS
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3
            || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2)
            return true;
        else
            return false;
#endif
    }
}
