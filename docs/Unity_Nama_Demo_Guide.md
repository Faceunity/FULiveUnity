# Demo running documentation-Unity 


### Updates：

2019-04-28 v6.0.0：

   - Optimize face detection, improve detection rate and improve performance
- Added the Light-Makeup function 
- Optimize the Facefusion 
- Optimize thesegmentation precision
- Tongue tracking trackface logic support, Getfaceinfo support
- Added Avatar pinch function, need FUEditor 6.0.0 or above
- Optimize Beauty filter 
- Fix mebedtls symbol conflicts
- Hairdressing and Animoji props support FUEditor v5.6.0 and above. The rest of the props are compatible with any SDK.



Project updates：

- The beauty props part of the interface has changed, please note the synchronization code
- Please refer to this document and code comments for tongue tracking
- anim_model.bytes and ardata_ex.bytes are abandoned，Please delete the relevant data and code

------
### Contents：
[TOC]

------
### 1. Introduction 
This document shows you how to run the Unity Demo of the Faceunity Nama SDK and experience the functions of the Faceunity. Features facial tracking, beauty, Animoji, props stickers, AR masks, face transfer, expression recognition, music filters, background segmentation, gesture recognition, distorting mirrors, portrait lighting, and portrait drivers. added a main interface to display the list of Faceunity products. The new version of Demo will control which products users can use based on client certificate permissions.  

------

### 

### 2. File structure
This section describes the structure of the Demo file, the various directories, and the functions of important files.

```
+FULiveUnity
  +Assets 			  	//Unity resource 
    +Examples				//example directory
      +Common					//common resource 
      	+Materials：common materials
        +NatCam：High-efficiency camera plug-ins support Android and iOS, while PCs are packaged with Unity's WebCamTexture, which is efficient.
        +Script：script
        +Shader：Shader used to render the model, for reference only.
        +Textures：the Logo for demo
        +UIImgs：Some public UI images。
      +DataOut					// FacePlugin's data output mode, using Unity for content rendering, uses NatCam to improve efficiency. Only the position, rotation, expression coefficients, etc. of the face are output for Unity rendering.
      	+Models: The human head model and the corresponding material texture.
      	+Scene: Demo scene，demoDataOut is a human head model rendering, and demoDataOut_Multiple is a multiplayer model rendering.
      	+Script: some script of demo.
		 -RenderToModel.cs: Responsible for docking the camera plug-in, inputting and outputting image data, and managing the rotation scaling of the output texture.
		 -StdController.cs: Responsible for controlling the position, rotation and expression coefficient of the human head.
		 -EyeController.cs: Responsible for controlling the position and rotation of the eye.
		 -UIManagerForDataOut.cs: UI controller for the DataOut scene.
		 -UIManagerForDataOut_Multiple.cs: The UI controller of the DataOut_Multiple scene is also responsible for multi-person model scheduling.
      +Simple					//The simplest use case for FacePlugin, directly using Unity's own WebCamTexture to simplify the code structure.
       +Scene: demoSimple
	   +Script: some script of demo.
		 -RenderSimple.cs: If you need to enter image data obtained by other channels, please refer to this function.
		 -UIManagerSimple.cs: The UI controller of the simple scene, registered the switch camera button, manages the face detection flag.
      +TexOut					//FacePlugin's texture output mode, using the Faceunity Nama SDK for content rendering, uses NatCam for efficiency. Directly output the data rendered by the plugin, you can use the attached binary props file.
      	+Resources: Binary files for all items and corresponding UI files.
	    +Scene: demoTexOut。
	    +Script: some script of demo.
		 -RenderToTexture.cs: Responsible for docking camera plug-ins, inputting and outputting image data, loading and unloading props.
		 -UIManagerForTexOut.cs: The UI controller for texture output mode works with RenderToTexture to show the functionality of all the props.
		 -ItemConfig.cs: A configuration file for information such as the binary file of the item and the path of the UI file.
    +Plugins				//SDK file directory
    +Script					//Core code file directory
      -FaceunityWorker.cs：Responsible for initializing the faceunity plugin and introducing the C++ interface. After the initialization is completed, the face tracking data is updated every frame.
    +StreamingAssets		//data file directory
      -v3.bytes：SDK data file, the lack of this file will cause initialization failure
      -tongue.bytes：Tongue tracking necessary files
      -EnableTongueForUnity.bytes：In some cases, the file that needs to be loaded to get the tongue tracking data
  +docs					//doc 
  +ProjectSettings   	//Unity project config
  -readme.md			//readme
  
```

------
### 3. Demo Running  

#### 3.1 Develop environment
##### 3.1.1 Platform
```
Windows、Android、iOS（9.0 or above）、Mac
```
##### 3.1.2 Develop environment
```
Unity5.4.6f3 or above
```

#### 3.2 Preparing 
- [download demo](https://github.com/Faceunity/FULiveUnity)
- Get certificates:
  1. call **(86)-0571-88069272** 
  2. send mail to **marketing@faceunity.com** 
#### 3.3 Configurations

- Copy the data in the certificate file to the LICENSE input box of the Inspector panel of the FaceunityWorker object in the scene, and save the scene (other scenes are the same).

![](imgs/img0.jpg)

#### 3.4 Compile and running

- Click the play button to run in UnityEditor, or open BuildSettings, select the scene you want to see, click Build or Build And Run to compile the installation package for the platform you want.

![](imgs\img1.jpg)

------
### 4.FAQ 
- Please select 9.0 or higher system version when compiling iOS.
- **Since Github does not support uploading files larger than 100MB, the iOS library is compressed, please decompress it yourself!**
- The Unity project import may cause the platform information of some native libraries to be lost. If the error is not found when running or compiling, please manually modify the platform information of the corresponding library. The native libraries in this case are:
  - Assets\Plugins
  - Assets\Examples\Common\NatCam\Core\Plugins
  ![](imgs\img2.jpg)
