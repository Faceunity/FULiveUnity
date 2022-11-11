# Unity Nama SDK 集成指导文档  
级别：Public
更新日期：2022-11-11
SDK版本: 8.4.0

------
## 最新更新内容：

2022-11-11 v8.4.0:

- 美型优化下巴功能，参数更新为intensity_chin_mode2。
- 美型优化瘦脸功能，参数不变。

2021-11-1 v8.0.0:

- 美颜磨皮新增均匀磨皮类型，可用于高端机，五官保护更加清晰，磨皮更加均匀。
- 美型原有功能优化，及新增小脸功能。  
- 优化祛黑眼圈法令纹效果。  
- 优化美妆口红、眉毛、睫毛、美瞳等功能。  
- 优化人脸检测，提高检出率。  
- 优化PC端背景分割效果，分割更加准确  

2021-07-08 v7.4.1:

- 优化高分辨美颜磨皮效果，相同尺度上对齐低分辨率  
- 修复人像分割贴纸人体和贴纸没有同时出现的问题  
- 修复道具切换过程中，偶现人脸检测重置问题。  

2021-04-16 v7.4.0:

- 优化2D人体点位和美体性能。  
- 优化人像分割效果和性能。优化手臂和手识别不稳定问题，优化背景误识别问题。修复人像分割偏移问题。  
- 优化美妆效果。优化美瞳贴合效果和溢色问题；优化唇妆遮挡效果，遮挡时口红不再显现。  
- 优化Animoji面部驱动效果。优化小幅度动作，如小幅度张嘴和眨眼时，驱动效果更加灵敏。  
- 优化情绪识别，支持8种基本情绪识。  
- 新增接口fuSetUseAsyncAIInference，支持异步模式，开启异步模式，帧率提升，可改善客户在一些低端设备上帧率不足问题。  
- 新增fuRender接口，为所有业务统一渲染接口，详见接口定义。  
- 新增接口 fuSetInputCameraBufferMatrix，fuSetInputCameraBufferMatrixState，fuSetInputCameraTextureMatrix，fuSetInputCameraTextureMatrixState，fuSetOutputMatrix，fuSetOutputMatrixState，用于设置图像转换矩阵，用于调整输出图像方向，详见接口定义。 

2020-12-29 v7.3.0:

- 美妆性能优化：1）和V7.2比性能显著提升，标准美妆Android端帧率提升29%，iOS端帧率提升17%。2）标准美颜+标准美妆，集成入第三方推流1小时后，在低端机上帧率高于15fps，可流畅运行
- 美体性能及效果优化
   -----性能优化：和V7.2比性能显著提升，Android端帧率提升26%，CPU降低32%；iOS端帧率提升11%，CPU降低46%，内存降低45%
   -----效果优化：1）优化大幅度运动时，头部和肩部位置附近物体变形幅度大的问题；2）人体在画面中出现消失时过渡更自然；3）遮挡情况美体效果更加稳定，不会有高频持续抖动情况
- 人像分割(背景分割)性能优化：和V7.2比性能显著提升，Android端帧率提升64%，CPU降低25%；iOS端帧率提升41%，CPU降低47%，内存降低44%
- 表情识别优化：1）人脸表情重新划分17种类型；2）较线上版本提升识别灵敏度，保证识别准确性
- 虚拟形象驱动效果优化：
   -----Animoji面部驱动效果优化：重点解决客户反馈的3个问题，包括：1）首帧检测模型显示较慢；2）较7.0之前版本，表情捕捉迟钝，细节捕捉不上；3）人脸转动时模型明显变小
   -----Avatar全身驱动效果优化：针对做连续高频大幅度运动的情况，如跳舞等场景，整体模型稳定性，尤其手臂稳定性提升，抖动情况显著改善。
- 绿幕抠像：1）修复调节相似度，前景会有背景图案的遗留bug；2）优化抠像后边缘绿边问题。
- 美颜功能优化：1）新增美型子功能【圆眼】：效果为使眼睛整体放大，尤其是纵向放大明显；2）亮眼：优化亮眼功能下眼睑溢色问题；3）支持透明通道
- 手势功能优化：1）新增支持手势回调接口；
- 控花、控雨、控雪道具重新制作，优化跟踪效果不连贯的问题
- 新增人脸拖拽变形功能，可使用FUCreator 2.1.0进行变形效果编辑。

2020-09-24 v7.2.0:

- 新增绿幕抠像功能，支持替换图片、视频背景等。
- 美颜模块新增瘦颧骨、瘦下颌骨功能。
- 优化美颜性能以及功耗，解决集成入第三方推流服务时易发热掉帧问题。
- 优化手势识别功能的效果以及性能，提升识别稳定性和手势跟随性效果，优化手势识别时cpu占有率。
- 优化PC版各个功能性能，帧率提升显著。美发、美体、背景分割帧率提升30%以上，美颜、Animoji、美妆、手势等功能也有10%以上的帧率提升。
- 优化包增量，SDK分为lite版，和全功能版本。lite版体积更小，包含人脸相关的功能(海报换脸除外)。
- 优化人脸跟踪稳定性，提升贴纸的稳定性。
- 提供独立核心算法SDK，接口文档详见算法SDK文档。
- 人脸算法能力接口封装，算法demo中新增包括人脸特征点位、表情识别和舌头动作3项核心人脸能力。

2020-07-29 v7.1.0:

- 新增美颜锐化功能，见美颜参数文档。
- 优化美颜磨皮效果，保留更多的高频细节。

2020-07-24 v7.0.1:

- 新增接口fuHumanProcessorSetBonemap  
- 新增接口fuHumanProcessorGetResultTransformArray  
- 新增接口fuHumanProcessorGetResultModelMatrix  
- 修复fuGetSestemError问题。  
- 修复fuSetMaxFaces，在释放AI模型后，设置失效问题。  
- 修复Android非高通机型，OES输入问题。  
- 修复美妆远距离嘴部黑框问题。  
- 修复美体美颜共存不支持问题。  

