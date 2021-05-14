/* 
*   NatCam Core
*   Copyright (c) 2016 Yusuf Olokoba
*/

#pragma warning disable 0414

namespace NatCamU.Core.Platforms {

    using UnityEngine;
    using Dispatch;
    using Utilities;
    using Native = NatCamNative;

    [CoreDoc(91)]
    public sealed partial class NatCamAndroid : INatCamBase, INatCam {

        #region --Events--

        public new event PreviewCallback OnStart {
            add {
                onStart += value;
            }
            remove {
                onStart -= value;
            }
        }
        public new event PreviewCallback OnFrame {
            add {
                onFrame += value;
            }
            remove {
                onFrame -= value;
            }
        }
        #endregion


        #region --Op vars--
        private IDispatch renderDispatch;
        private static AndroidJavaClass natcam;
        private static INatCamDevice device;
        #endregion


        #region --Properties--
        public INatCamDevice Device {
            get {
                device = device ?? new NatCamDeviceAndroid(natcam);
                return device;
            }
        }
        public int Camera {
            get {
                return natcam.CallStatic<int>("getCameraIndex");
            }
            set {
                #if NATCAM_PROFESSIONAL
                if (IsRecording) {
                    Utilities.LogError("Cannot switch cameras while recording");
                    return;
                }
                #endif
                natcam.CallStatic("setCamera", value);
            }
        }
        public Texture Preview {
            get {
                return preview;
            }
        }
        public Texture2D PreviewTexture {
            get {
                return preview;
            }
        }
        public bool IsPlaying {
            get {
                return natcam.CallStatic<bool>("isPlaying");
            }
        }
        public Switch Verbose {
            set {
                natcam.CallStatic("setVerboseMode", value == Switch.On);
            }
        }
        public bool HasPermissions {
            get {
                return natcam.CallStatic<bool>("checkPermissions");
            }
        }
        #endregion


        #region --Ctor--

        public NatCamAndroid () {
            natcam = new AndroidJavaClass("com.yusufolokoba.natcam.NatCam");
            renderDispatch = new RenderDispatch();
            DispatchUtility.onPause += OnPause;
            DispatchUtility.onOrient += OnOrient;
            Native.RegisterCoreCallbacks(INatCamBase.OnStart, INatCamBase.OnFrame, INatCamBase.OnPhoto);
            #if NATCAM_EXTENDED
            natcamextended = new AndroidJavaClass("com.yusufolokoba.natcamextended.NatCamExtended");
            Native.RegisterExtendedCallbacks(OnBarcode, OnFace, OnText, OnSave);
            #endif
            #if NATCAM_PROFESSIONAL
            natcamprofessional = new AndroidJavaClass("com.yusufolokoba.natcamprofessional.NatCamProfessional");
            natcamprofessional.CallStatic("setBitrate", VideoBitrate);
            natcamprofessional.CallStatic("setRecordAudio", RecordAudio);
            Native.RegisterProfessionalCallbacks(OnVideo);
            #endif
        }
        #endregion
        

        #region --Operations--

        public void Play () {
            dispatch = dispatch ?? new MainDispatch();
            #if NATCAM_EXTENDED
            natcamextended.CallStatic("setDetection", onMetadata != null);
            #endif
            OnOrient(DispatchUtility.Orientation);
            natcam.CallStatic("play");
            OnStart += GL.InvalidateState;
        }

        public void Pause () {
            #if NATCAM_EXTENDED
            onMetadata = null;
            #endif
            natcam.CallStatic("pause");
        }

        public void Release () {
            onStart = 
            onFrame = null;
            #if NATCAM_EXTENDED
            onMetadata = null;
            #endif
            #if NATCAM_PROFESSIONAL
            ReleasePreviewBuffer();
            #endif
            natcam.CallStatic("release");
            if (preview != null) MonoBehaviour.Destroy(preview); preview = null;
            if (dispatch != null) dispatch.Release(); dispatch = null;
        }

        public void CapturePhoto (PhotoCallback callback) {
            photoCallback = callback;
            natcam.CallStatic("capturePhoto");
        }

        public static int GetCameraDeviceCount()
        {
            return natcam.CallStatic<int>("getCameraDeviceCount");
        }
        #endregion


        #region --Utility--

        private void OnPause (bool paused) {
            #if NATCAM_PROFESSIONAL
            if (IsRecording) {
                Utilities.LogError("Suspending app while recording. Ending recording");
                StopRecording();
            }
            #endif
            natcam.CallStatic("onPause", paused);
        }

        private void OnOrient (Orientation orientation) {
            natcam.CallStatic("setOrientation", (byte)orientation);
        }
        #endregion
    }
}
#pragma warning restore 0414