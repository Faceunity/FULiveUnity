using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace FaceUnity
{
    public class LegacyCamera : ICamera
    {
        WebCamTexture webCamTexture;

        readonly bool useTexID = false;
        Color32[] webtexdata;   //用于保存每帧从相机类获取的数据
        GCHandle img_handle;    //webtexdata的GCHandle
        IntPtr p_img_ptr;   //webtexdata的指针


        public LegacyCamera()
        {
            type = ICameraType.Legacy;
            isFrontFacing = true;
            currentCameraIndex = -1;
        }

        ~LegacyCamera()
        {
            webCamTexture = null;
            if (img_handle.IsAllocated)
                img_handle.Free();
        }

        public override int CameraCount()
        {
            return WebCamTexture.devices.Length;
        }

        public override CameraDevice[] EnumerateCamera()
        {
            var devices = WebCamTexture.devices;
            var cameraDevice = new CameraDevice[devices.Length];

            for (int i = 0; i < devices.Length; ++i)
            {
                ref var cdevice = ref cameraDevice[i];
                cdevice.Name = devices[i].name;
                cdevice.IsFrontFacing = devices[i].isFrontFacing;
            }
            return cameraDevice;
        }

        public override void SelectCamera(int index)
        {
            if (index < 0 || index > WebCamTexture.devices.Length)
                return;
            if (webCamTexture != null)
            {
                webCamTexture.Stop();
                webCamTexture = null;
                currentCameraIndex = -1;
            }

            var device = WebCamTexture.devices[index];
            webCamTexture = new WebCamTexture(device.name, preferredWidth, preferredHeight, (int)preferredMaxFPS);
            webCamTexture.Play();

            {
                currentCameraIndex = index;
                name = device.name;
                isFrontFacing = device.isFrontFacing;
                rotation = webCamTexture.videoRotationAngle;
                width = webCamTexture.width;
                height = webCamTexture.height;
                horizontalFieldOfView = defaultHorizontalFieldOfView;
                cameraOutput.updateDataMode = UpdateDataMode.None;
                cameraOutput.bufferPtr = IntPtr.Zero;
                cameraOutput.texID = 0;

                if (!useTexID)
                {
                    if (img_handle.IsAllocated)
                        img_handle.Free();
                    webtexdata = new Color32[width * height];
                    img_handle = GCHandle.Alloc(webtexdata, GCHandleType.Pinned);
                    p_img_ptr = img_handle.AddrOfPinnedObject();
                }
            }
            base.SelectCamera(index);
        }

        public override void StartCamera()
        {
            if (webCamTexture && !webCamTexture.isPlaying)
                webCamTexture.Play();
            base.StartCamera();
        }

        public override bool UpdateCamera()
        {
            var result = webCamTexture && webCamTexture.isPlaying && webCamTexture.didUpdateThisFrame;
            rotation = webCamTexture.videoRotationAngle;
            width = webCamTexture.width;
            height = webCamTexture.height;
            if (useTexID)
            {
                cameraOutput.updateDataMode = UpdateDataMode.TexID;
                cameraOutput.bufferPtr = IntPtr.Zero;
                cameraOutput.texID = (int)webCamTexture.GetNativeTexturePtr();
            }
            else
            {
                if (webtexdata.Length != width * height)
                {
                    if (img_handle.IsAllocated)
                        img_handle.Free();
                    webtexdata = new Color32[width * height];
                    img_handle = GCHandle.Alloc(webtexdata, GCHandleType.Pinned);
                    p_img_ptr = img_handle.AddrOfPinnedObject();
                }
                webCamTexture.GetPixels32(webtexdata);
                cameraOutput.updateDataMode = UpdateDataMode.RGBABuffer;    //直接用纹理ID的模式会有一些方向上的问题，用这个简单点
                cameraOutput.bufferPtr = p_img_ptr;
                cameraOutput.texID = 0;
            }
            base.UpdateCamera();
            return result;
        }

        public override void StopCamera()
        {
            if (webCamTexture && webCamTexture.isPlaying)
                webCamTexture.Stop();
            base.StopCamera();
        }

        public override void OpenSettingsDialog()
        {
            //Do Nothing
        }
    }
}