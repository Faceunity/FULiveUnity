# Unity Nama C# API Reference
------
### Updates：

2021-11-15 v8.0.0：

1. Added fuSetFaceDetParam function to set face detection parameters.
2. Updated fuSetup function

------
### Contents：
[TOC]

------
### 1. Introduction 
This document is the infrastructure layer interface for FaceUnity face tracking and video effects development kit .The Nama API in this document is an interface in FaceunityWorker.cs and can be used directly for development on Unity. Among them, some Unity demo scenes also provide some secondary packaged interfaces, which is closer to the actual development than the interface in this article.

The SDK used by Unity encapsulates a layer of GL call layer on the original Nama SDK. This package enables the Nama SDK's GL environment to be synchronized with Unity's GL environment，refer to the link  (https://docs.unity3d.com/Manual/NativePluginInterface.html)。

In the following text, the original Nama SDK is referred to as Nama, and the GL call layer is called FacePlugin, which is called UnityNamaSDK.

All calls related to the SDK require sequential execution in the same thread and do not support multithreading. In order to synchronize UnityNamaSDK and Unity's GL environment, some interfaces will not take effect immediately after the call, but wait until the end of the current Unity life cycle. The coroutine executed every frame opened after the initialization is completed will call the GL event to execute the current Unity life cycle. Some of the interfaces that are called are described in the API content.

If you need to use the rendering function of the SDK, you need Unity to open the OpenGL rendering mode. If it is not turned on or turned on incorrectly, it will cause a crash. Our environment requirements for OpenGL are GLES 2.0 and above. For specific calling methods, refer to FULiveUnity Demo. If you don't need to use the SDK's rendering capabilities, you can ask how technical support can call Nama directly.

The infrastructure layer interface is classified into five categories according to the role of logic: initialization, propsloading, main running interface, destruction, functional interface, C# auxiliary interface.

------
### 2. APIs
#### 2.1 Initialization
##### fu_Setup 
Initialize the system environment, load the system data, and perform network authentication. Must be executed before calling the SDK's other interfaces, otherwise it will cause a crash.

```c#
public static extern int fu_Setup(IntPtr v3buf, int v3buf_sz, IntPtr licbuf, int licbuf_sz);
```

__Parameter:__

*v3buf*:  Pointer to binary data read in v3.bytes

*v3buf_sz*:  length of v3.bytes

*licbuf*：the text data of the certificate, the divided array data is converted into sbyte format

*licbuf_sz[in]*：sbyte array length

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### jc_part_inited 
The return value indicates whether UnityNamaSDK initializes PART1 successfully. This interface is mainly used to prevent the secondary initialization of UnityNamaSDK, which will cause the program to crash.

```c#
public static extern int jc_part_inited();
```

__Parameter:__

Null

__Return Value:__

When 1 is returned for success, 0 is for failure.

__Comment:__

This interface will take effect immediately.

------

##### fu_GetNamaInited 
The return value indicates whether UnityNamaSDK initialized PART2 successfully. When both PART1 and PART2 are successful, the UnityNamaSDK is actually initialized successfully.

```c#
public static extern int fu_GetNamaInited();
```

__Parameter:__

Null

__Return Value:__

When 1 is returned for success, 0 is for failure.

__Comment:__

This interface will take effect immediately.

------

##### ~~fu_LoadExtendedARData 函数~~

**abandoned**

```c#
public static extern int fu_LoadExtendedARData(IntPtr databuf, int databuf_sz);
```

------

##### ~~fu_LoadAnimModel 函数~~

**abandoned**

```c#
public static extern int fu_LoadAnimModel(IntPtr databuf, int databuf_sz);
```

------

##### fu_LoadTongueModel 

the required data files for loading the tongue tracking 

```c#
public static extern int fu_LoadTongueModel(IntPtr databuf, int databuf_sz);
```

__Parameter:__

*databuf*:  pointer to binary data read in tongue.bytes
*databuf_sz*:  length of tongue.bytes

__Return Value:__

Null

__Comment:__

This interface will be delayed.

---



#### 2.2 Main running interface

##### SetImage 

One of the image data input interfaces. Input buffer array in RGBA format, the most versatile, can be used on all platforms.

```c#
 public static extern int SetImage(IntPtr imgbuf,int flags, bool isbgra, int w, int h);
```

__Parameter:__

*imgbuf*: buffer array pointer in RGBA format
*flags*: data input flag
*isbgra*: 0 for RGBA format，1 for BGRA format
*w*: the width of the image 
*h*: the height of the image

```
flags: FU_ADM_FLAG_FLIP_X = 32;
       FU_ADM_FLAG_FLIP_Y = 64; only flips the prop rendering, and does not flip the entire image
```

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### SetDualInput 

One of the image data input interfaces. At the same time, input the buffer array and texture ID of the NV21 format, usually on the Android device, and use the native camera plug-in to obtain the corresponding data for use, which is the most efficient.

```c#
 public static extern int SetDualInput(IntPtr nv21buf, int texid, int flags, int w, int h);
```

__Parameter:__

*nv21buf*: buffer array pointer in NV21 format
*texid*: texture ID in RGBA format
*flags*: data input flag，the same parameter to SetImage
*w*: the width of the image
*h*: the height of the image

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### SetNV21Input 

One of the image data input interfaces. Enter the buffer array in NV21 format, usually on an Android device, and use the native camera plugin to get the corresponding data to use.

```c#
 public static extern int SetNV21Input(IntPtr nv21buf, int flags, int w, int h);
```

__Parameter:__

*nv21buf*: buffer array pointer in NV21 format
*flags*: data input flag，the same parameter to SetImage
*w*: the width of the image
*h*: the height of the image

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### SetImageTexId 

One of the image data input interfaces. Enter the GL texture ID, which cannot be used in some special GL environments, but to a certain extent the performance is higher than Image.

```c#
 public static extern int SetImageTexId(int texid, int flags, int w, int h);
```

__Parameter:__

*texid*: texture ID in RGBA format
*flags*: data input flag，the same parameter to SetImage
*w*: the width of the image
*h*: the height of the image

__Return Value:__

Null

__Comment:__

This interface will be delayed.

---

##### fu_SetRuningMode 

Set the operating mode of UnityNamaSDK

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

__Parameter:__

*runningMode*: runnig mode

```
- FU_Mode_None：stop rendering
- FU_Mode_RenderItems：enable face tracking and rendering props
- FU_Mode_Beautification：only enable beautififation
- FU_Mode_Masked ：After setting the Mask with fu_setItemIds, this mode will take effect.
- FU_Mode_TrackFace：Only enable face tracking
```

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### fu_GetRenderEventFunc 

**The core logic of the SDK call** : the value returned by this function can be used with the GL.IssuePluginEvent to run the SDK.，for more details please refer to(https://docs.unity3d.com/Manual/NativePluginInterface.html) 

```c#
 public static extern IntPtr fu_GetRenderEventFunc();
 GL.IssuePluginEvent(fu_GetRenderEventFunc(), 1);	//example
```

__Parameter:__

Null

__Return Value:__

Pointer to the SDK runtime function

__Comment:__

This interface will take effect immediately.

---

##### fu_GetNamaTextureId 

Get the texture ID of the image rendered by this plugin.

```c#
 public static extern int fu_GetNamaTextureId();
```

__Parameter:__

Null

__Return Value:__

GL texture ID

__Comment:__

This interface will take effect immediately.

---

##### fu_GetFaceInfo 

Get face tracking information.

```c#
 public static extern int fu_GetFaceInfo(int face_id, IntPtr ret, int szret, [MarshalAs(UnmanagedType.LPStr)]string name);
```

__Parameter:__

*face_id*: current the number id of faces
*ret*: pointer to an array for receiving data
*szret*: the length of the array used to receive the data
*name*: parameter name to be obtained

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.
For the specific calling method and available parameters of this interface, please refer to the related code in FaceunityWorker.cs.

------

##### fu_SetTongueTracking 

When FURuningMode is FU_Mode_RenderItems, EnableTongueForUnity.bytes is loaded to enable tongue tracking.

  When FURuningMode is FU_Mode_TrackFace, fu_SetTongueTracking(1) is called to enable tongue tracking. Note that you need to set it once after switching to FU_Mode_TrackFace! ! !

```c#
 public static extern int fu_SetTongueTracking(int enable);
```

__Parameter:__

*enable*: 0 for off，1 for on

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### fu_SetFaceDetParam 

```
\brief Set a face detector parameter.
\param detector is the detector context, currently it is allowed to set to NULL, i.e., globally set all contexts.
\param name is the parameter name, it can be:
	"use_new_cnn_detection": 1 if the new cnn-based detection method is used, 0 else
	"other_face_detection_frame_step": if one face already exists, then we detect other faces not each frame, but with a step,default 10 frames.
	if use_new_cnn_detection == 1, then
		"min_facesize_small", int[default=18]: minimum size to detect a small face; must be called **BEFORE** fuSetup
		"min_facesize_big", int[default=27]: minimum size to detect a big face; must be called **BEFORE** fuSetup
		"small_face_frame_step", int[default=5]: the frame step to detect a small face; it is time cost, thus we do not detect each frame
		"use_cross_frame_speedup", int[default=0]: perform a half-cnn inference each frame to speedup
	else
		"scaling_factor": the scaling across image pyramids, default 1.2f
		"step_size": the step of each sliding window, default 2.f
		"size_min": minimal face supported on 640x480 image, default 50.f
		"size_max": maximal face supported on 640x480 image, default is a large value
		"min_neighbors": algorithm internal, default 3.f
		"min_required_variance": algorithm internal, default 15.f
\param value points to the new parameter value, e.g., 
	float scaling_factor=1.2f;
	dde_facedet_set(ctx, "scaling_factor", &scaling_factor);
	float size_min=100.f;
	dde_facedet_set(ctx, "size_min", &size_min);
```

```c#
 public static extern int fu_SetFaceDetParam([MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value);
```

__Parameter:__

*name*: parameter name 
*value*: Parameter pointer

__Return Value:__

Null

__Comment:__

This interface will be delayed.

---

##### fu_OnDeviceLost 

Reset Nama's GL rendering environment

```c#
 public static extern void fu_OnDeviceLost();
```

__Parameter:__

Null

__Return Value:__

无

__Comment:__

This interface will take effect immediately.

------

##### fu_OnCameraChange 

Reset face tracking in Nama (not involving GL)

```c#
 public static extern void fu_OnCameraChange();
```

__Parameter:__

Null

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

------

##### ClearImages 

Reset FacePlugin's GL rendering environment

```c#
 public static extern void ClearImages();
```

__Parameter:__

Null

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

---



#### 2.3 load and destroy props

##### fu_setItemDataFromPackage 

This interface is used to load the item files adapted by UnityNamaSDK, such as beautification, stickers, Animoji, etc., but it is not recommended to call this interface directly, but use the packaged fu_CreateItemFromPackage

```c#
private static extern void fu_setItemDataFromPackage(IntPtr databuf, int databuf_sz);
```

__Parameter:__

*databuf*:  pointer to binary data read in the item file
*databuf_sz*:  length of the item file

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### fu_getItemIdxFromPackage 

Get the index of the last loaded item (also known as ItemID)

```c#
public static extern int fu_getItemIdxFromPackage();
```

__Parameter:__

Null

__Return Value:__

Index of the last loaded item

__Comment:__

This interface will take effect immediately.

---

##### fu_setItemIds 

When the props are loaded, the props will not be effective immediately to start rendering. Instead, the item ID of the item to be rendered is input through this interface to enable the rendering of the corresponding item. If the corresponding ItemID is not input, the corresponding item will not be rendered.

```c#
public static extern int fu_setItemIds(IntPtr idxbuf, int idxbuf_sz, IntPtr mask);
```

__Parameter:__

*idxbuf*:  an array of all the ItemIDs of the items that need to be rendered
*idxbuf_sz*:  length of the array
*mask*:  multi-faces and multiple props, each face uses different props

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### fu_DestroyItem 

Destroy specified items

```c#
public static extern void fu_DestroyItem(int itemid);
```

__Parameter:__

*itemid*:  ItemID returned after the item is loaded

__Return Value:__

Null

__Comment:__

This interface will be delayed.
If this function is called multiple times within the same frame, only the last call will take effect.

------

##### fu_DestroyAllItems 

Destroy all currently loaded items

```c#
public static extern void fu_DestroyAllItems();
```

__Parameter:__

Null

__Return Value:__

Null

__Comment:__

This interface will be delayed.
If this function is called multiple times within the same frame, only the last call will take effect.

---

##### fu_ItemSetParamd 

Set the parameter (double) to the prop

```c#
public static extern int fu_ItemSetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, double value);
```

__Parameter:__

*itemid*: Need to set the parameter of the item ID
*name*: Parameter name
*value*: Parameter

__Return Value:__

When it returns 1, it indicates success, and 0 indicates failure.

__Comment:__

This interface will take effect immediately.

------

##### fu_ItemSetParamdv 

Set parameters for the item (double array)

```c#
public static extern int fu_ItemSetParamdv(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int value_sz);
```

__Parameter:__

*itemid*: Need to set the parameter of the item ID
*name*: Parameter name
*value*: Parameter pointer 
*value_sz*: parameter length

__Return Value:__

When it returns 1, it indicates success, and 0 indicates failure.

__Comment:__

This interface will take effect immediately.

------

##### fu_ItemSetParams 

Set parameters for the item (string)

```c#
public static extern int fu_ItemSetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string value);
```

__Parameter:__

*itemid*: Need to set the parameter of the item ID
*name*: Parameter name 
*value*: Parameter

__Return Value:__

When it returns 1, it indicates success, and 0 indicates failure.

__Comment:__

This interface will take effect immediately.

------

##### fu_ItemGetParamd 

Get a parameter of the specified item (double)

```c#
public static extern double fu_ItemGetParamd(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name);
```

__Parameter:__

*itemid*: Need to set the parameter of the item ID
*name*: Parameter name

__Return Value:__

Parameters to be obtained

__Comment:__

This interface will take effect immediately.

------

##### fu_ItemGetParams 

Get a parameter of the specified item (string)

```c#
public static extern int fu_ItemGetParams(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]byte[] buf, int buf_sz);
```

__Parameter:__

*itemid*: Need to set the parameter of the item ID
*name*: Parameter name 
*buf*: Parameter pointer
*buf_sz*: Parameter length

__Return Value:__

When it returns 1, it indicates success, and 0 indicates failure.

__Comment:__

This interface will take effect immediately.

---

##### fu_CreateTexForItem 

Set textures for props

```c#
public static extern int fu_CreateTexForItem(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr value, int width, int height);
```

__Parameter:__

*itemid*: Need to set the texture of the item ID
*name*: The name of the texture to be set
*value*: Pointer to the buffer of texture in RGBA format
*width*: Texture width
*height*: Texture height

__Return Value:__

When it returns 1, it indicates success, and 0 indicates failure.

__Comment:__

This interface will take effect immediately.

------

##### fu_DeleteTexForItem 

Destroy a texture of the specified item

```c#
public static extern int fu_DeleteTexForItem(int itemid, [MarshalAs(UnmanagedType.LPStr)]string name);
```

__Parameter:__

*itemid*: Need to destroy the texture of the item ID
*name*: The name of the texture that needs to be destroyed

__Return Value:__

When it returns 1, it indicates success, and 0 indicates failure.

__Comment:__

This interface will take effect immediately.

------



#### 2.4 Functional interface

##### fu_GetModuleCode 

Obtain the certificate authentication result, a total of 64bit flag.

```c#
public static extern int fu_GetModuleCode(int i);
```

__Parameter:__

*i*: Enter 0 to get the first 32bit, input 1 to get the 32bit

__Return Value:__

The authentication result flag.

__Comment:__

This interface will take effect immediately.

---

##### fu_SetExpressionCalibration 

Control automatic expression correction.

```c#
public static extern void fu_SetExpressionCalibration(int enable);
```

__Parameter:__

*enable*:  0 for off，1 for on

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

------

##### fu_SetDefaultRotationMode

Set the default RotationMode, which is the default rendering direction.

```c#
public static extern void fu_SetDefaultRotationMode(int i);
```

__Parameter:__

*i*:  Enter an integer from 0 to 3. For specific applications, please refer to the getRotateEuler function in StdController.cs in UnityDemo.

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

------

##### fu_SetASYNCTrackFace 

enable asynchronous tracking, some models can improve performance, but some models have degraded performance.

```c#
public static extern int fu_SetASYNCTrackFace(int i);
```

__Parameter:__

*i*: 0 for off，1 for on，the default setting is off

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

------

##### fu_SetDefaultOrientation 

Set the default face detection direction. Correct settings can improve detection speed and performance.

```c#
 public static extern void fu_SetDefaultOrientation(int rmode);
```

__Parameter:__

*rmode*: An integer from 0 to 3 with the same meaning as fu_SetDefaultRotationMode

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

---

##### fu_SetFocalLengthScale 

Set the Scale for Nama rendering FOV.

```c#
 public static extern void fu_SetFocalLengthScale(float scale);
```

__Parameter:__

*scale*: This number needs to be greater than 0 to adjust the FOV of Nama

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

------

##### SetUseNatCam 

UnityDemo uses NatCam to improve camera efficiency, while modifying its code to further improve efficiency with FacePlugin, but if customers need to use their own camera plug-ins, they need to call this interface to close the relevant optimization code in FacePlugin to prevent anomalies. This switch only works on the Android platform, other platforms do not need to care about this issue.

```c#
 public static extern void SetUseNatCam(int enable);
```

__Parameter:__

*enable*: 0 for off，1 for on，the default setting is on

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

---

##### SetFlipTexMarkX 

Flipping the input texture only takes effect when using SetDualInput. Some Android platforms nv21buf and tex have inconsistent directions. You can use this interface to set the XY X-axis image.

```c#
 public static extern int SetFlipTexMarkX(bool mark);
```

__Parameter:__

*mark*: 0 means not flipping, 1 means flipping X axis

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### SetFlipTexMarkY 

Flipping the input texture is only effective when using SetDualInput. Some Android platforms have inconsistent directions between nv21buf and tex. You can use this interface to set the Y-axis image of tex.

```c#
 public static extern int SetFlipTexMarkY(bool mark);
```

__Parameter:__

*mark*:0 means no flipping, 1 means flipping Y axis

__Return Value:__

Null

__Comment:__

This interface will be delayed.

------

##### SetPauseRender 

Manually temporarily mask the rendering of UnityNamaSDK. After calling this function, UnityNamaSDK will temporarily stop parsing the input image data, and there is still image data input.

```c#
 public static extern void SetPauseRender(bool ifpause);
```

__Parameter:__

*ifpause*: 0 means no pause, 1 means pause

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

------

##### fu_IsTracking 

Get how many faces in the image after the current parsing is completed

```c#
 public static extern int fu_IsTracking();
```

__Parameter:__

Null

__Return Value:__

The number of faces, this value is affected by fu_SetMaxFaces

__Comment:__

This interface will take effect immediately.

------

##### fu_SetMaxFaces 

Set the maximum number of faces detected

```c#
 public static extern int fu_SetMaxFaces(int num);
```

__Parameter:__

*num*: Maximum number of faces

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.
**This interface is called once before each call (GL.IssuePluginEvent(fu_GetRenderEventFunc(), 1);).**

---

##### fu_GetFaceIdentifier 

Enter the current Nth face and get the unique ID of the face.

```c#
 public static extern int fu_GetFaceIdentifier(int face_id);
```

__Parameter:__

*face_id*: fu_IsTracking() will return the current total of N faces, this number needs to be satisfied (0 <= face_id < N)

__Return Value:__

Enter the number N to return the ID unique to the current Nth face.

__Comment:__

This interface will take effect immediately.

------

##### ~~fu_SetQualityTradeoff 函数~~

**abandoned**

```c#
 public static extern void fu_SetQualityTradeoff(float num);
```

------

##### fu_EnableLog 

enable the Log of the FacePlugin layer. The PC platform needs to open the Unity console by itself, or use the RegisterDebugCallback to open the Unity internal log.

```c#
 public static extern void fu_EnableLog(bool isenable);
```

__Parameter:__

*isenable*: 0 for off，1 for on

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

---

##### RegisterDebugCallback 

Use with fu_EnableLog, register a C# delegate to process the returned log information, generally use Debug.Log directly in UnityConsole.

```c#
 private static extern void RegisterDebugCallback(DebugCallback callback);
```

__Parameter:__

*callback*: Callback commission

__Return Value:__

Null

__Comment:__

This interface will take effect immediately.

---

##### fu_GetVersion 

get Nama version

```c#
 public static extern IntPtr fu_GetVersion();
 Marshal.PtrToStringAnsi(fu_GetVersion());	//example
```

__Parameter:__

Null

__Return Value:__

Nama version

__Comment:__

This interface will take effect immediately.

------

##### fu_GetSystemError

Get the error that occurred in the last Nama

```c#
 public static extern int fu_GetSystemError();
```

__Parameter:__

Null

__Return Value:__

The code of the error that occurred in the previous Nama

__Comment:__

This interface will take effect immediately.

------

##### fu_GetSystemErrorString 

Convert the error code to the corresponding string

```c#
 public static extern IntPtr fu_GetSystemErrorString(int code);
 Marshal.PtrToStringAnsi(fu_GetSystemErrorString(fu_GetSystemError()));		//example
```

__Parameter:__

*code*: error code

__Return Value:__

String pointer

__Comment:__

This interface will take effect immediately.

---



#### 2.5 C# end auxiliary interface

##### fu_CreateItemFromPackage

enable this coroutine to load the Nama props. This item will automatically wait for two frames, and the item will be loaded after the item is actually loaded.

```c#
 public static IEnumerator fu_CreateItemFromPackage(IntPtr databuf, int databuf_sz)
```

__Parameter:__

*databuf*: Pointer to the array of item files
*databuf_sz*: Length of the array of item files

__Return Value:__

Null

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

------

##### InitCFaceUnityCoefficientSet 

Instantiate all classes that get face information. These examples contain fu_GetFaceInfo for getting various face information.

```c#
 void InitCFaceUnityCoefficientSet(int maxface)
```

__Parameter:__

*maxface*: the max number of faces

__Return Value:__

Null

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

------

##### Start 

This coroutine will initialize the entire UnityNamaSDK. CallPluginAtEndOfFrames is automatically turned on when initialization is complete.

```c#
 IEnumerator Start()
```

__Parameter:__

Null

__Return Value:__

Null

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

------

##### CallPluginAtEndOfFrames 

enable this coroutine, automatically call UnityNamaSDK at the end of each frame to recognize the face and render the current image frame. If the related Parameter (EnableExpressionLoop) is enabled, the recognized face information will be automatically obtained at the same time.

```c#
 private IEnumerator CallPluginAtEndOfFrames()
```

__Parameter:__

Null

__Return Value:__

Null

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

------

##### DebugMethod 

Use with RegisterDebugCallback to enter the returned log information.

```c#
 private static void DebugMethod(string message)
```

__Parameter:__

*message*: log information

__Return Value:__

Null

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

------

##### OnApplicationQuit 

Clean up the relevant data inside GCHandle and UnityNamaSDK when the application exits.

```c#
 private void OnApplicationQuit()
```

__Parameter:__

Null

__Return Value:__

Null

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

------

##### EnvironmentCheck

To check if the current rendering environment is OpenGL, this plugin only supports rendering in OpenGL rendering environment.

```c#
 bool EnvironmentCheck()
```

__Parameter:__

Null

__Return Value:__

False means the test fails, true means the test passes.

__Comment:__

This is a C# function, not a UnityNamaSDK interface.

---



### 3. FAQ 

#### 3.1 What is the input of the parameter type IntPtr?

The IntPtr type is usually not used in C#, but when you need to interact with Native code, you can use this type instead of the pointer.

There are many ways to get IntPtr. It is recommended to use GCHandle to get a pointer to an array, or use Marshal.AllocHGlobal to create a new one. For details, please refer to FULiveUnity Demo.

