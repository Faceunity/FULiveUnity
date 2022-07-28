# Unity Nama C# API 参考文档
级别：Public
更新日期：2021-11-15
SDK版本: v.8.0.0

------

## 最新更新内容：

2021-11-15 v8.0.0:

- 美颜磨皮新增均匀磨皮类型，可用于高端机，五官保护更加清晰，磨皮更加均匀。
- 美型原有功能优化，及新增小脸功能。  
- 优化祛黑眼圈法令纹效果。  
- 优化美妆口红、眉毛、睫毛、美瞳等功能。  
- 优化人脸检测，提高检出率。  
- 优化PC端背景分割效果，分割更加准确  

2021-07-08 v7.4.1:

1. 优化高分辨美颜磨皮效果，相同尺度上对齐低分辨率  
2. 修复人像分割贴纸人体和贴纸没有同时出现的问题  
3. 修复道具切换过程中，偶现人脸检测重置问题。  

2021-04-16 v7.4.0:

1. 优化2D人体点位和美体性能。  
2. 优化人像分割效果和性能。优化手臂和手识别不稳定问题，优化背景误识别问题。修复人像分割偏移问题。  
3. 优化美妆效果。优化美瞳贴合效果和溢色问题；优化唇妆遮挡效果，遮挡时口红不再显现。  
4. 优化Animoji面部驱动效果。优化小幅度动作，如小幅度张嘴和眨眼时，驱动效果更加灵敏。  
5. 优化情绪识别，支持8种基本情绪识。  
6. 新增接口fuSetUseAsyncAIInference，支持异步模式，开启异步模式，帧率提升，可改善客户在一些低端设备上帧率不足问题。  
7. 新增fuRender接口，为所有业务统一渲染接口，详见接口定义。  
8. 新增接口 fuSetInputCameraBufferMatrix，fuSetInputCameraBufferMatrixState，fuSetInputCameraTextureMatrix，fuSetInputCameraTextureMatrixState，fuSetOutputMatrix，fuSetOutputMatrixState，用于设置图像转换矩阵，用于调整输出图像方向，详见接口定义。  

2020-12-29 v7.3.0:

- 优化美妆性能，和V7.2比，标准美妆Android端帧率提升29%，iOS端帧率提升17%；标准美颜+标准美妆，集成入第三方推流1小时后，在低端机上帧率高于15fps，可流畅运行。
- 优化美体性能，和V7.2比，性能显著提升，Android端帧率提升26%，CPU降低32%；iOS端帧率提升11%，CPU降低46%，内存降低45%。
- 优化背景分割性能，和V7.2比，性能显著提升，Android端帧率提升64%，CPU降低25%；iOS端帧率提升41%，CPU降低47%，内存降低44%。
- 优化美体功能效果，优化大幅度运动时，头部和肩部位置附近物体变形幅度大的问题；人体在画面中出现消失时过渡更自然；遮挡情况美体效果更加稳定，不会有高频持续抖动情况。
- 优化表情识别功能，提高识别准确性，共能识别17种表情动作。
- 优化绿幕抠像效果，提高边缘准确度。
- 优化人脸表情跟踪驱动效果，优化首帧检测模型显示较慢问题，加强细微表情跟踪，优化人脸转动时模型明显变小问题。
- 优化全身Avatar跟踪驱动效果，针对做连续高频大幅度运动的情况，如跳舞等场景，整体模型稳定性，尤其手臂稳定性提升，抖动情况显著改善。
- 优化美颜亮眼下眼睑溢色问题。
- 新增人脸拖拽变形功能，可使用FUCreator 2.1.0进行变形效果编辑。
- 新增美颜美型模块瘦圆眼功能，效果为使眼睛整体放大，尤其是纵向放大明显。
- 新增支持手势回调接口fuSetHandGestureCallBack，详见接口文档。
- 新增AI能力，表情识别，AITYPE为FUAITYPE_FACEPROCESSOR_EXPRESSION_RECOGNIZER

2020-09-24 v7.2.0:

- 新增绿幕抠像功能，支持替换图片、视频背景等，详见绿幕抠像功能文档。
- 美颜模块新增瘦颧骨、瘦下颌骨功能。
- 优化美颜性能以及功耗，优化集成入第三方推流服务时易发热掉帧问题。
- 优化手势识别功能的效果以及性能，提升识别稳定性和手势跟随性效果，优化手势识别时cpu占有率。
- 优化PC版各个功能性能，帧率提升显著。美发、美体、背景分割帧率提升30%以上，美颜、Animoji、美妆、手势等功能也有10%以上的帧率提升。
- 优化包增量，SDK分为lite版，和全功能版本。lite版体积更小，包含人脸相关的功能(海报换脸除外)。
- 优化人脸跟踪稳定性，提升贴纸的稳定性。
- 提供独立核心算法SDK，接口文档详见算法SDK文档([FUAI_C_API_参考文档.md](./FUAI_C_API_参考文档.md))。
- fuGetFaceInfo接口新增三个参数，分别为：舌头方向(tongue_direction)，表情识别(expression_type)，头部旋转信息欧拉角参数(rotation_euler)。
- 新增fuOnDeviceLostSafe函数，详见接口文档。
- 新增fuSetFaceProcessorDetectMode函数，人脸识别跟踪区分图片模式和视频模式，详见接口文档。
- 新增人体动作识别动作定义文档([人体动作识别文档.md](../resource/docs/人体动作识别文档.md))。
- 新增ai_hand_processor.bundle，替代ai_gesture.bundle，提供手势识别跟踪能力。

2020-7-29 v7.1.0:

1. 新增美颜锐化功能，见美颜参数文档。
2. 优化美颜磨皮效果，保留更多的高频细节。

2020-7-24 v7.0.1:

1. 新增接口fuHumanProcessorSetBonemap
2. 新增接口fuHumanProcessorGetResultTransformArray
3. 新增接口fuHumanProcessorGetResultModelMatrix
4. 修复fuGetSestemError问题。
5. 修复fuSetMaxFaces，在释放AI模型后，设置失效问题。
6. 修复Android非高通机型，OES输入问题。
7. 修复美妆远距离嘴部黑框问题。
8. 修复美体美颜共存不支持问题。

2020-6-30 v7.0.0:

1. 新增人体算法能力接口，包括人体检测、2D人体关键点（全身、半身）、人体3D骨骼（全身、半身）、手势识别、人像mask、头发mask、头部mask、动作识别等能力。
2. 新增接口，详见接口说明
   - fuGetLogLevel,获取当前日志级别。
     - fuSetLogLevel,设置当前日志级别。
     - fuOpenFileLog,打开文件日志，默认使用console日志。
     - fuFaceProcessorSetMinFaceRatio，设置人脸检测距离的接口。
     - fuSetTrackFaceAIType，设置fuTrackFace算法运行类型接口。
     - fuSetCropState，设置裁剪状态。
     - fuSetCropFreePixel，设置自由裁剪参数。
     - fuSetFaceProcessorFov，设置FaceProcessor人脸算法模块跟踪fov。
     - fuGetFaceProcessorFov，获取FaceProcessor人脸算法模块跟踪fov。
     - fuHumanProcessorReset，重置HumanProcessor人体算法模块状态。
     - fuHumanProcessorSetMaxHumans，设置HumanProcessor人体算法模块跟踪人体数。
     - fuHumanProcessorGetNumResults，获取HumanProcessor人体算法模块跟踪人体数。
     - fuHumanProcessorGetResultTrackId，获取HumanProcessor人体算法模块跟踪Id。
     - fuHumanProcessorGetResultRect，获取HumanProcessor人体算法模块跟踪人体框。
     - fuHumanProcessorGetResultJoint2ds，获取HumanProcessor人体算法模块跟踪人体2D关键点。
     - fuHumanProcessorGetResultJoint3ds，获取HumanProcessor人体算法模块跟踪人体3D关键点。
     - fuHumanProcessorSetBonemap，设置HumanProcessor人体算法模块，3D骨骼拓扑结构信息。
     - fuHumanProcessorGetResultTransformArray， 获取HumanProcessor人体算法模块跟踪人体3D骨骼信息。
     - fuHumanProcessorGetResultModelMatrix， 获取HumanProcessor人体算法模块跟踪人体3D骨骼，根节点模型变化矩阵。
   
     - fuHumanProcessorGetResultHumanMask，获取HumanProcessor人体算法模块全身mask。
     - fuHumanProcessorGetResultActionType，获取HumanProcessor人体算法模块跟踪人体动作类型。
     - fuHumanProcessorGetResultActionScore，获取HumanProcessor人体算法模块跟踪人体动作置信度。
     - fuFaceProcessorGetResultHairMask，获取HumanProcessor人体算法模块头发mask。
     - fuFaceProcessorGetResultHeadMask，获取HumanProcessor人体算法模块头部mask。
     - fuHandDetectorGetResultNumHands，获取HandGesture手势算法模块跟踪手势数量。
     - fuHandDetectorGetResultHandRect，获取HandGesture手势算法模块跟踪手势框。
     - fuHandDetectorGetResultGestureType，获取HandGesture手势算法模块跟踪手势类别。
     - fuHandDetectorGetResultHandScore，获取HandGesture手势算法模块跟踪手势置信度。
3. 废弃接口
   - fuSetStrictTracking
   - fuSetASYNCTrackFace
   - fuSetFaceTrackParam  
   - fuSetDeviceOrientation  
   - fuSetDefaultOrientation

2020-03-19 v6.7.0:

1. 优化6.6.0版本表情系数的灵活度，Animoji表情跟踪更加灵活。  
2. 新增接口 fuIsLibraryInit，检测SDK是否已初始化。  
3. AI能力模型中人脸相关能力合并为一体。将FUAITYPE::FUAITYPE_FACELANDMARKS75,FUAITYPE_FACELANDMARKS209,FUAITYPE_FACELANDMARKS239,FUAITYPE_FACEPROCESSOR统一合并到FUAITYPE_FACEPROCESSOR。美颜美妆道具中"landmarks_type"参数关闭，由系统自动切换。  
4. 优化美颜模块：  
	- 新增去黑眼圈、去法令纹功能。  
	- 优化美颜美型效果。  
	- 美颜磨皮效果优化，新增支持仅磨人脸区域功能。  
5. 新增接口支持图像裁剪，可用于瘦脸边缘变形裁剪。详见fuSetCropState，fuSetCropFreePixel接口。
6. 优化美妆效果，人脸点位优化，提高准确性。 
	- 优化口红点位与效果，解决张嘴、正脸、低抬头、左右转头、抿嘴动作的口红溢色。
	- 优化美瞳点位效果，美瞳效果更准确。
	- 腮红效果优化，解决了仰头角度下腮红强拉扯问题。

2020-01-19 v6.6.0:

注意: 更新SDK 6.6.0时，在fuSetup之后，需要马上调用 fuLoadAIModelFromPackage 加载ai_faceprocessor.bundle 到 FUAITYPE::FUAITYPE_FACEPROCESSOR!!!

在Nama 6.6.0及以上，AI能力的调用会按道具需求调用，避免同一帧多次调用；同时由Nama AI子系统管理推理，简化调用过程；将能力和产品功能进行拆分，避免在道具bundle内的冗余AI模型资源，方便维护升级，同时加快道具的加载；方便各新旧AI能力集成，后续的升级迭代。

基本逻辑：Nama初始化后，可以预先加载一个或多个将来可能使用到的AI能力模块。调用实时render处理接口时，Nama主pipe会在最开始的时候，分析当前全部道具需要AI能力，然后由AI子系统执行相关能力推理，然后开始调用各个道具的‘生命周期’函数，各道具只需要按需在特定的‘生命周期’函数调用JS接口获取AI推理的结果即可，并用于相关逻辑处理或渲染。

1. 新增加接口 fuLoadAIModelFromPackage 用于加载AI能力模型。
2. 新增加接口 fuReleaseAIModel 用于释放AI能力模型。
3. 新增加接口 fuIsAIModelLoaded 判断AI能力是否已经加载。
4. 新增fuSetMultiSamples接口，MSAA抗锯齿接口，解决虚拟形象等内容边缘锯齿问题。

例子1：背景分割
	a. 加载AI能力模型，fuLoadAIModelFromPackage加载ai_bgseg.bundle 到 FUAITYPE::FUAITYPE_BACKGROUNDSEGMENTATION上。
	b. 加载产品业务道具A，A道具使用了背景分割能力。
	c. 切换产品业务道具B，B道具同样使用了背景分割能力，但这时AI能力不需要重新加载。

2019-09-25 v6.4.0:

- v6.4.0 接口无变动。

2019-08-14 v6.3.0:

- 新增fu_SetFaceTrackParam函数，用于设置人脸跟踪参数。

2019-06-27 v6.2.0:

- fu_SetFaceDetParam函数增加可设置参数。

2019-05-27 v6.1.0:

- 新增fu_SetupLocal函数，支持离线鉴权。
- 新增fu_DestroyLibData函数，支持tracker内存释放。

2019-04-28 v6.0.0:

- 新增fu_SetFaceDetParam函数，用于设置人脸检测参数。
- 更新了fu_Setup函数。

------
### 目录：
本文档内容目录：

[TOC]

------
### 1. 简介 
本文是相芯人脸跟踪及视频特效开发包(Nama SDK)的底层接口文档。该文档中的 Nama API 为 FaceunityWorker.cs 中的接口，可以直接用于 Unity 上的开发。其中，部分Unity演示场景中也提供了一些二次封装好的接口，相比本文中的接口会更贴近实际的开发。

