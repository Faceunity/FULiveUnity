/* 
*   NatCam Core
*   Copyright (c) 2016 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using System;
    using System.Runtime.InteropServices;

    public static partial class NatCamNative {

        private const string CoreAssembly = FaceunityWorker.unity_plugin_name;

        #region ---Delegates---
        public delegate void StartCallback (IntPtr texPtr, int width, int height);
        public delegate void PreviewCallback (IntPtr texPtr);
        public delegate void PhotoCallback (IntPtr imgPtr, int width, int height, int size, byte orientation);
        #endregion

        #region --Initialization--
        [DllImport(CoreAssembly)]
        public static extern void RegisterCoreCallbacks (StartCallback startCallback,  PreviewCallback previewCallback, PhotoCallback photoCallback);
        #endregion

#if UNITY_IOS

        #region --Operations--
        [DllImport(CoreAssembly)]
        public static extern int GetCamera ();
        [DllImport(CoreAssembly)]
        public static extern void SetCamera (int camera);
        [DllImport(CoreAssembly)]
        public static extern bool IsPlaying ();
        [DllImport(CoreAssembly)]
        public static extern void Play ();
        [DllImport(CoreAssembly)]
        public static extern void Pause ();
        [DllImport(CoreAssembly)]
        public static extern void Release ();
        [DllImport(CoreAssembly)]
        public static extern void CapturePhoto ();
        [DllImport(CoreAssembly)]
        public static extern void ReleasePhoto ();
        [DllImport(CoreAssembly)]
        public static extern byte GetOrientation ();
        [DllImport(CoreAssembly)]
        public static extern void SetOrientation (byte orientation);
        [DllImport(CoreAssembly)]
        public static extern int GetCameraDeviceCount ();
        #endregion

        #region --Utility--
        [DllImport(CoreAssembly)]
        public static extern void OnPause (bool paused);
        [DllImport(CoreAssembly)]
        public static extern void SetVerboseMode (bool verbose);
        #endregion


#else
        public static int GetCamera () {return -1;}
        public static void SetCamera (int camera) {}
        public static bool IsPlaying () {return false;}
        public static void Play () {}
        public static void Pause () {}
        public static void Release () {}
        public static void CapturePhoto () {}
        public static void ReleasePhoto() {}
        public static byte GetOrientation () {return 0;}
        public static void SetOrientation (byte orientation) {}
        public static int GetCameraDeviceCount() { return 0; }
        public static void OnPause (bool paused) {}
        public static void SetVerboseMode (bool verbose) {}
#endif
    }
}