2020-06-30 v7.0.0:

- 新增人体算法能力，包括人体检测、2D人体关键点（全身、半身）、人体3D骨骼（全身、半身）、手势识别、人像mask、头发mask、头部mask、动作识别等能力。
- 性能优化，优化美颜整体性能，中低端机尤为明显，相较于上一版本6.7.0平均提升30%。
- 性能优化，优化美颜初始化耗时，解决中低端机进入长时间卡顿问题。
- 美颜效果提升
  - 修复嘴型功能抬头时上嘴唇失效问题。
  - 修复口罩遮挡时，单输入亮眼会飘问题。
  - 修复口罩遮挡时，白牙会有亮片。
  - 修复去黑眼圈/去法令纹，近距离有分割线问题。
  - 修复长鼻功能特殊机型上问题。
  - 支持仅使用美颜滤镜时,关闭人脸检测功能。
  - 优化人脸跟踪点位的稳定性。
- 美妆效果提升
  - 优化低头、侧脸时口红会飘问题。
  - 优化正常睁眼时美瞳溢出问题。
  - 修复闭眼仍然美瞳效果，美瞳溢出问题。
  - 优化鼻子高光晃动问题。
  - 优化眉毛抖动问题。
- 优化表情跟踪稳定性，解决手机轻微晃动的时候抖动较严重问题，提升Animoji等表情跟踪效果。
- 优化美发效果，使用新的人体算法能力中的头发mask。
- 修复单纹理输入时画面模糊问题。
- 修复Android单输入存在内存泄漏问题。
- 修复多道具同时加载问题。
- 新增接口，详见接口文档
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
- 废弃接口
  - fuSetStrictTracking
  - fuSetASYNCTrackFace
  - fuSetFaceTrackParam

2020-03-19 v6.7.0:

1. 美颜效果
   - 新增去黑眼圈、去法令纹功能
   - 优化磨皮效果，新增只磨皮人脸区域接口功能
   - 优化原有美型效果
2. 优化表情跟踪效果，解决了6.6.0版表情系数表情灵活度问题——FaceProcessor模块优化
   - 解决Animoji表情灵活度问题，基本与原有SDK v6.4.0效果相近
   - 解决优化了表情动图的鼻子跟踪效果问题
3. 优化美妆效果，人脸点位优化，提高准确性
   - 优化口红点位与效果，解决张嘴、正脸、低抬头、左右转头、抿嘴动作的口红溢色
   - 优化美瞳点位效果，美瞳效果稳定
   - 美妆素材效果优化，增加卧蚕提升了眼影层次感，优化腮红拉扯问题
4. 新增接口支持图像裁剪，解决瘦脸边缘变形问题（边缘变形剪裁）
5. 新增接口判断初始化完成状态
6. Unity版本新增离线鉴权

2020-01-19 v6.6.0：

__版本整体说明:__ SDK 6.6.0 主要针对美颜、美妆进行效果优化，性能优化，稳定性优化，同时新增部分特性，使得美颜、美妆效果进入行业顶尖水平。建议对美颜、美妆需求较高的B端用户更新SDK。  
__注意!!!__：此版本由于底层替换原因，表情识别跟踪能力稍有降低，特别是Animoji、表情触发道具的整体表情表现力稍有减弱。Animoji的皱眉、鼓嘴、嘟嘴等动作表现效果比之较差，表情触发道具的发怒（皱眉）、鼓嘴、嘟嘴的表情触发道具较难驱动。其余ARMesh、哈哈镜、明星换脸、动态人像（活照片）的面部跟踪整体稍有10%的效果减弱。故用到表情驱动的功能重度B端用户，仍建议使用SDK6.4.0版，使用其余功能（美颜叠加贴纸等其余功能）的场景不受影响，表情识别跟踪能力将在下一版进行优化更新。   

- 美颜优化：  
  1). 新增美型6款功能，包括开眼角、眼距、眼睛角度、长鼻、缩人中、微笑嘴角。
   2). 新增17款滤镜，其中包含8款自然系列滤镜、8款质感灰系列滤镜、1款个性滤镜。
   3). 优化美颜中亮眼、美牙效果。
   4). 优化美颜中3个脸型，调整优化使得V脸、窄脸、小脸效果更自然。
   5). 优化美白红润强度，美白、红润功能开放2倍参数，详见美颜文档。
- 美妆优化：  
  1). 新增13套自然系组合妆，13套组合妆是滤镜+美妆的整体效果，可自定义。
   2). 新增3款口红质地：润泽、珠光、咬唇。
   3). 提升美妆点位准确度 ，人脸点位由209点增加至 239点。
   4). 优化美妆素材叠加方式，使得妆容效果更加服帖自然。
   5). 优化粉底效果，更加贴合人脸轮廓。
- 提升人脸点位跟踪灵敏度，快速移动时跟踪良好，使美颜美妆效果跟随更紧密。
- 提升人脸点位的稳定性，解决了半张脸屏幕、大角度、遮挡等场景的阈值抖动问题，点位抖动问题也明显优化。
- 提升人脸跟踪角度，人脸最大左右偏转角提升至70度，低抬头检测偏转角也明显提升。
- 优化美发道具CPU占有率，Android/iOS提升约30%
- 新增MSAA抗锯齿接口，fuSetMultiSamples，解决虚拟形象（animoji与捏脸功能）边缘锯齿问题，详见接口文档。
- 架构升级，支持底层AI算法能力和业务逻辑拆分，优化性能，使得系统更加容易扩展和更新迭代：  
  1). 新增加接口 fuLoadAIModelFromPackage 用于加载AI能力模型。
   2). 新增加接口 fuReleaseAIModel 用于释放AI能力模型。
   3). 新增加接口 fuIsAIModelLoaded 用于判断AI能力是否已经加载。

