# Demo运行说明文档-Unity 
级别：Public
更新日期：2022-11-11
SDK版本: 8.4.0

------

## 最新更新内容：

2022-11-11 v8.4.0:

- 美型优化下巴功能，参数更新为intensity_chin_mode2。
- 美型优化瘦脸功能，参数不变。

2021-11-15 v8.0.0:

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
7. Unity版本新增离线鉴权

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

文档：

   - [美颜道具功能文档](美颜道具功能文档.md)

   - [美妆道具功能文档](美妆道具功能文档.md)

工程案例更新：

- 增加新的接口支持yuv420输入图像数据
- Nama8.0.0重构了相机模块代码，采用新的nativeCamera,新相机直接从C++层去调用相机，区别于之前通过Java中间层，效率更高

- Nama7.4.0重构了Unity/Nama中间层的代码，部分接口被删除，同时跟踪帧率不再跟随相机帧率，而是Unity渲染帧率，这样可以提高整体流畅度。
- Nama7.4.0优化了输入输出数据的旋转镜像问题，具体请查看集成指导文档5.2
- 由于Nama7.0改动巨大，接口变动也较大，但是接口调用方式基本不变，接口变动详见Unity_Nama_API_参考文档
- 由于Nama 6.6的内部机制更新，AI和渲染分离，现在Nama运行在FU_Mode_RenderItems模式下（渲染Nama道具）时，如果不加载任何道具，Nama也不会运行任何AI逻辑，此时无法进行人脸检测等操作，也无法拿到相关数据！！！因此本工程案例里在DataOut场景和Simple场景中都添加了自动加载一个空道具的逻辑，以应对出现的问题。
- 当Nama运行在FU_Mode_TrackFace模式下时，无需加载任何道具，会自动跑人脸识别的AI逻辑
- Nama6.6同时也带来了道具加载卸载机制的更新，新的道具加载卸载接口已经全部都是同步接口，调用后立即执行，没有异步没有协程，简化了道具加载卸载的逻辑复杂度。道具载入可以用多线程调用以降低主线程被卡住的风险（fuCreateItemFromPackage）
- 本次更新添加了一个C#封装函数以更新默认道具/跟踪方向，这个函数会根据当前平台环境、相机是否镜像以及重力感应方向，自动设置道具和跟踪的默认方向，在场景中需要每帧调用以适应重力感应。具体描述请看API文档，具体应用请看本函数在Demo中的引用。

------
### 目录：
本文档内容目录：

[TOC]

------
### 1. 简介 
本文档旨在说明如何将Faceunity Nama SDK的Unity Demo运行起来，体验Faceunity Nama SDK的功能。FULiveUnity 是集成了 Faceunity 面部跟踪、美颜、Animoji、道具贴纸、AR面具、表情识别、音乐滤镜、背景分割、手势识别、哈哈镜功能的Demo。Demo有一个展示Faceunity产品列表的主界面，Demo将根据客户证书权限来控制用户可以使用哪些产品。  

------
### 2. Demo文件结构
本小节，描述Demo文件结构，各个目录，以及重要文件的功能。

```
+FULiveUnity
  +Assets 			  	//Unity资源目录
    +Examples				//示例目录
      +Common					//Demo的公共资源。
      	+Materials：一些公共材质。
        +NatCam：高效率的摄像机插件，支持安卓和iOS，而PC则是封装了Unity的WebCamTexture，效率一般。
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
### 3. 运行Demo 

#### 3.1 开发环境
##### 3.1.1 支持平台
```
Windows、Android、iOS（9.0以上系统）、Mac
```
##### 3.1.2 开发环境
```
Unity2018.4.23f1 及以上
```

#### 3.2 准备工作 
- [下载demo代码](https://github.com/Faceunity/FULiveUnity)
- 获取证书:
  1. 拨打电话 **0571-89774660** 
  2. 发送邮件至 **marketing@faceunity.com** 进行咨询。

#### 3.3 相关配置

- 将证书文件中的数据复制到场景中FaceunityWorker物体的Inspector面板的LICENSE输入框内，并保存场景（其他场景同理）。

![](imgs/img0.jpg)

#### 3.4 编译运行

- 点击播放按钮在UnityEditor里运行，或者打开BuildSettings，选择你想看的场景，点击Build或者Build And Run编译出你想要的平台的安装包。

![](imgs\img1.jpg)

------
### 4. 常见问题 
- iOS编译时请选择**9.0**以上系统版本。
- **因Github不支持上传100MB以上的文件，iOS的库经过压缩，使用时请自行解压！**
- Unity工程导入可能会导致部分native库的平台信息丢失，如果运行或者编译时报错提示找不到库，请手动修改相应库的平台信息。本案例中的native库有:
  - Assets\Plugins
    ![](imgs\img2.jpg)
- iOS平台编译Link可能报错，缺少某些framework，请手动添加CoreML.framework以及Metal.framework
- 使用本SDK版本7.1.2以后时，iOS平台请在Xcode工程中关闭BitCode，7.1.2后的版本的SDK iOS版不再包含BitCode信息，因此体积大大减小。

