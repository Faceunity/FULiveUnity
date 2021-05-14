/* 
*   NatCam Core
*   Copyright (c) 2016 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;

    public partial interface INatCam {

        #region --Events--
        event PreviewCallback OnStart;
        event PreviewCallback OnFrame;
        #endregion

        #region --Properties--
        INatCamDevice Device {get;}
        int Camera {get; set;}
        Texture Preview {get;}
        bool IsPlaying {get;}
        Switch Verbose {set;}
        bool HasPermissions {get;}
        #endregion
        
        #region --Operations--
        void Play ();
        void Pause ();
        void Release ();
        void CapturePhoto (PhotoCallback callback);
        #endregion
    }
}