__注1__：从SDK 6.6.0 开始，为了更新以及迭代更加方便，由原先一个nama.so拆分成两个库nama.so以及fuai.so，其中nama.so为轻量级渲染引擎，fuai.so为算法引擎。升级6.6.0时，需添加fuai库。  
__注2__: 更新SDK 6.6.0时，在fuSetup之后，需要马上调用 fuLoadAIModelFromPackage 加载 ai_faceprocessor.bundle !!!  
__注3__: SDK 6.6.0 进行较大的架构调整 , 架构上拆分底层算法能力和业务场景，使得SDK更能够按需复用算法模块，节省内存开销，算法能力模块后期更容易维护升级，使用方式详见新增加的一组接口定义fuLoadAIModelFromPackage / fuReleaseAIModel / fuIsAIModelLoaded 。  

2019-09-25 v6.4.0:

- 新增美体瘦身功能，支持瘦身、长腿、美臀、细腰、肩部调整，一键美体。
- 优化美颜功能中精细磨皮，性能以及效果提升，提升皮肤细腻程度，更好保留边缘细节。
- 优化美发功能，边缘稳定性及性能提升。
- 优化美妆功能，性能提升，CPU占有率降低，Android中低端机表现明显。
- 优化手势识别功能，性能提升，CPU占有率降低，在Android机型表现明显。
- 修复人脸检测多人脸偶现crash问题。
- 修复捏脸功能中模型截断问题。
- 关闭美颜道具打印冗余log。

2019-08-14 v6.3.0：

- 优化人脸美妆功能，提高性能，降低功耗。
- 新增fuSetFaceTrackParam接口，用于设置人脸表情跟踪参数。 

- 新增人脸美颜精细磨皮效果。

**工程案例更新：**

- 增加新的接口支持yuv420输入图像数据
- Nama8.0.0重构了相机模块代码，采用新的nativeCamera,新相机直接从C++层去调用相机，区别于之前通过Java中间层，效率更高
- Nama7.4.0重构了Unity/Nama中间层的代码，部分接口被删除，同时跟踪帧率不再跟随相机帧率，而是Unity渲染帧率，这样可以提高整体流畅度。
- Nama7.4.0优化了输入输出数据的旋转镜像问题，具体请查看5.2
- Nama7.4.0以后把重新整理了道具文件的路径，现在所有.bytes文件都在Assets\StreamingAssets\faceunity下
- 由于Nama7.0改动巨大，接口变动也较大，但是接口调用方式基本不变，接口变动详见Unity_Nama_API_参考文档
- 由于Nama 6.6的内部机制更新，AI和渲染分离，现在Nama运行在FU_Mode_RenderItems模式下（渲染Nama道具）时，如果不加载任何道具，Nama也不会运行任何AI逻辑，此时无法进行人脸检测等操作，也无法拿到相关数据！！！因此本工程案例里在DataOut场景和Simple场景中都添加了自动加载一个空道具的逻辑，以应对出现的问题。
- 当Nama运行在FU_Mode_TrackFace模式下时，无需加载任何道具，会自动跑人脸识别的AI逻辑
- Nama6.6同时也带来了道具加载卸载机制的更新，新的道具加载卸载接口已经全部都是同步接口，调用后立即执行，没有异步没有协程，简化了道具加载卸载的逻辑复杂度。道具载入可以用多线程调用以降低主线程被卡住的风险（fuCreateItemFromPackage）
- 本次更新添加了一个C#封装函数以更新默认道具/跟踪方向，这个函数会根据当前平台环境、相机是否镜像以及重力感应方向，自动设置道具和跟踪的默认方向，在场景中需要每帧调用以适应重力感应。具体描述请看API文档，具体应用请看本函数在Demo中的引用。

------
## 目录：
本文档内容目录：

[TOC]

------
## 1. 简介 
本文档旨在说明如何将Faceunity Nama SDK集成您的Unity工程中。  

------


## 2. SDK文件结构

本小节，描述Demo文件结构，各个目录，以及重要文件的功能。

