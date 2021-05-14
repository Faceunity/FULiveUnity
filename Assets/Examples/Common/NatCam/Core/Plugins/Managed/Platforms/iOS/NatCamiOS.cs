/* 
*   NatCam Core
*   Copyright (c) 2016 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;
    using System.Runtime.InteropServices;
    using Dispatch;
    using Utilities;

    [CoreDoc(92)]
    public sealed partial class NatCamiOS : INatCamBase, INatCam {

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
        private static INatCamDevice device;
        #endregion


        #region --Properties--
        public INatCamDevice Device {
            get {
                device = device ?? new NatCamDeviceiOS();
                return device;
            }
        }
        public int Camera {
            get {
                return NatCamNative.GetCamera();
            }
            set {
                #if NATCAM_PROFESSIONAL
                if (IsRecording) {
                    Utilities.LogError("Cannot switch cameras while recording");
                    return;
                }
                #endif
                NatCamNative.SetCamera(value);
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
                return NatCamNative.IsPlaying();
            }
        }
        public Switch Verbose {
            set {
                NatCamNative.SetVerboseMode(value == Switch.On);
            }
        }
        public bool HasPermissions {
            get {
                return CheckPermissions();
            }
        }
        #endregion


        #region --Ctor--

        public NatCamiOS () {
            DispatchUtility.onPause += OnPause;
            DispatchUtility.onOrient += OnOrient;
            NatCamNative.RegisterCoreCallbacks(INatCamBase.OnStart, INatCamBase.OnFrame, INatCamBase.OnPhoto);
            #if NATCAM_EXTENDED
            NatCamNative.RegisterExtendedCallbacks(OnBarcode, OnFace, OnText, OnSave);
            #endif
            #if NATCAM_PROFESSIONAL
            SetBitrate(VideoBitrate);
            SetRecordAudio(RecordAudio);
            NatCamNative.RegisterProfessionalCallbacks(OnVideo);
            #endif
        }
        #endregion
        

        #region --Operations--

        public void Play () {
            dispatch = dispatch ?? new MainDispatch();
            #if NATCAM_EXTENDED
            SetDetection(onMetadata != null, UseCoreImageMetadataBackend);
            #endif
            OnOrient(DispatchUtility.Orientation);
            NatCamNative.Play();
        }

        public void Pause () {
            #if NATCAM_EXTENDED
            onMetadata = null;
            #endif
            NatCamNative.Pause();
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
            NatCamNative.Release();
            if (preview != null) MonoBehaviour.Destroy(preview); preview = null;
            if (dispatch != null) dispatch.Release(); dispatch = null;
        }

        public void CapturePhoto (PhotoCallback callback) {
            photoCallback = callback;
            NatCamNative.CapturePhoto();
        }

        public static int GetCameraDeviceCount()
        {
            return NatCamNative.GetCameraDeviceCount();
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
            NatCamNative.OnPause(paused);
        }

        private void OnOrient (Orientation orientation) {
            NatCamNative.SetOrientation((byte)orientation);
        }
        #endregion


        #region --Native Interop--
        #if UNITY_IOS
        [DllImport("__Internal")]
        private static extern bool CheckPermissions ();
        #else
        private static bool CheckPermissions () {return true;}
        #endif
        #endregion
    }
}