Unity使用的SDK是在原始的Nama SDK上封装了一层GL调用层，这个封装使得Nama SDK的GL环境得以和Unity的GL环境同步，具体可以 [参考这里](https://docs.unity3d.com/Manual/NativePluginInterface.html)。

以下文字中原始Nama SDK简称**Nama**，GL调用层简称**UnityPlugin**，合称**UnityNamaSDK**。

Nama所有渲染功能要求在Unity的GL线程中执行。为了同步Nama和Unity的GL环境，部分接口调用后不会立即生效，只是暂时把参数存储在UnityPlugin层中，等到代码逻辑执行GL.IssuePluginEvent指令后，Unity的GL线程真正调用Nama渲染指令后，先前部分接口设置的参数才会生效，这种接口在以下描述中会备注**延迟**生效。

Nama7.0以后利用GL.IssuePluginEvent的eventID参数分开了Nama的各种渲染调用目前有：

```c#
public enum Nama_GL_Event_ID
    {
        FuSetup = 0,	//Nama初始化
        NormalRender = 1,	//Nama渲染
        ReleaseGLResources = 2,	//释放道具以外的所有GL资源
        FuDestroyAllItems = 3,	//释放所有道具的GL资源
    }
//这样调用
GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.NormalRender);
```

Nama7.0后为了简化UnityPlugin逻辑，部分不需要在UnityPlugin层中缓存数据的接口改为直接从Nama中调用，这类接口特征就是以**fu_**开始，如，fu_IsLibraryInit，现在这类接口统一改成**fu**开始，如fuIsLibraryInit，dllimport也选择Nama的名字。

如果需要用到SDK的绘制功能，则需要Unity开启OpenGL渲染模式，没有开启或开启不正确会导致崩溃。我们对OpenGL的环境要求为 GLES 2.0 以上。具体调用方式，可以参考FULiveUnity Demo。如果不需要使用SDK的绘制功能，可以咨询技术支持如何直接调用Nama。

底层接口根据作用逻辑归为五类：初始化、主运行接口、加载销毁道具、功能接口、C#端辅助接口。

------
### 2. APIs
#### 2.1 初始化
##### fu_Setup 函数
初始化系统环境，加载系统数据，并进行网络鉴权。必须在调用UnityNamaSDK其他接口前执行，否则会引发崩溃。

```c#
public static extern int fu_Setup(IntPtr v3buf, int v3buf_sz, IntPtr licbuf, int licbuf_sz);
```

__参数:__

*v3buf*: v3.bytes中读取的二进制数据的指针，已弃用，传NULL

*v3buf_sz*:  v3.bytes的长度，已弃用，传0

*licbuf*：证书的文本数据，用`,`分割，将分割后的数组数据转成sbyte格式

*licbuf_sz[in]*：sbyte数组长度

__返回值:__

返回0代表失败。返回1代表准备执行中，是否成功请在运行GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.FuSetup) 后循环调用fuIsLibraryInit()来获取是否成功的信息。如初始化失败，可以通过 ```fuGetSystemError``` 获取错误代码。

__备注:__

这个接口会**延迟**生效。

---

##### fu_SetupLocal 函数

功能和fu_Setup类似，但是用于离线鉴权。

这个接口的原理是，首次鉴权时输入一个设备独有的原始签名bundle，这次鉴权要求有网络连接，签名完毕可以通过fu_GetOfflineBundle获取签名后的bundle，如果签名成功，则后续鉴权只需要使用这个bundle代替原始离线bundle，输入fu_SetupLocal即可实现离线鉴权。

更详细的的信息请咨询技术支持。

```c#
public static extern int fu_SetupLocal(IntPtr v3buf, int v3buf_sz, IntPtr licbuf, int licbuf_sz, IntPtr offline_bundle_ptr, int offline_bundle_sz);
```

__参数:__

*v3buf*: v3.bytes中读取的二进制数据的指针，已弃用，传NULL

*v3buf_sz*:  v3.bytes的长度，已弃用，传0

*licbuf*：证书的文本数据，用`,`分割，将分割后的数组数据转成sbyte格式

*licbuf_sz[in]*：sbyte数组长度

offline_bundle_ptr：原始离线bundle的指针

offline_bundle_sz：原始离线bundle的长度

__返回值:__

返回0代表失败。返回1代表准备执行中，是否成功请在运行GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.FuSetup) 后循环调用fuIsLibraryInit()来获取是否成功的信息。如初始化失败，可以通过 ```fuGetSystemError``` 获取错误代码。

__备注:__

这个接口会**延迟**生效。

---

##### fu_GetOfflineBundle 函数

返回签名完毕的离线bundle（不一定签名成功）。

```c#
public static extern int fu_GetOfflineBundle(ref IntPtr offline_bundle_ptr, IntPtr offline_bundle_sz);
```

__参数:__

*offline_bundle_ptr*: 离线bundle的指针

*offline_bundle_sz*:  离线bundle的长度

__返回值:__

离线鉴权是否成功

__备注:__

这个接口会**立即**生效。

------

##### fuIsLibraryInit 函数

返回值表示UnityNamaSDK初始化是否成功。

```c#
public static extern int fuIsLibraryInit();
```

__参数:__

无

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuLoadTongueModel 函数

加载舌头跟踪需要的数据文件

```c#
public static extern int fuLoadTongueModel(byte[] databuf, int databuf_sz);
```

__参数:__

*databuf*:  tongue.bytes中读取的二进制数据的数组
*databuf_sz*:  tongue.bytes的长度

__返回值:__

0为未加载，1为加载。

__备注:__

这个接口会**立即**生效。

这个接口即将被废弃，fuLoadAIModelFromPackage加载ai_face_processor.bytes时已经带有新版舌头跟踪数据，使用新版接口时不需要使用本接口

------
##### fuSetTrackFaceAIType  函数
设置fuTrackFace算法运行类型接口
```C
public static extern void fuSetTrackFaceAIType(FUAITYPE ai_type);
```
__参数:__  

*ai_type [in]*：aitype，详见FUAITYPE定义。

```c#
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
    FUAITYPE_FACEPROCESSOR_EMOTION_RECOGNIZER = 1 << 16,
    FUAITYPE_FACEPROCESSOR_DISNEYGAN = 1 << 17,
    FUAITYPE_HUMAN_PROCESSOR = 1 << 18,
    FUAITYPE_HUMAN_PROCESSOR_DETECT = 1 << 19,
    FUAITYPE_HUMAN_PROCESSOR_2D_SELFIE = 1 << 20,
    FUAITYPE_HUMAN_PROCESSOR_2D_DANCE = 1 << 21,
    FUAITYPE_HUMAN_PROCESSOR_2D_SLIM = 1 << 22,
    FUAITYPE_HUMAN_PROCESSOR_3D_SELFIE = 1 << 23,
    FUAITYPE_HUMAN_PROCESSOR_3D_DANCE = 1 << 24,
    FUAITYPE_HUMAN_PROCESSOR_SEGMENTATION = 1 << 25,
    FUAITYPE_FACE_RECOGNIZER = 1 << 26
}
```

__备注:__ 
这个接口会**立即**生效。

------

##### fuLoadAIModelFromPackage 函数

加载AI运算需要的数据文件

```c#
public static extern int fuLoadAIModelFromPackage(byte[] databuf, int databuf_sz, FUAITYPE ai_type);
```

__参数:__

*databuf*:  AI数据文件中读取的二进制数据的数组
*databuf_sz*:  数组的长度
*ai_type*:  AI数据文件类型，详见上文

__返回值:__

0为未加载，1为加载。

__备注:__

这个接口会**立即**生效。

AI能力会随SDK一起发布，存放在Assets\StreamingAssets\faceunity目录中。

__注意:__ 桌面版(windows, mac)请使用xxx_pc.bytes , 这是针对pc进行性能优化版本。