```
+FULiveUnity
  +Assets 			  	//Unity资源目录
    +Examples				//示例目录
      +Common					//Demo的公共资源。
      	+Materials：一些公共材质。
        +Script：一些公共脚本。
        +Shader：渲染模型使用的Shader，仅供参考。
        +Textures：Demo的Logo图片。
        +UIImgs：一些公共UI图片。
      +DataOut					// NamaSDK的数据输出模式，使用Unity进行内容渲染，使用了NativeCamera以提高效率。仅输出人脸的位置、旋转、表情系数等，以供Unity渲染。
      	+Models: 人头模型和对应材质纹理。
      	+Scene: Demo场景，demoDataOut 是人头模型渲染，demoDataOut_Multiple是多人模型渲染。
      	+Script: Demo的相关脚本。
		 -RenderToModel.cs: 初始化相机和加载默认道具。
		 -StdController.cs: 负责控制人头的位置、旋转和表情系数。
		 -EyeController.cs: 负责控制眼睛的位置和旋转。
		 -UIManagerForDataOut.cs: DataOut场景的UI控制器。
		 -UIManagerForDataOut_Multiple.cs: DataOut_Multiple场景的UI控制器，也负责多人模型调度。
      +Simple					//最简单的NamaSDK的使用案例。
       +Scene: demoSimple是本例的场景。
	   +Script: Demo的相关脚本。
		 -RenderSimple.cs: 如果你需要输入其他渠道获得的图像数据，请参考这个函数。
		 -UIManagerSimple.cs: 简单场景的UI控制器，注册了切换相机按钮，管理人脸检测标志。
      +TexOut					//NamaSDK的纹理输出模式，使用NamaSDK进行内容渲染，使用了NativeCamera以提高效率。直接输出本插件渲染好的数据，可以使用附带的二进制道具文件。
      	+Resources: 一些资源文件。
	    +Scene: demoTexOut是本例的场景。
	    +Script: Demo的相关脚本。
		 -RenderToTexture.cs: 初始化相机。
		 -UIManagerForTexOut.cs: 纹理输出模式的UI控制器，和RenderToTexture配合以展现所有道具的功能。
		 -ItemConfig.cs: 道具的二进制文件和UI文件的路径等信息的配置文件。
    +Plugins				
      +CNamaSDK             //NamaSDK文件目录
      +NativeCamera         //NativeCamera文件目录
    +Script					//核心代码文件目录
      -FaceunityWorker.cs：负责初始化NamaSDK并引入C++接口，初始化完成后每帧更新人脸跟踪数据
      -BaseRender.cs:RenderModel,RenderSimple,RenderToTexture的父类，负责对接相机插件，输入输出图像数据，加载卸载道具。
      -CameraManager.cs:相机管理类
      -ICamera：相机的父类
      -NativeCamera：直接调用c++相机接口，运行相机（Android7.0以上的手机支持c++直接调用Android相机）
      -LegacyCamera：Unity的WebCamTexture运行相机（Android7.0以下手机，以及某些Android7.0以上不支持C++直接调用相机）
    +StreamingAssets		//数据文件目录
      +faceunity
       +graphics：特殊的道具，如美美颜美妆道具
       +items：常规道具，如animoji
       +model：算法数据文件
        -ai_bgseg.bytes：背景分割AI数据文件
        -ai_bgseg_green.bytes：带绿幕的背景分割AI数据文件
        -ai_face_processor.bytes：初始化完成后必须加载的AI数据文件（通用版）
        -ai_face_processor_pc.bytes：初始化完成后必须加载的AI数据文件（PC/MAC版）
        -ai_hand_processor.bytes：手势识别AI数据文件（通用版）
        -ai_hand_processor_pc.bytes：手势识别AI数据文件（PC/MAC版）
        -ai_human_processor.bytes：人体姿态跟踪AI数据文件（通用版）
        -ai_human_processor_pc.bytes：人体姿态跟踪AI数据文件（PC/MAC版）
        -aitype.bytes：用于在FU_Mode_RenderItems模式下，调整AIType
        -tongue.bytes：舌头跟踪AI数据文件（即将废弃，ai_face_processor内已包含新版舌头跟踪数据）
  +docs					//文档目录
  +ProjectSettings   	//Unity工程配置目录
  -readme.md			//工程总文档
  
```

------
## 3. 集成指引


### 3.1 开发环境
#### 3.1.1 支持平台
```
Windows、Android、iOS（9.0以上系统）、Mac
```
#### 3.1.2 开发环境
```
Unity2018.4.23f1 及以上
```

