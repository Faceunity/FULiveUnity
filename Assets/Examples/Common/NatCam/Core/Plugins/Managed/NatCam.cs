/* 
*   NatCam Core
*   Copyright (c) 2016 Yusuf Olokoba
*/

namespace NatCamU.Core {

    using UnityEngine;
    using Platforms;
    using Dispatch;
    using Utilities;
    using Util = Utilities.Utilities;

    [CoreDoc(0)]
    public static partial class NatCam {

        #region --Events--
        
        /// <summary>
        /// Event fired when the preview starts
        /// </summary>
        [CoreDoc(1)]
        public static event PreviewCallback OnStart {
            add {
                Implementation.OnStart += value;
            }
            remove {
                Implementation.OnStart -= value;
            }
        }
        /// <summary>
        /// Event fired on each camera preview frame
        /// </summary>
        [CoreDoc(2)]
        public static event PreviewCallback OnFrame {
            add {
                Implementation.OnFrame += value;
            }
            remove {
                Implementation.OnFrame -= value;
            }
        }
        #endregion


        #region --Properties--

        /// <summary>
        /// The backing implementation NatCam uses on this platform
        /// </summary>
        [CoreDoc(3), CoreRef(0, 1, 2)]
        public static readonly INatCam Implementation;
        public static readonly int DeviceCount;
        /// <summary>
        /// The camera preview as a Texture
        /// </summary>
        [CoreDoc(4)]
        public static Texture Preview {
            get {
                return Implementation.Preview;
            }
        }
        /// <summary>
        /// Get or set the active camera.
        /// </summary>
        [CoreDoc(5), CoreCode(0), CoreCode(1)]
        public static DeviceCamera Camera {
            get {
                return Implementation.Camera;
            }
            set {
                if (value != -1) Implementation.Camera = value;
            }
        }
        /// <summary>
        /// Is the preview running?
        /// </summary>
        [CoreDoc(6)]
        public static bool IsPlaying {
            get {
                return Implementation.IsPlaying;
            }
        }
        /// <summary>
        /// Set NatCam's verbose logging mode
        /// </summary>
        public static Switch Verbose {
            set {
                Implementation.Verbose = value;
                Util.verbose = value == Switch.On;
            }
        }
        #endregion


        #region --Operations--

        /// <summary>
        /// Start the camera preview
        /// </summary>
        /// <param name="camera">Optional. Camera that the preview should start from</param>
        [CoreDoc(7)]
        public static void Play (DeviceCamera camera = null) {
            if (camera) Implementation.Camera = camera;
            Implementation.Play();
        }

        /// <summary>
        /// Pause the camera preview
        /// </summary>
        [CoreDoc(8)]
        public static void Pause () {
            Implementation.Pause();
        }

        /// <summary>
        /// Release all NatCam resources
        /// </summary>
        [CoreDoc(9)]
        public static void Release () {
            Implementation.Release();
        }

        /// <summary>
        /// Capture a photo
        /// </summary>
        /// <param name="callback">The callback to be invoked when NatCam receives the captured photo</param>
        [CoreDoc(10, 8), CoreCode(2), CoreCode(3)]
        public static void CapturePhoto (PhotoCallback callback) {
            if (callback == null) {
                Util.LogError("Cannot capture photo when callback is null");
                return;
            }
            if (!Implementation.IsPlaying) {
                Util.LogError("Cannot capture photo when session is not running");
                return;
            }
			Implementation.CapturePhoto(callback);
        }
        #endregion


        #region --Initializer--

        static NatCam () {
            // Create implementation for this platform
#if UNITY_EDITOR_OSX
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#elif UNITY_EDITOR_WIN
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#elif UNITY_STANDALONE_OSX
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#elif UNITY_STANDALONE_WIN
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#elif UNITY_IOS
			//Implementation = new NatCamLegacy();
            Implementation = new NatCamiOS();
            DeviceCount = NatCamiOS.GetCameraDeviceCount();
#elif UNITY_ANDROID
            Implementation = new NatCamAndroid();
            DeviceCount = NatCamAndroid.GetCameraDeviceCount();
#elif UNITY_WSA
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#elif UNITY_WEBGL
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#else
            Implementation = new NatCamLegacy();
            DeviceCount = NatCamLegacy.GetCameraDeviceCount();
#endif
            // Quit when app dies
            DispatchUtility.onQuit += Release;
        }

        #endregion
    }
}