- ai_face_processor.bytes 为人脸特征点、表情跟踪以及头发mask、头部maskAI能力模型，需要默认加载，对应FUAITYPE_FACEPROCESSOR。
- ai_hand_processor.bytes  为手势识别AI能力模型，对应FUAITYPE_HANDGESTURE。。
- ai_human_processor.bytes 为人体算法能力模型，包括人体检测、2D人体关键点（全身、半身）、人体3D骨骼（全身、半身）、人像mask、动作识别等能力，对应FUAITYPE_HUMAN_PROCESSOR。

------

##### fuReleaseAIModel 函数

卸载AI运算需要的数据文件

```c#
public static extern int fuReleaseAIModel(FUAITYPE ai_type);
```

__参数:__

*ai_type*:  AI数据文件类型，详见上文

__返回值:__

1为已释放，0为未释放。

__备注:__

这个接口会**立即**生效。

------

##### fuIsAIModelLoaded 函数

查询对应类型的AI运算数据文件是否已加载

```c#
public static extern int fuIsAIModelLoaded(FUAITYPE ai_type);
```

__参数:__

*ai_type*:  AI数据文件类型，详见上文

__返回值:__

0为未加载，否则为已加载

__备注:__

这个接口会**立即**生效。

------

##### fuDestroyLibData 函数

特殊函数，当不再需要Nama SDK时，可以释放由 ```fu_Setup```初始化所分配的人脸跟踪模块的内存，约30M左右。调用后，人脸跟踪以及道具绘制功能将失效，相关函数将失败。如需使用，需要重新调用 ```fu_Setup```进行初始化。

```c#
 public static extern void fuDestroyLibData();
```

__参数:__

无

__返回值:__

无

__备注:__

这个接口会**立即**生效。

---



#### 2.2 主运行接口

##### SetImage 函数

图像数据输入接口之一。输入RGBA格式的buffer数组，通用性最强，各平台均可使用。

```c#
 public static extern int SetImage(IntPtr imgbuf,int flags, bool isbgra, int w, int h);
```

__参数:__

*imgbuf*: RGBA格式的buffer数组指针
*flags*: 请输入0
*isbgra*: 0表示RGBA格式，1表示BGRA格式
*w*: 图像宽
*h*: 图像高

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

------

##### SetYUVInput函数

图像数据输入接口之一。

```C#
 public static extern int SetYUVInput(IntPtr yuvbuf, int flags, int w, int h);
```

**参数：**

*yuvbuf*: yuv格式的buffer数组指针
*flags*: 请输入0
*w*: 图像宽
*h*: 图像高

------

##### SetDualInput 函数

图像数据输入接口之一。同时输入NV21格式的buffer数组和纹理ID，通常在安卓设备上，通过原生相机插件获取相应数据来使用，效率最高。

```c#
 public static extern int SetDualInput(IntPtr nv21buf, int texid, int flags, int w, int h);
```

__参数:__

*nv21buf*: NV21格式的buffer数组指针
*texid*: RGBA格式的纹理ID
*flags*: 请输入0
*w*: 图像宽
*h*: 图像高

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

------

##### SetNV21Input 函数

图像数据输入接口之一。输入NV21格式的buffer数组，通常在安卓设备上，通过原生相机插件获取相应数据来使用。

```c#
 public static extern int SetNV21Input(IntPtr nv21buf, int flags, int w, int h);
```

__参数:__

*nv21buf*: NV21格式的buffer数组指针
*flags*: 请输入0
*w*: 图像宽
*h*: 图像高

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

------

##### SetImageTexId 函数

图像数据输入接口之一。输入GL纹理ID，某些特殊GL环境下无法使用，但是一定程度上性能高于Image。

```c#
 public static extern int SetImageTexId(int texid, int flags, int w, int h);
```

__参数:__

*texid*: RGBA格式的纹理ID
*flags*: 请输入0
*w*: 图像宽
*h*: 图像高

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

---

##### fu_SetRuningMode 函数

设置UnityNamaSDK的运行模式

```c#
public static extern void fu_SetRuningMode(FU_RUNNING_MODE runningMode);

public enum FU_RUNNING_MODE
{
    FU_RUNNING_MODE_UNKNOWN = 0,
    FU_RUNNING_MODE_RENDERITEMS, //face tracking and render item (beautify is one type of item) ,item means '道具'
    FU_RUNNING_MODE_BEAUTIFICATION,//non face tracking, beautification only.
    FU_RUNNING_MODE_TRACK//tracking face only then get face infomation, without render item. it's very fast.
};
```

__参数:__

*runningMode*: 运行模式

```
- FU_RUNNING_MODE_UNKNOWN：停止渲染
- FU_RUNNING_MODE_RENDERITEMS：开启人脸跟踪和渲染道具
- FU_RUNNING_MODE_BEAUTIFICATION：只开启美颜
- FU_RUNNING_MODE_TRACK：只开启人脸跟踪，速度最快
```

__返回值:__

无

__备注:__

这个接口会**延迟**生效。

------

##### fu_GetRenderEventFunc 函数

