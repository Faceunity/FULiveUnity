using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FaceUnity
{
    public class NativeCamera : ICamera
    {
        public enum ImageFormat : int
        {
            RGB_BITMAP = 0x00000001,
            Y_8 = 0x00000002,
            YUV_420_888 = 0x00000023,
            YUV_422_888 = 0x00000024,
            BGR_BITMAP = 0x00000006,
            RGBA_BITMAP = 0x00000007,
            BGRA_BITMAP = 0x00000008
        }

        public enum VideoFormat
        {
            UNKNOWN = 0,
            BGRA = 1,
            YUV_420_BT601_1 = 11,
            YUV_420_BT601_F = 12,
            YUV_420_BT709 = 13,
            YUV_422_BT601_1 = 21,
            YUV_422_BT601_F = 22,
            YUV_422_BT709 = 23
        };

        [StructLayout(LayoutKind.Sequential)]
        struct CameraProps
        {
            public IntPtr name;
            public int frontFacing;
            public int numStreamProps;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct StreamProps
        {
            public int width;
            public int height;
            public int videoFormat;
            public int numFrameRateRange;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct FrameRateRange
        {
            public float minFPS;
            public float maxFPS;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FrameProps
        {
            public ulong timeStampInMicroseconds;
            public int rotation;
            public float horizontalFieldOfView;
            public IntPtr bgra;
            public IntPtr yuv;
            public IntPtr y;
            public IntPtr u;
            public IntPtr v;
        }
        public struct VideoDevice
        {
            public string Name;
            public bool IsFrontFacing;
            public VideoStream[] Streams;
        }

        public struct VideoStream
        {
            public int Width;
            public int Height;
            public VideoFormat Format;
            public VideoFrameRateRange[] FrameRateRanges;
        }

        public struct VideoFrameRateRange
        {
            public float MinFPS;
            public float MaxFPS;
        }

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        public const string Assembly = "NativeCamera";
#elif UNITY_IOS
    public const string Assembly = "__Internal";
#else
        public const string Assembly = "NativeCamera";
#endif

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern void fu_camera_handler_init();

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern void fu_camera_handler_close();

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern int fu_camera_handler_enumerate_camera();

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr fu_camera_handler_get_camera_props(
            int device_index,
            out CameraProps props);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr fu_camera_handler_get_stream_props(
            int device_index,
            int stream_index,
            out StreamProps props);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr fu_camera_handler_get_frame_rate_range(
            int device_index,
            int stream_index,
            int frame_rate_index,
            out FrameRateRange props);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern int fu_camera_handler_open_camera(
            int device_index,
            int stream_index,
            int frame_rate_index,
            float min_fps,
            float max_fps);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern int fu_camera_handler_close_camera(
            int device_index);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern void fu_camera_handler_set_active(
                int device_index,
                int active);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern int fu_camera_handler_take_snapshot(
                int device_index,
                out FrameProps out_props);

        [DllImport(Assembly, CallingConvention = CallingConvention.Cdecl)]
        static extern void fu_camera_handler_open_settings_dialog(
                int device_index,
                IntPtr owner_handle);


        VideoDevice[] videoDevices;

        public NativeCamera()
        {
            type = ICameraType.Native;
            isFrontFacing = true;
            currentCameraIndex = -1;
            //需要确定得到相机权限才能运行初始化函数
            fu_camera_handler_init();
        }

        ~NativeCamera()
        {
            fu_camera_handler_close();
        }

        public override int CameraCount()
        {
            if (videoDevices != null) return videoDevices.Length;
            return 0;
        }

        public override CameraDevice[] EnumerateCamera()
        {
            var numCameras = fu_camera_handler_enumerate_camera();
            if (numCameras <= 0) return null;
            var cameraDevice = new CameraDevice[numCameras];
            videoDevices = new VideoDevice[numCameras];

            CameraProps cameraProps;
            StreamProps streamProps;
            FrameRateRange frameRateRange;

            for (int i = 0; i < numCameras; ++i)
            {
                ref var vdevice = ref videoDevices[i];
                ref var cdevice = ref cameraDevice[i];
                fu_camera_handler_get_camera_props(i, out cameraProps);
                vdevice.Name = Util.UTF8ToString(cameraProps.name);
                vdevice.IsFrontFacing = cameraProps.frontFacing != 0;
                vdevice.Streams = new VideoStream[cameraProps.numStreamProps];

                cdevice.Name = vdevice.Name;
                cdevice.IsFrontFacing = vdevice.IsFrontFacing;

                for (int j = 0; j < cameraProps.numStreamProps; ++j)
                {
                    ref var stream = ref vdevice.Streams[j];
                    fu_camera_handler_get_stream_props(i, j, out streamProps);
                    stream.Width = streamProps.width;
                    stream.Height = streamProps.height;
                    stream.Format = (VideoFormat)streamProps.videoFormat;
                    stream.FrameRateRanges = new VideoFrameRateRange[streamProps.numFrameRateRange];

                    for (int k = 0; k < streamProps.numFrameRateRange; ++k)
                    {
                        ref var frameRate = ref stream.FrameRateRanges[k];
                        fu_camera_handler_get_frame_rate_range(i, j, k, out frameRateRange);
                        frameRate.MinFPS = frameRateRange.minFPS;
                        frameRate.MaxFPS = frameRateRange.maxFPS;
                    }
                }
            }
            return cameraDevice;
        }
        public override void SelectCamera(int index)
        {
            if (index < 0 || index > videoDevices.Length)
                return;
            if (currentCameraIndex >= 0)
            {
                fu_camera_handler_close_camera(currentCameraIndex);
                currentCameraIndex = -1;
            }

            ref var device = ref videoDevices[index];

            int selectedWidth = -1;
            int selectedHeight = -1;
            int selectedSizeMatchScore = -1;
            int selectedStreamIndex = -1;
            int selectedFrameRateIndex = -1;
            float selectedMinFPS = 0.0f;
            float selectedMaxFPS = 0.0f;

            //  Caveat: FaceTime HD Camera only supports the first stream
            bool shouldSelectFirstStream = false;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            shouldSelectFirstStream = device.Name.StartsWith("FaceTime HD Camera");
#endif

            for (int i = 0; i < device.Streams.Length; ++i)
            {
                ref var stream = ref device.Streams[i];
                if (stream.Format == VideoFormat.UNKNOWN)
                {
                    continue;
                }

                int sizeMatchScore = 0;
                if (stream.Width == preferredWidth && stream.Height == preferredHeight)
                {
                    sizeMatchScore = 16777216;
                }
                else
                {
                    sizeMatchScore = stream.Width + stream.Height;
                }

                if (sizeMatchScore > selectedSizeMatchScore)
                {
                    selectedWidth = stream.Width;
                    selectedHeight = stream.Height;
                    selectedSizeMatchScore = sizeMatchScore;
                }
                if (shouldSelectFirstStream)
                {
                    break;
                }
            }

            //  Once we select the size, it is the time to select the frame rate range
            for (int checkMinMaxFPS = 1; checkMinMaxFPS >= 0; checkMinMaxFPS--)
            {
                //  At the first run we check the minFPS / maxFPS strictly.
                for (int i = 0; i < device.Streams.Length; ++i)
                {
                    ref var stream = ref device.Streams[i];
                    if (stream.Format == VideoFormat.UNKNOWN)
                    {
                        continue;
                    }

                    if (stream.Width != selectedWidth || stream.Height != selectedHeight)
                    {
                        continue;
                    }
                    for (int j = 0; j < stream.FrameRateRanges.Length; ++j)
                    {
                        ref var frameRateRange = ref stream.FrameRateRanges[j];
                        if (checkMinMaxFPS > 0)
                        {
                            if (preferredMinFPS > 0.0f && frameRateRange.MinFPS < preferredMinFPS)
                            {
                                continue;
                            }
                            if (preferredMaxFPS > 0.0f && frameRateRange.MaxFPS > preferredMaxFPS)
                            {
                                continue;
                            }
                        }
                        if (selectedMinFPS < frameRateRange.MinFPS ||
                            selectedMinFPS == frameRateRange.MinFPS &&
                            selectedMaxFPS < frameRateRange.MaxFPS)
                        {
                            selectedStreamIndex = i;
                            selectedFrameRateIndex = j;
                            selectedMinFPS = frameRateRange.MinFPS;
                            selectedMaxFPS = frameRateRange.MaxFPS;
                        }
                    }
                    if (shouldSelectFirstStream && selectedStreamIndex >= 0)
                    {
                        break;
                    }
                }
                if (selectedStreamIndex >= 0)
                {
                    break;
                }
            }

            fu_camera_handler_open_camera(index, selectedStreamIndex, selectedFrameRateIndex, preferredMinFPS, preferredMaxFPS);

            {
                ref var stream = ref device.Streams[selectedStreamIndex];
                currentCameraIndex = index;
                name = device.Name;
                isFrontFacing = device.IsFrontFacing;
                width = stream.Width;
                height = stream.Height;
                horizontalFieldOfView = defaultHorizontalFieldOfView;
                cameraOutput.updateDataMode = UpdateDataMode.None;
                cameraOutput.bufferPtr = IntPtr.Zero;
                cameraOutput.texID = 0;
            }
            base.SelectCamera(index);
        }

        public override void StartCamera()
        {

            fu_camera_handler_set_active(currentCameraIndex, 1);
            base.StartCamera();
        }

        public override bool UpdateCamera()
        {
            FrameProps props;
            var result = fu_camera_handler_take_snapshot(currentCameraIndex, out props) == 1;
            if (result)
            {
                rotation = props.rotation;
                horizontalFieldOfView = props.horizontalFieldOfView;
                if (props.bgra != IntPtr.Zero)
                {
                    cameraOutput.updateDataMode = UpdateDataMode.BGRABuffer;
                    cameraOutput.bufferPtr = props.bgra;
                    cameraOutput.texID = 0;
                }
                else if (props.yuv != IntPtr.Zero)
                {
                    cameraOutput.updateDataMode = UpdateDataMode.YUV420Buffer;
                    cameraOutput.bufferPtr = props.yuv;
                    cameraOutput.texID = 0;
                }
                else
                {
                    cameraOutput.updateDataMode = UpdateDataMode.None;
                    cameraOutput.bufferPtr = IntPtr.Zero;
                    cameraOutput.texID = 0;
                    result = false;
                }
            }
            base.UpdateCamera();
            return result;
        }

        public override void StopCamera()
        {
            //fu_camera_handler_set_active(currentCameraIndex, 0);
            //修复带伸缩摄像头的手机，关闭实时捕捉后，摄像头不能缩回去的问题
            fu_camera_handler_close_camera(currentCameraIndex);
            base.StopCamera();
        }

        public override void OpenSettingsDialog()
        {
            fu_camera_handler_open_settings_dialog(currentCameraIndex, IntPtr.Zero);
        }
    }
}