### 3.2 准备工作 
- [下载demo代码](https://github.com/Faceunity/FULiveUnity)
- 获取证书:
  1. 拨打电话 **0571-88069272** 
  2. 发送邮件至 **marketing@faceunity.com** 进行咨询。

### 3.3 相关配置
- 将证书文件中的数据复制到场景中FaceunityWorker物体的Inspector面板的LICENSE输入框内，并保存场景（其他场景同理）。

![](imgs/img0.jpg)

- 如需要将本SDK集成到你的工程中去：
  1. 请将demo中的Assets文件夹下除Examples文件夹以外的所有文件及文件夹复制到你的工程中去。
  2. 配置渲染环境为OpenGL。
  3. 在场景中新建一个物体，挂载FaceunityWorker组件，参照上图填写证书信息。
  4. 编写你自己的图像输入的代码组件。
  5. 参考demo获取跟踪数据，或者加载道具进行渲染。

### 3.4 初始化

**本条案例可查看demoSimple场景。** 

查看demoSimple场景中的物体Faceunity Worker，这个物体挂载了4个代码组件，其中FaceunityWorker是所有使用本SDK的场景都必须要有的组件，这个组件引入了本SDK的所有接口，初始化代码、每帧进行SDK渲染的协程(CallPluginAtEndOfFrames)以及相关代码，剩下组件的为本案例的示例代码。

每个场景开始的时候FaceunityWorker会调用DontDestroyOnLoad使宿主物体不在切换场景的时候被销毁，同时确保场景中只有一个FaceunityWorker组件实例。本Demo中的所有案例都不支持相互切换场景，如有需求请确保在切换场景的时候这个组件不被销毁，同时有且只有这一个组件实例。

FaceunityWorker的Start函数中会初始化本SDK，初始化完成后会执行初始化完成事件，同时开启每帧进行SDK渲染的协程(CallPluginAtEndOfFrames)。切换场景的时候CallPluginAtEndOfFrames可能会停止运行，可能需要再次调用相关协程启动函数。

### 3.5 输入图像数据

**本条案例可查看demoSimple场景。** 

这个案例用最少的代码运行本SDK，为使本例能运行，在RenderSimple里直接初始化相机输入相机返回的图像数据。**如果你想输入自己的图像数据，请参考本案例。**

RenderSimple里最关键的函数：

```c#
public void UpdateData(IntPtr ptr,int texid,int w,int h, UpdateDataMode mode)
```

这个函数展示了如何输入图像数据进本插件，以及怎样从插件中取出渲染完毕的数据。

```
    public enum UpdateDataMode
    {
       RGBABuffer,
       BGRABuffer,
       TexID,
       NV21Buffer,
       NV21BufferAndTexID,
       YUV420Buffer
    }
```

以上六种UpdateDataMode为本SDK输入图像数据的四种格式。

RGBABuffer格式的buffer数组，通用性最强，各平台均可使用

BGRABuffer格式的buffer数组，通用性最强，各平台均可使用

`TexID` GL纹理ID，某些特殊GL环境下无法使用，但是一定程度上性能高于Image

`NV21Buffer` NV21格式的buffer数组，通常在安卓设备上，通过原生相机插件使用

NV21BufferAndTexID同时输入NV21格式的buffer数组和纹理ID，通常在安卓设备上，通过原生相机插件使用，效率最高

YUV420Buffer通常在安卓设备上，通过原生相机插件使用，NV21也是YUV的一种

```c#
public static extern int fu_SetRuningMode(int runningMode);
public enum FURuningMode
    {
        FU_Mode_None = 0,
        FU_Mode_RenderItems, 
        FU_Mode_Beautification,
        FU_Mode_Masked, 
        FU_Mode_TrackFace
    };
```

fu_SetRuningMode可以设置本插件运行模式，针对需求设置运行模式可以大大提高效率。FU_Mode_RenderItems为默认运行模式，可以在FaceunityWorker.cs中自行更改初始值，也可在运行时更改。具体各项效果请查看API文档。

- 由于Nama 6.6的内部机制更新，AI和渲染分离，现在Nama运行在FU_Mode_RenderItems模式下（渲染Nama道具）时，如果不加载任何道具，Nama也不会运行任何AI逻辑，此时无法进行人脸检测等操作，也无法拿到相关数据！！！因此本工程案例里在DataOut场景和Simple场景中都添加了自动加载一个空道具的逻辑，以应对出现的问题。
- 当Nama运行在FU_Mode_TrackFace模式下时，无需加载任何道具，会自动跑人脸识别的AI逻辑

**查看BaseRenerder。** 

demoDataOut和demoTexOut场景中使用了NativeCamera来加速视频功能，同时要处理视频旋转镜像，因此数据输入过程较为复杂。

***UNITY_ANDROID的环境下：*** 

在RenderSimple/RenderToModel/RenderToTexture中，会在初始化时开启NativeCamra，NativeCamra会在C++层拿到相机数据直接向本SDK输入数据。输出的时候请自行处理镜像旋转缩放。

***UNITY_IOS的环境下：*** 

初始化步骤同ANDROID，NativeCamra会在C++层拿到相机数据直接向本SDK输入数据。但是输出的时候IOS环境下插件自动处理镜像和旋转，输出后只需处理缩放。

### 3.6 输出跟踪数据

**本条案例可查看demoDataOut场景。** 

demoDataOut场景中点击UI上的TrackPositon可以切换渲染模式，点击头像Icon可以切换模型。

初始化完成后，FaceunityWorker会在每帧调用的CallPluginAtEndOfFrames中更新人脸跟踪数据（如果EnableExpressionLoop为true），每张人脸的每个数据都以CFaceUnityCoefficientSet这个类的形式存储着，外界需要获取数据的时候只需要直接调用这个数据对应的这个类实例即可，具体可以参考这个场景中的StdController和EyeController。

**demoDataOut场景中：** 

StdController在每帧通过FaceunityWorker中预存的地址获取旋转和表情数据，并根据镜像情况设置模型对应参数。其中表情数据是包含了56个blendshape值，和预先制作的带有56个blendshape的模型配合，使用SkinnedMeshRenderer.SetBlendShapeWeight设置。

EyeController在每帧通过FaceunityWorker中预存的地址获取眼睛的旋转数据，并根据镜像情况设置眼睛旋转。

点击UI上的TrackPositon可以开启/关闭位置跟踪，同时切换RuningMode。TrackPositon开启时设置运行模式为FU_Mode_RenderItems，同时渲染摄像机图像到UI上。关闭时设置运行模式为FU_Mode_TrackFace，同时关闭图像渲染以提高性能。

**demoDataOut_Multiple场景中：** 

demoDataOut场景AR模式（开启TrackPositon）的多人版本，在场景中Faceunity Worker物体的Inspector中设置MAXFACE可以修改最多同时跟踪的人脸数量。

**关于舌头跟踪：**

1.必须加载tongue.bytes

2.Texout场景中加载带舌头功能的道具，比如青蛙，即可展现出舌头跟踪效果。

3.在Dataout场景中，FURuningMode为FU_Mode_RenderItems的时候，加载EmptyItem.bytes，才能开启舌头跟踪。FURuningMode为FU_Mode_TrackFace的时候，调用fu_SetTongueTracking(1)，才能开启舌头跟踪。注意，每次切换到FU_Mode_TrackFace之后都需要设置一次！！！

### 3.7 输出图像数据（道具创建、销毁、切换）

**本条案例可查看demoTexOut场景。**

 在demoTexOut场景中，挂载了RenderToTexture（继承baseRenerder），这个组件不仅负责控制相机并输入图像数据，同时也封装了一些道具加载卸载的接口，如果你还不是很了解原生的道具接口以及相关特性，可以参考这个案例。

```c#
public IEnumerator LoadItem(Item item, int slotid = 0, LoadItemCallback cb=null)
```

这个接口封装了道具的加载和启用，同时使用slot的概念控制多个道具的使用。

```c#
int[] itemid_tosdk;
GCHandle itemid_handle;
IntPtr p_itemsid;
```

itemid_tosdk即slot数组，里面保存了每个slot需要渲染的itemid。如果为0或其他无效id则会被跳过，不用担心输错数据。

```c#
var bundledata = Resources.LoadAsync<TextAsset>(item.fullname);
int itemid = FaceunityWorker.fuCreateItemFromPackage(pObject, bundle_bytes.Length);
UnLoadItem(slotid); //卸载上一个在这个slot槽内的道具
FaceunityWorker.fu_SetItemIds(p_itemsid, SLOTLENGTH, IntPtr.Zero);
```

这4行代码组成了这个接口的主要功能（这里只是伪代码），第一行调用unity的IO接口读取道具文件，第二行调用fuCreateItemFromPackage加载道具，并返回道具的itemid，**这里建议在实际应用中在其他线程调用这个接口以降低主线程被卡住的风险**，因为加载完毕的道具并不会直接被渲染，这里要借助slot数组，第3行先卸载在slot数组的第N位道具，然后将刚刚得到的itemid填入slot数组的第N位，第4行设置接下来真正渲染的所有道具。

------
## 4. 功能模块
**本条案例可查看demoTexOut场景。**

除特殊文件如v3.bytes、tongue.bytes、ai_XXX.bytes这种AI数据文件以外的道具（*.bytes或 *.bundle）都可直接用LoadItem或者原生接口加载。

以下功能模块均可以使用这个方法加载，以下只详细描述对应道具所独有的参数。

```c#
//rtt即RenderToTexture的实例，这个为实际代码中封装后的加载方法
yield return rtt.LoadItem(ItemConfig.beautySkin[0], (int)SlotForItems.Beauty);

//原生方法
var bundledata = Resources.LoadAsync<TextAsset>(itempath);
yield return bundledata;
var data = bundledata.asset as TextAsset;
byte[] bundle_bytes = data != null ? data.bytes : null;
GCHandle hObject = GCHandle.Alloc(bundle_bytes, GCHandleType.Pinned);
IntPtr pObject = hObject.AddrOfPinnedObject();
int itemid = FaceunityWorker.fuCreateItemFromPackage(pObject, bundle_bytes.Length);
hObject.Free();
var itemid_tosdk = new int[1];
var itemid_handle = GCHandle.Alloc(itemid_tosdk, GCHandleType.Pinned);
var p_itemsid = itemid_handle.AddrOfPinnedObject();
FaceunityWorker.fu_SetItemIds(p_itemsid, 1, IntPtr.Zero);
```

### 4.1 视频美颜

视频美颜配置方法与视频加特效道具类似，首先创建美颜道具句柄，并保存在句柄数组中:

```c#
//rtt即RenderToTexture的实例，这个为实际代码中封装后的加载方法
yield return rtt.LoadItem(ItemConfig.beautySkin[0], (int)SlotForItems.Beauty);
```

在处理视频时，美颜道具句柄会通过句柄数组传入图像处理接口，处理完成后美颜效果将会被作用到图像中。示例如下：

```c#
var m_fu_texid = FaceunityWorker.fu_GetNamaTextureId();
var m_rendered_tex = Texture2D.CreateExternalTexture(width, height, TextureFormat.RGBA32, false, true, (IntPtr)m_fu_texid);
//SDK会返回纹理ID，即m_fu_texid
//使用纹理ID创建纹理，即m_rendered_tex，将它用在你的材质上，即可输出渲染结果
//所有道具的渲染结果都在这张纹理上
```

#### 参数设置

美颜道具主要包含七个模块的内容：滤镜、美白、红润、磨皮、亮眼、美牙、美型。

美颜道具的所有功能都通过设置参数来进行设置，以下为设置参数的代码示例：

```c#
//封装过的代码，以下代码功能为设置各美颜参数为默认值
BeautySkinItemName = ItemConfig.beautySkin[0].name;
for (int i = 0; i < BeautyConfig.beautySkin_1.Length; i++)
	{
		rtt.SetItemParamd(BeautySkinItemName, 			BeautyConfig.beautySkin_1[i].paramword, BeautyConfig.beautySkin_1[i].defaultvalue);
	}
for (int i = 0; i < BeautyConfig.beautySkin_2.Length; i++)
	{
		rtt.SetItemParamd(BeautySkinItemName, BeautyConfig.beautySkin_2[i].paramword, BeautyConfig.beautySkin_2[i].defaultvalue);
	}

//原生方法
public static extern int fuItemSetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, double value);
public static extern int fuItemSetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string value);
//例：
FaceunityWorker.fuItemSetParamd(1, "color_level", 1.0f);
```

每个模块都有默认效果，它们可以调节的参数如下。

#### 4.1.1 滤镜

滤镜功能主要通过参数filter_level 和 filter_name来控制

```
filter_level 取值范围 0.0-1.0,0.0为无效果，1.0为最大效果，默认值1.0
filter_name 取值为一个字符串，默认值为 “origin” ，origin即为使用原图效果
```

filter_name参数的取值和相关介绍详见：[美颜道具功能文档](美颜道具功能文档.md) ，在 **滤镜对应key值** 部分有详细介绍，对于老版本（6.0之前）的用户，可以参考 **新老滤镜对应关系** 部分。

#### 4.1.2 美白和红润

##### 美白

美白功能主要通过参数color_level来控制

```
color_level 取值范围 0.0-2.0,0.0为无效果，2.0为最大效果，默认值0.5
```

##### 红润

红润功能主要通过参数red_level 来控制

```
red_level 取值范围 0.0-2.0,0.0为无效果，2.0为最大效果，默认值0.5
```

#### 4.1.3 磨皮

控制磨皮的参数有四个：blur_level，skin_detect，nonskin_blur_scale，heavy_blur

```
blur_level: 磨皮程度，取值范围0.0-6.0，默认6.0
skin_detect:肤色检测开关，0为关，1为开
nonskin_blur_scale:肤色检测之后非肤色区域的融合程度，取值范围0.0-1.0，默认0.45
heavy_blur: 朦胧磨皮开关，0为清晰磨皮，1为朦胧磨皮
blur_type：此参数优先级比heavy_blur低，在使用时要将heavy_blur设为0，0 清晰磨皮  1 朦胧磨皮  2精细磨皮 3 均匀磨皮
blur_use_mask: ios端默认为1，其他端默认为0。1为开启基于人脸的磨皮mask，0为不使用mask正常磨皮。只在blur_type为2时生效。
```

**注意1：精细磨皮为建议使用的磨皮类型。**

注意2：朦胧磨皮为高级美颜功能，需要相应证书权限才能使用

#### 4.1.4 亮眼

亮眼功能主要通过参数eye_bright 来控制

```
eye_bright   取值范围 0.0-1.0,0.0为无效果，1.0为最大效果，默认值1.0
```

注意：亮眼为高级美颜功能，需要相应证书权限才能使用

#### 4.1.5美牙

美牙功能主要通过参数tooth_whiten来控制

```
tooth_whiten   取值范围 0.0-1.0,0.0为无效果，1.0为最大效果，默认值1.0
```

注意：美牙为高级美颜功能，需要相应证书权限才能使用

#### 4.1.6 去黑眼圈

去黑眼圈功能主要通过参数remove_pouch_strength来控制

```
remove_pouch_strength   取值范围 0.0-1.0,0.0为无效果，1.0为最大效果，默认值1.0
```

注意：去黑眼圈为高级美颜功能，需要相应证书权限才能使用

#### 4.1.7 去法令纹

去法令纹功能主要通过参数remove_nasolabial_folds_strength来控制

```
remove_nasolabial_folds_strength   取值范围 0.0-1.0,0.0为无效果，1.0为最大效果，默认值1.0
```

注意：去法令纹为高级美颜功能，需要相应证书权限才能使用

#### 4.1.8 美型

美型的整体程度由face_shape_level参数控制

```
face_shape_level   取值范围 0.0-1.0,0.0为无效果，1.0为最大效果，默认值1.0
```

美型的渐变由change_frames参数控制

```
change_frames       0为关闭 ，大于0开启渐变，值为渐变所需要的帧数
```

美型的种类主要由face_shape 参数控制

```
face_shape: 变形取值 0:女神变形 1:网红变形 2:自然变形 3:默认变形 4:精细变形
```

在face_shape选取不同参数时，对应可以使用的参数也不同：

##### face_shape参数详解

```
1.
face_shape 为0 1 2 3时
对应0：女神 1：网红 2：自然 3：默认 4:精细变形 默认4
可以使用参数
eye_enlarging: 	默认0.5,		//大眼程度范围0.0-1.0
cheek_thinning:	默认0.0,  		//瘦脸程度范围0.0-1.0
2.
face_shape 为4时，为用户自定义的精细变形，开放了脸型相关参数，添加了窄脸小脸参数
eye_enlarging: 	默认0.5,		//大眼程度范围0.0-1.0
cheek_thinning:	默认0.0,  		//瘦脸程度范围0.0-1.0
cheek_v:	默认0.0,  		//v脸程度范围0.0-1.0
cheek_narrow:   默认0.0,          //窄脸程度范围0.0-1.0
cheek_small:   默认0.0,          //小脸程度范围0.0-1.0
intensity_nose: 默认0.0,        //瘦鼻程度范围0.0-1.0
intensity_forehead: 默认0.5,    //额头调整程度范围0.0-1.0，0-0.5是变小，0.5-1是变大
intensity_mouth:默认0.5,       //嘴巴调整程度范围0.0-1.0，0-0.5是变小，0.5-1是变大
intensity_chin: 默认0.5,       //下巴调整程度范围0.0-1.0，0-0.5是变小，0.5-1是变大
intensity_philtrum：默认0.5    //人中调节范围0.0~1.0， 0.5-1.0变长，0.5-0.0变短
intensity_long_nose：默认0.5    //鼻子长度调节范围0.0~1.0， 0.5-0.0变短，0.5-1.0变长
intensity_eye_space：默认0.5    //眼距调节范围0.0~1.0， 0.5-0.0变长，0.5-1.0变短
intensity_eye_rotate：默认0.5    //眼睛角度调节范围0.0~1.0， 0.5-0.0逆时针旋转，0.5-1.0顺时针旋转
intensity_smile：默认0.0    //微笑嘴角程度范围0.0~1.0 1.0程度最强
intensity_canthus：默认0.0    //开眼角程度范围0.0~1.0 1.0程度最强
```

注意1：变形为高级美颜功能，需要相应证书权限才能使用

注意2：以上参数后面均表明了取值范围，如果超出了取值范围会影响效果，不建议使用

### 4.2 Animoji

```c#
yield return rtt.LoadItem(ItemConfig.item_1[0], (int)SlotForItems.Item);
```

Animoji默认渲染时不会跟踪人脸位置，但是可以通过开启AR模式来开启跟踪和背景图像显示：

```c#
rtt.SetItemParamd(item.name, "{\"thing\":\"<global>\",\"param\":\"follow\"}", 1);
```

镜像相关参数：

```c#
//is3DFlipH 参数是用于对3D道具的顶点镜像
FaceunityWorker.fuItemSetParamd(itemid, "is3DFlipH", param);
//isFlipExpr 参数是用于对道具内部的表情系数的镜像
FaceunityWorker.fuItemSetParamd(itemid, "isFlipExpr", param);
//isFlipTrack 参数是用于对道具的人脸跟踪位置旋转的镜像
FaceunityWorker.fuItemSetParamd(itemid, "isFlipTrack", param);
//isFlipLight 参数是用于对道具内部的灯光的镜像
FaceunityWorker.fuItemSetParamd(itemid, "isFlipLight", param);
```

### 4.3 动漫滤镜

```c#
yield return rtt.LoadItem(ItemConfig.commonFilter[0], (int)SlotForItems.CommonFilter);
```

设置参数来切换滤镜风格：

```c#
rtt.SetItemParamd((int)SlotForItems.CommonFilter, "style", 0);
//你可以尝试设置0以外的整数
```

### 4.4 AR面具

```c#
yield return rtt.LoadItem(ItemConfig.item_3[0], (int)SlotForItems.Item);
```

### 4.5 背景分割

```c#
yield return rtt.LoadItem(ItemConfig.item_7[0], (int)SlotForItems.Item);
```

### 4.6 表情识别

```c#
yield return rtt.LoadItem(ItemConfig.item_5[0], (int)SlotForItems.Item);
```

### 4.7 手势识别

```c#
yield return rtt.LoadItem(ItemConfig.item_8[0], (int)SlotForItems.Item);
```

手势识别的旋转镜像参数为独有：

```c#
//rotMode为手势识别方向
FaceunityWorker.fuItemSetParamd(itemid, "rotMode", 2);
//loc_x_flip为渲染X轴镜像
FaceunityWorker.fuItemSetParamd(itemid, "loc_x_flip", 1.0);
```

### 4.8 贴纸

```c#
yield return rtt.LoadItem(ItemConfig.item_2[0], (int)SlotForItems.Item);
```

### 4.9 人脸夸张变形功能

```c#
yield return rtt.LoadItem(ItemConfig.item_9[0], (int)SlotForItems.Item);
```

### 4.10 音乐滤镜

```c#
yield return rtt.LoadItem(ItemConfig.item_6[0], (int)SlotForItems.Item);
```

通过随着音乐设置播放时间才能让滤镜“动”一起

```c#
rtt.SetItemParamd(name, "music_time", audios.time * 1000);
```

### 4.11 质感美颜

这个选项本质不是某类道具，而是美颜和美妆的组合，其中美颜部分请参考上述内容，美妆部分请参考[美妆道具功能文档](美妆道具功能文档.md)。美妆道具依赖一个独立的bundle（MakeupAssist）这个bundle要一起加载。

```c#
yield return rtt.LoadItem(ItemConfig.makeup[1], (int)SlotForItems.MakeupAssist);
yield return rtt.LoadItem(ItemConfig.makeup[0], (int)SlotForItems.Makeup);
```

这个道具的调用方法相对比较复杂：

```c#
rtt.SetItemParamd((int)SlotForItems.Makeup, "is_makeup_on", 1);
rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_intensity", makeupitem.intensity);
                
rtt.SetItemParamdv((int)SlotForItems.Makeup, "makeup_lip_color", makeupitem.Lipstick_color);
rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_intensity_lip", makeupitem.Lipstick_intensity);
rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_lip_mask", 1.0);
 
CreateTexForItem(uisprites.GetTexture(MakeupType.Blush, makeupitem.Blush_id), "tex_blusher");
rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_intensity_blusher", makeupitem.Blush_intensity);
 
CreateTexForItem(uisprites.GetTexture(MakeupType.Eyebrow, makeupitem.Eyebrow_id), "tex_brow");
rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_intensity_eyeBrow", makeupitem.Eyebrow_intensity);
 
CreateTexForItem(uisprites.GetTexture(MakeupType.Eyeshadow, makeupitem.Eyeshadow_id), "tex_eye");
rtt.SetItemParamd((int)SlotForItems.Makeup, "makeup_intensity_eye", makeupitem.Eyeshadow_intensity);
```

------
## 5. 常见问题 

### 5.1 编译相关

- iOS编译时请选择**9.0**以上系统版本。

- **因Github不支持上传100MB以上的文件，iOS的库经过压缩，使用时请自行解压！**

- Unity工程导入可能会导致部分native库的平台信息丢失，如果运行或者编译时报错提示找不到库，请手动修改相应库的平台信息。本案例中的native库有:
  - Assets\Plugins
    ![](imgs\img2.jpg)
  
- iOS平台编译Link可能报错，缺少某些framework，请手动添加CoreML.framework以及Metal.framework

### 5.2 关于镜像/旋转

整个工程中镜像/旋转的概念在多个地方被多次提到，这里解释一下不同地方的镜像/旋转的概念。
#### 5.2.1 输入图像数据镜像/旋转 和 跟踪数据的镜像/旋转
```c#
//Nama7.4.0以后添加了以下接口：
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

//调整输入buffer的旋转镜像
fuSetInputCameraBufferMatrix(TRANSFORM_MATRIX tex_trans_mat);
//是否开启buffer调整
fuSetInputCameraBufferMatrixState(bool isEnable);

//调整输入纹理的旋转镜像
fuSetInputCameraTextureMatrix(TRANSFORM_MATRIX tex_trans_mat);
//是否开启纹理调整
fuSetInputCameraTextureMatrixState(bool isEnable);

//调用以上接口口，输出的buffer/tex的方向/镜像都会被调整，同时输出的跟踪数据方向也会被对齐到对应方向
//理论上来说以后不再需要根据平台调整跟踪数据方向
```


#### 5.2.2 道具渲染的镜像/旋转
道具的镜像旋转部分由以下参数控制，或者某些独有参数。

```c#
//以下接口仅对老Animoji道具有效！7.4.0之后的道具无需设置
//is3DFlipH 参数是用于对3D道具的顶点镜像
FaceunityWorker.fuItemSetParamd(itemid, "is3DFlipH", param);
//isFlipExpr 参数是用于对道具内部的表情系数的镜像
FaceunityWorker.fuItemSetParamd(itemid, "isFlipExpr", param);
//isFlipTrack 参数是用于对道具的人脸跟踪位置旋转的镜像
FaceunityWorker.fuItemSetParamd(itemid, "isFlipTrack", param);
//isFlipLight 参数是用于对道具内部的灯光的镜像
FaceunityWorker.fuItemSetParamd(itemid, "isFlipLight", param);

//默认人脸识别方向，会影响所有道具的默认方向，现在在FaceunityWorker.cs中封装了一个FixRotation方法，这个方法会根据环境自动调用fuSetDefaultRotationMode来适应
public static string FixRotationWithAcceleration(Vector3 g, bool ifMirrored = false)
public static string FixRotation(bool ifMirrored = false, FUAI_CAMERA_VIEW eyeViewRot = FUAI_CAMERA_VIEW.ROT_0)
FaceunityWorker.fuSetDefaultRotationMode(0);
```

#### 5.2.3 输出图像的镜像/旋转

```c#
//Nama7.4.0后不在需要再unity层调整输出的纹理方向镜像，但是依然需要调整UI来平铺纹理
public void SelfAdjusSize();

//或者参考这个函数，根据你的需求后处理调整纹理
public Texture2D AdjustTex(Texture2D tex_origin,int SwichXY, int flipx, int flipy);
```