**SDK调用的核心逻辑** 这个函数返回的值配合GL.IssuePluginEvent即可运行SDK主逻辑，具体原因请[参考这里](https://docs.unity3d.com/Manual/NativePluginInterface.html) 

Nama7.0以后利用GL.IssuePluginEvent的eventID参数分开了Nama的各种渲染调用

```c#
public static extern IntPtr fu_GetRenderEventFunc();

public enum Nama_GL_Event_ID
    {
        FuSetup = 0,
        NormalRender = 1,
        ReleaseGLResources = 2,
        FuDestroyAllItems = 3,
    }
GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.FuSetup)	//调用示例
```

__参数:__

无

__返回值:__

SDK运行函数的指针

__备注:__

这个接口会**立即**生效。

---

##### fu_GetNamaTextureId 函数

获取本插件渲染完毕的图像的纹理ID。

```c#
 public static extern int fu_GetNamaTextureId();
```

__参数:__

无

__返回值:__

GL纹理ID

__备注:__

这个接口会**立即**生效。

---

##### fuGetFaceInfo 函数

获取人脸跟踪信息。

```c#
 public static extern int fuGetFaceInfo(int face_id, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr ret, int szret);
```

__参数:__

*face_id*: 当前第几张人脸
*name*: 需要获取的参数名字
*ret*: 用于接收数据的数组的指针
*szret*: 用于接收数据的数组的长度

__返回值:__

返回 1 代表获取成功，信息通过 ret 返回。返回 0 代表获取失败。

__备注:__

这个接口会**立即**生效。
这个接口的具体调用方法和可获取参数名请参考FaceunityWorker.cs中的相关代码。

---

##### fuGetFaceInfoRotated 函数

获取设置了（fuSetInputCameraTextureMatrix/fuSetInputCameraBufferMatrix）后的人脸跟踪信息。

```c#
 public static extern int fuGetFaceInfo(int face_id, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr ret, int szret);
```

__参数:__

*face_id*: 当前第几张人脸
*name*: 需要获取的参数名字
*ret*: 用于接收数据的数组的指针
*szret*: 用于接收数据的数组的长度

__返回值:__

返回 1 代表获取成功，信息通过 ret 返回。返回 0 代表获取失败。

__备注:__

这个接口会**立即**生效。
这个接口的具体调用方法和可获取参数名请参考FaceunityWorker.cs中的相关代码。

------

##### fu_SetTongueTracking 函数

 FURuningMode为FU_Mode_RenderItems的时候，加载aitype.bytes，才能开启舌头跟踪。
 FURuningMode为FU_Mode_TrackFace的时候，调用fu_SetTongueTracking(1)，才能开启舌头跟踪。注意，每次切换到FU_Mode_TrackFace之后都需要设置一次！！！

```c#
 public static extern int fu_SetTongueTracking(int enable);
```

__参数:__

*enable*: 0表示关闭，1表示开启

__返回值:__

返回 1 代表成功，返回 0 代表失败。

__备注:__

这个接口会**延迟**生效。

---

##### ~~fu_OnDeviceLost 函数~~

已弃用

重置Nama的GL渲染环境

```c#
 public static extern void fu_OnDeviceLost();
```

__参数:__

无

__返回值:__

无

__备注:__

这个接口会**立即**生效。

用这个代替：

```c#
GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.ReleaseGLResources);
```

------

##### fuOnCameraChange 函数

重置Nama中的人脸跟踪功能（不涉及GL）

```c#
 public static extern void fuOnCameraChange();
```

__参数:__

无

__返回值:__

无

__备注:__

这个接口会**立即**生效。

---

#### 2.3 加载销毁道具

##### fuCreateItemFromPackage 函数

这个接口用于加载UnityNamaSDK所适配的道具文件，如美颜，贴纸，Animoji等等

```c#
public static extern int fuCreateItemFromPackage(IntPtr databuf, int databuf_sz);
```

__参数:__

*databuf*:  道具文件中读取的二进制数据的指针
*databuf_sz*:  道具文件的长度

__返回值:__

一个整数句柄，作为该道具在系统内的标识符。

__备注:__

这个接口会**立即**生效。

这个函数可以用多线程调用以降低主线程被卡住的风险

------

##### fuDestroyItem 函数

销毁指定道具

```c#
public static extern void fuDestroyItem(int itemid);
```

__参数:__

*itemid*:  道具加载后返回的ItemID

__返回值:__

无

__备注:__

这个接口会**立即**生效。

------

##### ~~fu_DestroyAllItems 函数~~

已弃用

销毁当前所有加载的道具

```c#
public static extern void fu_DestroyAllItems();
```

__参数:__

无

__返回值:__

无

__备注:__

这个接口会**立即**生效。

用这个代替：

```c#
GL.IssuePluginEvent(fu_GetRenderEventFunc(), (int)Nama_GL_Event_ID.ReleaseGLResources);
```

------

##### fu_SetItemIds 函数

当加载完道具后，道具不会立即生效开始渲染，而是要通过这个接口输入要渲染的道具的ItemID来开启对应道具的渲染，如果不输入相应ItemID，对应的道具就不会渲染。

```c#
public static extern int fu_SetItemIds(int[] p_items, int n_items, int[] p_masks);
```

__参数:__

*p_items*:  所有需要渲染的道具的ItemID组成的数组
*n_items*:  数组的长度
*p_masks*:  多人脸多道具时每个人脸用不同的道具，可以为空，但是不为空时要和p_items等长！

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

---

##### fuBindItems 函数

将某几个道具绑定到另一个道具上

```c#
public static extern int fuBindItems(int itemid, int[] p_items, int n_items);
```

__参数:__

*itemid*: 目标道具的ID
*p_items*: 需要绑定的道具ID集合数组
*n_items*:数组长度

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

---

##### fuUnbindItems 函数

从某个道具上解绑一些道具

```c#
public static extern int fuUnbindItems(int itemid, int[] p_items, int n_items);
```

__参数:__

*itemid*: 目标道具的ID
*p_items*: 需要解绑的道具ID集合数组
*n_items*:数组长度

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

---

##### fuUnbindItems 函数

从某个道具上解绑所有道具

```c#
public static extern int fuUnbindAllItems(int itemid);
```

__参数:__

*itemid*: 目标道具的ID

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuItemSetParamd 函数

给道具设置参数（double）

```c#
public static extern int fuItemSetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, double value);
```

__参数:__

*itemid*: 需要设置参数的道具ID
*name*: 参数的名字
*value*: 参数

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuItemSetParams 函数

给道具设置参数（string）

```c#
public static extern int fuItemSetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string value);
```

__参数:__

*itemid*: 需要设置参数的道具ID
*name*: 参数的名字
*value*: 参数

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuItemSetParamdv 函数

给道具设置参数（double数组）

```c#
public static extern int fuItemSetParamdv(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);
```

__参数:__

*itemid*: 需要设置参数的道具ID
*name*: 参数的名字
*value*: 参数指针
*value_sz*: 参数长度

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuItemGetParamd 函数

获取指定道具的某个参数（double）

```c#
public static extern double fuItemGetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name);
```

__参数:__

*itemid*: 需要获取参数的道具ID
*name*: 参数的名字

__返回值:__

需要获取的参数

__备注:__

这个接口会**立即**生效。

------

##### fuItemGetParams 函数

获取指定道具的某个参数（string）

```c#
public static extern int fuItemGetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]byte[] buf, int buf_sz);
```

__参数:__

*itemid*: 需要获取参数的道具ID
*name*: 参数的名字
*buf*: 参数指针
*buf_sz*: 参数的长度

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuItemGetParamdv 函数

获取指定道具的某个参数（double数组）

```c#
public static extern int fuItemGetParamdv(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);
```

__参数:__

*itemid*: 需要获取参数的道具ID
*name*: 参数的名字
*buf*: 参数指针
*buf_sz*: 参数的长度

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

---

##### fuCreateTexForItem 函数

给道具设置纹理

```c#
public static extern int fuCreateTexForItem(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int width, int height);
```

__参数:__

*itemid*: 需要设置纹理的道具ID
*name*: 需要设置的纹理的名字
*value*: RGBA格式的纹理的buffer的指针
*width*: 纹理宽
*height*: 纹理高

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuDeleteTexForItem 函数

销毁指定道具的某个纹理

```c#
public static extern int fuDeleteTexForItem(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name);
```

__参数:__

*itemid*: 需要销毁纹理的道具ID
*name*: 需要销毁的纹理的名字

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

#### 2.4 功能接口

##### fuGetModuleCode 函数

获取证书鉴权结果，总共64bit的标志位。

```c#
public static extern int fuGetModuleCode(int i);
```

__参数:__

*i*: 输入0获取前32bit，输入1获取后32bit

__返回值:__

鉴权结果标志位。

__备注:__

这个接口会**立即**生效。

------

##### fuSetDefaultRotationMode 函数

设置默认的RotationMode，即默认的渲染方向

```c#
public static extern void fuSetDefaultRotationMode(FU_ROTATION_MODE rotate_mode);

public enum FU_ROTATION_MODE
{
    ROT_0 = 0,
    ROT_90 = 1,
    ROT_180 = 2,
    ROT_270 = 3,
}
```

__参数:__

*rotate_mode*:  默认的渲染方向，相对于输入数据方向

__返回值:__

无

__备注:__

这个接口会**立即**生效。

------

##### fuGetCurrentRotationMode 函数

获取默认的RotationMode，即默认的渲染方向

```c#
public static extern FU_ROTATION_MODE fuGetCurrentRotationMode();
```

__参数:__

无

__返回值:__

默认的RotationMode，即默认的渲染方向

__备注:__

这个接口会**立即**生效。

------

##### ~~fuSetASYNCTrackFace 函数~~

已弃用

开启异步跟踪功能，某些机型可以性能会提升，但是某些机型性能下降。

```c#
public static extern int fuSetASYNCTrackFace(int i);
```

__参数:__

*i*: 输入0关闭，输入1开启，默认关闭

__返回值:__

当返回1时表示成功，0表示失败。

__备注:__

这个接口会**立即**生效。

------

##### fuSetMultiSamples 函数

设置MSAA抗锯齿功能的采样数。默认为0，处于关闭状态。

```c#
public static extern int fuSetMultiSamples(int samples);
```

__参数:__

*samples*：默认为0，处于关闭状态。samples要小于等于设备GL_MAX_SAMPLES，通常可以设置4。

__返回值:__

设置后系统的采样数，设置成功返回samples。 

__备注:__

该功能为硬件抗锯齿功能，需要ES3的Context。
这个接口会**立即**生效。

------

##### SetUseNatCam 函数

UnityDemo使用了NatCam来提高相机效率，同时修改了其代码以便配合UnityPlugin进一步提高效率，但是如果客户需要使用自己的相机插件，则需要调用这个接口来关闭UnityPlugin中相关的优化代码，以防止出现异常。这个开关只在安卓平台生效，其他平台无需关心这个问题。

```c#
 public static extern void SetUseNatCam(int enable);
```

__参数:__

*enable*: 0代表关闭，1代表开启，默认开启

__返回值:__

无

__备注:__

这个接口会**立即**生效。

---

##### ~~SetFlipTexMarkX 函数~~

已弃用

翻转输入的纹理，仅在使用SetDualInput时生效，有些安卓平台nv21buf和tex的方向不一致，可以用这个接口设置tex的X轴镜像。

```c#
 public static extern int SetFlipTexMarkX(bool mark);
```

__参数:__

*mark*: 0表示不翻转，1表示翻转X轴

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

------

##### ~~SetFlipTexMarkY 函数~~

已弃用

翻转输入的纹理，仅在使用SetDualInput时生效，有些安卓平台nv21buf和tex的方向不一致，可以用这个接口设置tex的Y轴镜像。

```c#
 public static extern int SetFlipTexMarkY(bool mark);
```

__参数:__

*mark*: 0表示不翻转，1表示翻转Y轴

__返回值:__

无意义

__备注:__

这个接口会**延迟**生效。

------

##### SetPauseRender 函数

手动暂时屏蔽UnityNamaSDK的渲染，调用这个函数后UnityNamaSDK将暂时停止解析输入的图像数据，即使当前仍有图像数据输入。

```c#
 public static extern void SetPauseRender(bool ifpause);
```

__参数:__

*ifpause*: 0代表不暂停，1代表暂停

__返回值:__

无

__备注:__

这个接口会**立即**生效。

---

##### EnableBufferTest 函数

会在输入的buffer的X轴起点和Y轴起点附近画一个红色的框，用于测试buffer方向，只在输入RGBA buffer的时候生效

```c#
 public static extern void EnableBufferTest(bool t);
```

__参数:__

t: true为开启，false为关闭，默认为false

__返回值:__

无

__备注:__

这个接口会**立即**生效。

------

##### fuIsTracking 函数

获取当前解析完毕后图像中有几张人脸

```c#
 public static extern int fuIsTracking();
```

__参数:__

无

__返回值:__

人脸数，这个值受到fuSetMaxFaces的影响

__备注:__

这个接口会**立即**生效。

------

##### fuSetMaxFaces 函数

设置最多检测几张人脸

```c#
 public static extern int fuSetMaxFaces(int num);
```

__参数:__

*num*: 最多人脸数

__返回值:__

设置之前的系统最大人脸数。

__备注:__

这个接口会**立即**生效。

---

##### fuGetFaceIdentifier 函数

输入当前第N张脸，获取该张脸的独有ID。

```c#
 public static extern int fuGetFaceIdentifier(int face_id);
```

__参数:__

*face_id*: fuIsTracking()会返回当前总共有N张脸，这个数字需要满足(0 <= face_id < N)

__返回值:__

输入数字N，返回当前第N张脸所独有的ID

__备注:__

这个接口会**立即**生效。

------

##### fu_EnableLog 函数

开启UnityPlugin层的Log。Windows环境需要自行开启Unity控制台（fu_EnableLogConsole），或者配合RegisterDebugCallback开启Unity内Log。

```c#
 public static extern void fu_EnableLog(bool isenable);
```

__参数:__

*isenable*: 0表示关闭，1表示开启

__返回值:__

无

__备注:__

这个接口会**立即**生效。

---

##### fu_RegisterDebugCallback 函数

配合fu_EnableLog使用，注册一个C#委托用于处理返回的Log信息，一般就直接使用Debug.Log打在UnityConsole里。

```c#
 private static extern void fu_RegisterDebugCallback(DebugCallback callback);
```

__参数:__

*callback*: 回调委托

__返回值:__

无

__备注:__

这个接口会**立即**生效。

---

##### fu_EnableLogConsole 函数

开启控制台，**仅Windows环境下有效**，可以用来看NAMA SDK输出的log（不包括使用fu_EnableLog 函数开启的UnityPlugin层的Log），同时使用fuSetLogLevel 控制日志等级

```C
public static extern void fu_EnableLogConsole(bool isenable);
```

__参数:__  

*isenable [in]*：true为开启，false为关闭

__备注:__  

这个接口会**立即**生效。

请在初始化（fu_Setup）之前开启

------

##### fuSetLogLevel 函数

Windows环境需要使用fu_EnableLogConsole开启控制台，或者使用fuOpenFileLog开启log写入文件，再使用本接口设置日志等级。

设置当前日志级别，默认INFO级别。设置FU_LOG_LEVEL_OFF时关闭全部日志，设置日志时大于等于当前级别的日志才能正常输出。

```c#
public static extern int fuSetLogLevel(FULOGLEVEL level);
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
```

__参数:__

*level*: log等级

__返回值:__

1表示成功，0表示失败

__备注:__

这个接口会**立即**生效。

------

##### fuGetLogLevel 函数

获取Nama的Log等级。

```c#
public static extern FULOGLEVEL fuGetLogLevel();
```

__参数:__

无

__返回值:__

log等级

__备注:__

这个接口会**立即**生效。

------
##### fuOpenFileLog 函数
打开文件日志，默认nullptr使用console日志。

```C
/**
\brief open file log
\param file_fullname - nullptr for default terminal, non-null for log into file. 
\param max_file_size, max file size in byte. 
\param max_files, max file num for rotating log. 
\return zero for failed, one for success.
*/
public static extern int fuOpenFileLog([MarshalAs(UnmanagedType.LPStr)]string file_pullname, int max_file_size, int max_files);
```
__参数:__

*file_pullname [in]*：日志文件名，全路径，由外部决定日志文件位置，如果为nullptr表示使用默认的console日志。
*max_file_size [in]*：日志文件最大文件大小（单位为byte），超过将重置。
*max_files [in]*：轮换日志文件数量，多个日志文件中进行轮转。
__返回值:__

1表示成功，0表示失败

__备注:__ 

这个接口会**立即**生效。 

---

##### fuGetVersion 函数

获取Nama版本信息

```c#
 public static extern IntPtr fuGetVersion();
 Marshal.PtrToStringAnsi(fuGetVersion());	//调用示例
```

__参数:__

无

__返回值:__

Nama版本

__备注:__

这个接口会**立即**生效。

------

##### fuGetSystemError 函数

获取上一个Nama中发生的错误

```c#
 public static extern int fuGetSystemError();
```

__参数:__

无

__返回值:__

上一个Nama中发生的错误的代号

__备注:__

这个接口会**立即**生效。

系统错误代码及其含义如下：

| 错误代码 | 错误信息                          |
| :------- | :-------------------------------- |
| 1        | 随机种子生成失败                  |
| 2        | 机构证书解析失败                  |
| 3        | 鉴权服务器连接失败                |
| 4        | 加密连接配置失败                  |
| 5        | 客户证书解析失败                  |
| 6        | 客户密钥解析失败                  |
| 7        | 建立加密连接失败                  |
| 8        | 设置鉴权服务器地址失败            |
| 9        | 加密连接握手失败                  |
| 10       | 加密连接验证失败                  |
| 11       | 请求发送失败                      |
| 12       | 响应接收失败                      |
| 13       | 异常鉴权响应                      |
| 14       | 证书权限信息不完整                |
| 15       | 鉴权功能未初始化                  |
| 16       | 创建鉴权线程失败                  |
| 17       | 鉴权数据被拒绝                    |
| 18       | 无鉴权数据                        |
| 19       | 异常鉴权数据                      |
| 20       | 证书过期                          |
| 21       | 无效证书                          |
| 22       | 系统数据解析失败                  |
| 0x100    | 加载了非正式道具包（debug版道具） |
| 0x200    | 运行平台被证书禁止                |

------

##### fuGetSystemErrorString 函数

将错误代号转换成对应string

```c#
 public static extern IntPtr fuGetSystemErrorString(int code);
 Marshal.PtrToStringAnsi(fuGetSystemErrorString(fu_GetSystemError()));		//示例
```

__参数:__

*code*: 错误代号

__返回值:__

字符串指针

__备注:__

这个接口会**立即**生效。

---

##### fuSetCropState 函数

是否开启和关闭裁剪功能，参数设为0关闭，设为1开启。

```c#
public static extern int fuSetCropState(int state);
```

__参数:__

*state*：是否开启和关闭裁剪功能，参数设为0关闭，设为1开启。

__返回值:__

返回状态0为关闭，1开启。

__备注:__

这个接口会**立即**生效。

------

##### fuSetCropFreePixel 函数

自由裁剪接口：x0,y0为裁剪后的起始坐标（裁剪前为（0,0）），x1,y1为裁剪后的终止坐标（裁剪前为（imageWidth,imageHeight））。

```c#
public static extern int fuSetCropFreePixel(int x0, int y0, int x1, int y1);
```

__参数:__

*(x0,y0)*：x0,y0为裁剪后的起始坐标（裁剪前为（0,0））
*(x1,y1)*：x1,y1为裁剪后的终止坐标（裁剪前为（imageWidth,imageHeight））

__返回值:__

返回状态0为失败，1成功。

__备注:__

这个接口会**立即**生效。

---

##### fuSetFaceProcessorFov 函数

设置人脸跟踪坐标空间的FOV，建议跟渲染空间同步，这样才能在AR模式中完美对应

```c#
public static extern void fuSetFaceProcessorFov(float fov);
```

__参数:__

*fov*：短边fov

__返回值:__

无

__备注:__

这个接口会**立即**生效。

默认值：FUAITYPE_FACEPROCESSOR_FACECAPTURE模式下8.6度(角度值)，FUAITYPE_FACEPROCESSOR模式下25度(角度值)，参数推荐范围[5°，60°]，距离默认参数过远可能导致效果下降。

------

##### fuGetFaceProcessorFov 函数

获取人脸跟踪坐标空间的FOV

```c#
public static extern float fuGetFaceProcessorFov();
```

__参数:__

无

__返回值:__

fov

__备注:__

这个接口会**立即**生效。

---

##### fuSetInputCameraTextureMatrix  函数
调整输入的纹理的旋转方向和镜像
```c#
    /**
     \brief input description for fuRender api, use to transform the input gpu
     texture to portrait mode(head-up). then the final output will be portrait mode.
     the outter user present render pass should use identity matrix to present the
     result.
     \param tex_trans_mat, the transform matrix use to transform the input
     texture to portrait mode.
     \note when your input is cpu buffer only don't use
     this api, fuSetInputCameraBufferMatrix will handle all case.
     */
private static extern void fuSetInputCameraTextureMatrix(TRANSFORM_MATRIX tex_trans_mat);

public enum TRANSFORM_MATRIX
{
    /*
         * 8 base orientation cases, first do counter-clockwise rotation in degree,
         * then do flip
         */
    DEFAULT = 0,             // no rotation, no flip
    CCROT0 = DEFAULT,        // no rotation, no flip
    CCROT90,                 // counter-clockwise rotate 90 degree
    CCROT180,                // counter-clockwise rotate 180 degree
    CCROT270,                // counter-clockwise rotate 270 degree
    CCROT0_FLIPVERTICAL,     // vertical flip
    CCROT0_FLIPHORIZONTAL,   // horizontal flip
    CCROT90_FLIPVERTICAL,    // first counter-clockwise rotate 90 degree，then
    // vertical flip
    CCROT90_FLIPHORIZONTAL,  // first counter-clockwise rotate 90 degree，then
    // horizontal flip
    /*
                                  * enums below is alias to above enums, there are only 8 orientation cases
                                  */
    CCROT0_FLIPVERTICAL_FLIPHORIZONTAL = CCROT180,
    CCROT90_FLIPVERTICAL_FLIPHORIZONTAL = CCROT270,
    CCROT180_FLIPVERTICAL = CCROT0_FLIPHORIZONTAL,
    CCROT180_FLIPHORIZONTAL = CCROT0_FLIPVERTICAL,
    CCROT180_FLIPVERTICAL_FLIPHORIZONTAL = DEFAULT,
    CCROT270_FLIPVERTICAL = CCROT90_FLIPHORIZONTAL,
    CCROT270_FLIPHORIZONTAL = CCROT90_FLIPVERTICAL,
    CCROT270_FLIPVERTICAL_FLIPHORIZONTAL = CCROT90,
}
```
__参数:__  

*tex_trans_mat*：旋转镜像参数

__备注:__  

这个接口会**立即**生效。

---

##### fuSetInputCameraTextureMatrixState  函数

开启或关闭纹理旋转镜像调整

```c#
    /**
     \brief set input camera texture transform matrix state, turn on or turn off
     */
private static extern void fuSetInputCameraTextureMatrixState(bool isEnable);
```

__参数:__  

*isEnable*：true为开启，false为关闭

__备注:__  

这个接口会**立即**生效。

---

##### fuSetInputCameraBufferMatrix  函数

调整输入的Buffer的旋转方向和镜像

```c#
    /**
     \brief input description for fuRender api, use to transform the input cpu
     buffer to portrait mode(head-up). then the final output will be portrait mode. 
     the outter user present render pass should use identity matrix to present the
     result.
     \param buf_trans_mat, the transform matrix use to transform the input
     cpu buffer to portrait mode.
     \note when your input is gpu texture only don't
     use this api, fuSetInputCameraTextureMatrix will handle all case.
     */
    private static extern void fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX buf_trans_mat);
```

__参数:__  

*tex_trans_mat*：旋转镜像参数

__备注:__  

这个接口会**立即**生效。

---

##### fuSetInputCameraBufferMatrixState  函数

开启或关闭Buffer旋转镜像调整

```c#
    /**
     \brief set input camera texture transform matrix state, turn on or turn off
     */
private static extern void fuSetInputCameraBufferMatrixState(bool isEnable);
```

__参数:__  

*isEnable*：true为开启，false为关闭

__备注:__  

这个接口会**立即**生效。

---

##### fuSetOutputMatrix  函数

调整输出的纹理的旋转方向和镜像

```c#
/**
     \brief add optional transform for final result, when use
     fuSetInputCameraTextureMatrix/fuSetInputCameraBufferMatrix, we means the output
     is in portrait mode(head-up), and the outter user present render pass should
     use identity matrix to present the result. but in some rare case, user would
     like to use a diffent orientation output. in this case,use
     fuSetInputCameraTextureMatrix/fuSetInputCameraBufferMatrix(portrait mode), then
     use the additional fuSetOutputMatrix to transform the final result to perfer
     orientation.
     \note Don't use this api unless you have to!
     */
private static extern void fuSetOutputMatrix(TRANSFORM_MATRIX out_trans_mat);
```

__参数:__  

*out_trans_mat*：旋转镜像参数

__备注:__  

这个接口会**立即**生效。

---

##### fuSetOutputMatrixState  函数

开启或关闭输出纹理旋转镜像调整

```c#
    /**
     \brief set input camera texture transform matrix state, turn on or turn off
     */
private static extern void fuSetOutputMatrixState(bool isEnable);
```

__参数:__  

*isEnable*：true为开启，false为关闭

__备注:__  

这个接口会**立即**生效。

---

##### fuSetRttCacheState  函数

开启或关闭RTT缓存

```c#
/**
     \brief set internal render target cache state, it is turn off by default.
     */
public static extern void fuSetRttCacheState(bool isEnable);
```

__参数:__  

*isEnable*：true为开启，false为关闭

__备注:__  

这个接口会**立即**生效。

---

##### fuSetFaceProcessorDetectMode  函数

设置人脸检测模式

```c#
/**
 \brief set faceprocessor's face detect mode. when use 1 for video mode, face
 detect strategy is opimized for no face scenario. In image process scenario,
 you should set detect mode into 0 image mode.
 \param mode, 0 for image, 1 for video, 1 by default
 */
public static extern int fuSetFaceProcessorDetectMode(int mode);
```

__参数:__  

*mode*：0为图片，1为视频

__备注:__  

这个接口会**立即**生效。

------



#### 2.5 C#端辅助接口

##### InitCFaceUnityCoefficientSet 函数

实例化所有获取人脸信息的类， 这些实例内含了fuGetFaceInfo，用于获取各种人脸信息。

```c#
 void InitCFaceUnityCoefficientSet(int maxface)
```

__参数:__

*maxface*: 最多几张人脸

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### Start 函数

这个协程会准备初始化整个UnityNamaSDK。准备完毕会自动开启GLLoop。

```c#
 IEnumerator Start()
```

__参数:__

无

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### CallPluginAtEndOfFrames 函数

开启这个协程，在每个Unity生命周期的末尾会自动调用UnityNamaSDK来识别人脸并渲染当前图像帧，如果开启相关参数(EnableHeadLoop)，会同时自动获取识别后的人脸信息。

```c#
 private IEnumerator CallPluginAtEndOfFrames()
```

__参数:__

无

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### DebugMethod 函数

配合fu_RegisterDebugCallback使用，输入返回的Log信息

```c#
 private static void DebugMethod(string message)
```

__参数:__

*message*: log信息

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### LoadAIBundle 函数

封装过的AI数据文件加载函数

```c#
 private IEnumerator LoadAIBundle(string name,FUAITYPE type)
```

__参数:__

*name*: 数据文件全名（带后缀）
*type*: AI数据文件类型

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### LoadTongueBundle 函数

封装过的AI舌头跟踪数据文件加载函数

```c#
 private IEnumerator LoadTongueBundle(string name)
```

__参数:__

*name*: 数据文件全名（带后缀）

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### OnApplicationQuit 函数

在应用退出的时候清理GCHandle及UnityNamaSDK内部的相关数据。

```c#
 private void OnApplicationQuit()
```

__参数:__

无

__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

------

##### EnvironmentCheck 函数

检测当前渲染环境是否为OpenGL，本插件只支持在OpenGL的渲染环境下渲染。

```c#
 bool EnvironmentCheck()
```

__参数:__

无

__返回值:__

false表示检测不通过，true表示检测通过

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

---

##### FixRotation 函数

根据当前平台环境、眼睛注视方向，自动设置道具和跟踪的默认方向。具体应用请看本函数在Demo中的引用。

```c#
 public static void FixRotation(bool ifMirrored = false, FUAI_CAMERA_VIEW eyeViewRot = FUAI_CAMERA_VIEW.ROT_0)
```

__参数:__

`ifMirrored` 是否镜像
`eyeViewRot` 你想要的默认渲染方向，当手机屏幕面对你，home键在下时为ROT_0，在右时为ROT_90，在上时为ROT_180，在左时为ROT_270，一般由重力感应决定这个值


__返回值:__

无

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

---

##### FixRotationWithAcceleration 函数

根据当前平台环境、相机是否镜像以及重力感应方向，自动设置道具和跟踪的默认方向，在场景中需要每帧调用以适应重力感应。具体应用请看本函数在Demo中的引用。

```c#
 public static string FixRotationWithAcceleration(Vector3 g, bool ifMirrored = false)
```

__参数:__

`g` 重力感应参数（Input.acceleration）
`ifMirrored` 相机是否镜像


__返回值:__

当前设置的信息

__备注:__

这是一个C#函数，不是UnityNamaSDK接口

---


### 3. 常见问题 

#### 3.1 类型为IntPtr的形参到底该输入什么？

C#中通常不会用到IntPtr这个类型，但是当需要和Native代码交互的时候，可以用这个类型代替指针的作用。

获取IntPtr的方式很多，这里建议使用GCHandle来得到数组的指针，或者用Marshal.AllocHGlobal来新建一个，

在确定没有内存增减，不需要长期被Native层持有的情况下直接输入数组名称也是可以的，具体可以参考FULiveUnity Demo。

