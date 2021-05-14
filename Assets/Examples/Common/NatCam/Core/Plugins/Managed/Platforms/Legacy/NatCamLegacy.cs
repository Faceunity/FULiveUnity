/* 
*   NatCam Core
*   Copyright (c) 2016 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;
    using Dispatch;
    using Utilities;
    using Util = Utilities.Utilities;

    [CoreDoc(93)]
    public sealed partial class NatCamLegacy : INatCam {

        #region --Events--
        public event PreviewCallback OnStart;
        public event PreviewCallback OnFrame;
        #endregion


        #region --Op vars--
        private WebCamTexture preview;
        private int camera = -1;
        private bool firstFrame;
        private IDispatch dispatch;
        private static INatCamDevice device;
        #endregion


        #region --Properties--
        public INatCamDevice Device {
            get {
                device = device ?? new NatCamDeviceLegacy();
                return device;
            }
        }
        public int Camera {
            get {
                return camera;
            }
            set {
                if (!supportedDevice) return;
                #if NATCAM_PROFESSIONAL
                if (IsRecording) {
                    Utilities.LogError("Cannot switch cameras while recording");
                    return;
                }
                #endif
                camera = value;
                if (!IsPlaying) return;
                preview.Stop(); preview = null;
                Play();
            }
        }
        public Texture Preview {
            get {
                return preview;
            }
        }
        [CoreDoc(95)]
        public WebCamTexture PreviewTexture {
            get {
                return preview;
            }
        }
        public bool IsPlaying {
            get {
                return preview && preview.isPlaying;
            }
        }
        public Switch Verbose {
            set {

            }
        }
        public bool HasPermissions {
            get {
                return true;
            }
        }
        private static bool supportedDevice {
            get {
                bool ret = WebCamTexture.devices.Length > 0;
                if (!ret) Util.LogError("Current device has no cameras");
                return ret;
            }
        }
        #endregion
        

        #region --Operations--

        public void Play () {
            if (!supportedDevice) return;
            if (dispatch == null) {
                dispatch = new MainDispatch();
                dispatch.Dispatch(Update, true);
            }
            if (preview == null) {
                int width, height, rate = (int)device.GetFramerate(camera);
                string name = WebCamTexture.devices[camera].name;
                device.GetPreviewResolution(camera, out width, out height);
                rate = rate == 0 ? 30 : rate;
                preview = width == 0 ?  new WebCamTexture(name) : new WebCamTexture(name, width, height, rate);
            }
            #if NATCAM_EXTENDED
            SetDetection(OnMetadata != null);
            #endif
            firstFrame = true;
            preview.Play();
        }

        public void Pause () {
            #if NATCAM_EXTENDED
            OnMetadata = null;
            #endif
            if (preview) preview.Stop();
        }

        public void Release () {
            if (!preview) return;
            OnStart =
            OnFrame = null;
            #if NATCAM_EXTENDED
            SetDetection(false);
            OnMetadata = null;
            #endif
            #if NATCAM_PROFESSIONAL
            ReleasePreviewBuffer();
            #endif
            preview.Stop();
            MonoBehaviour.Destroy(preview);
            preview = null;
            dispatch.Release();
            dispatch = null;
            camera = -1;
        }

        public void CapturePhoto (PhotoCallback callback) {
            if (!supportedDevice || !preview || !preview.isPlaying) return;
            var photo = new Texture2D(preview.width, preview.height, TextureFormat.ARGB32, false, false);
            photo.SetPixels32(preview.GetPixels32());
            photo.Apply();
            if (callback != null) callback(photo, 0);
        }

        public static int GetCameraDeviceCount()
        {
            return WebCamTexture.devices.Length;
        }
        #endregion


        #region --State Management--

        private void Update () {
            if (!preview || !preview.isPlaying) return;
            if (!preview.didUpdateThisFrame || preview.width + preview.height == 16 << 1) return;
            if (firstFrame) {
                #if NATCAM_PROFESSIONAL
                InitializePreviewBuffer();
                #endif
                if (OnStart != null) OnStart();
                firstFrame = false;
            }
            if (OnFrame != null) OnFrame();
        }
        #endregion
    }
}