using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using FaceUnity;
using static FaceUnity.ICamera;

public class CameraManager : UnitySingleton<CameraManager>
{
    private ICamera driver;

    //public bool forceUseLegacyCamera = false;

    public int PreferredWidth = 1280;
    public int PreferredHeight = 720;

    public float PreferredMinFps = 30.0f;
    public float PreferredMaxFps = 120.0f;


    //只为了编辑器内展示
    public string CameraName = "";
    public int CameraWidth = 0;
    public int CameraHeight = 0;

    public bool IsInitialize { get => driver != null; }

    public event PreviewCallback OnSelect
    {
        add
        {
            driver.OnSelect += value;
        }
        remove
        {
            driver.OnSelect -= value;
        }
    }
    public event PreviewCallback OnStart
    {
        add
        {
            driver.OnStart += value;
        }
        remove
        {
            driver.OnStart -= value;
        }
    }
    public event PreviewCallback OnUpdate
    {
        add
        {
            driver.OnUpdate += value;
        }
        remove
        {
            driver.OnUpdate -= value;
        }
    }
    public event PreviewCallback OnStop
    {
        add
        {
            driver.OnStop += value;
        }
        remove
        {
            driver.OnStop -= value;
        }
    }


    bool cameraPermissionGranted;

    bool CameraPermissionGranted
    {
        get
        {
            if (!cameraPermissionGranted)
            {
                cameraPermissionGranted = Application.HasUserAuthorization(UserAuthorization.WebCam);
            }
            return cameraPermissionGranted;
        }
    }

    IEnumerator RequestCameraPermission()
    {
        cameraPermissionGranted = Application.HasUserAuthorization(UserAuthorization.WebCam);
        if (cameraPermissionGranted)
        {
            yield break;
        }

#if !(UNITY_EDITOR) && (UNITY_ANDROID)
        UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
#else
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            yield return Application.RequestUserAuthorization(
                UserAuthorization.WebCam);
        }
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            yield break;
        }
#endif
        cameraPermissionGranted = true;
    }

    public IEnumerator InitCamera(PreviewCallback onSelect = null, PreviewCallback onStart = null, PreviewCallback onUpdate = null, PreviewCallback onStop = null)
    {
        yield return RequestCameraPermission();
        if (!CameraPermissionGranted)
        {
            Debug.Log("permission.CAMERA denied");
            yield break;
        }

        //while (!CameraPermissionGranted)
        //{
        //    yield return RequestCameraPermission();
        //    yield return new WaitForSeconds(1);
        //}

        //bool useNativeCamera = true;

        //if (forceUseLegacyCamera)   //强制使用LegacyCamera
        //{
        //    useNativeCamera = false;
        //    Debug.Log("forceUseLegacyCamera");
        //}
        //else if (Application.platform == RuntimePlatform.Android && Util.GetAndroidAPIVersion() < 24) //NativeCamera不支持Android 7.0以下
        //{
        //    useNativeCamera = false;
        //    Debug.LogFormat("AndroidAPIVersion is too low:{0}", Util.GetAndroidAPIVersion());
        //}

        CameraDevice[] devices;
        //if (useNativeCamera)
        //{
        //    Debug.Log("new NativeCamera");
        //    driver = new NativeCamera();
        //    devices = driver.EnumerateCamera();
        //    if (devices == null || devices.Length <= 0)    //部分手机虽然Android 7.0以上，但是对NativeCamera支持不好
        //    {
        //        Debug.Log("Rare Case! new LegacyCamera");
        //        driver = new LegacyCamera();
        //        devices = driver.EnumerateCamera();
        //    }
        //}
        //else
        //{
            Debug.Log("new LegacyCamera");
            driver = new LegacyCamera();
            legacyCamera = (LegacyCamera)driver;
            devices = driver.EnumerateCamera();
        //}

        string cameraDevicesInfo = "Camera Num: " + devices.Length + ", ";
        foreach (var d in devices)
        {
            cameraDevicesInfo += "name:" + d.Name + ", IsFrontFacing:" + d.IsFrontFacing + ", ";
        }
        Debug.Log(cameraDevicesInfo);

        if (devices != null && devices.Length > 0)
        {
            int cameraIndex = 0;
            for (int i = 0; i < devices.Length; ++i)
            {
                if (devices[i].IsFrontFacing)
                {
                    cameraIndex = i;
                    break;
                }
            }
            Debug.Log($"Selected Camera:{devices[cameraIndex].Name}");
            SelectCamera(cameraIndex);
            if (onSelect != null)
                driver.OnSelect += onSelect;
            if (onStart != null)
                driver.OnStart += onStart;
            if (onUpdate != null)
                driver.OnUpdate += onUpdate;
            if (onStop != null)
                driver.OnStop += onStop;
            driver.StartCamera();
        }
        else
        {
            Debug.LogError("Can Not Find Camera!");
            yield return InitCamera(onSelect, onStart, onUpdate,onStop);
        }
    }

    public void UnInitCamera()
    {
        driver = null;
        CameraName = "";
        CameraWidth = 0;
        CameraHeight = 0;
    }

    public ICameraType Type { get { return driver.Type; } }
    public int CurrentCameraIndex { get { return driver.CurrentCameraIndex; } }
    public string Name { get { return driver.Name; } }
    public bool IsFrontFacing { get { return driver.IsFrontFacing; } }
    public int Rotation { get { return driver.Rotation; } }
    public int Width { get { return driver.Width; } }
    public int Height { get { return driver.Height; } }
    public float HorizontalFieldOfView { get { return driver.HorizontalFieldOfView; } }
    public CameraOutput CameraOutput { get { return driver.CameraOutput; } }

    public int CameraCount { get { return driver.CameraCount(); } }

    public void SwitchCamera()
    {
        if (!IsInitialize) return;
        var cameraIndex = driver.CurrentCameraIndex;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        cameraIndex = (cameraIndex + 1) % Math.Min(2, driver.CameraCount());          //在某些手机获取超过2个摄像头，第三个后不可用（如华为畅享9s）
#else
        cameraIndex = (cameraIndex + 1) % driver.CameraCount();
#endif
        SelectCamera(cameraIndex);
        StartCamera();
    }

    public void SelectCamera(int cameraIndex = -1)
    {
        if (!IsInitialize) return;
        if (cameraIndex < 0) cameraIndex = driver.CurrentCameraIndex;
        driver.PreferredWidth = PreferredWidth;
        driver.PreferredHeight = PreferredHeight;
        driver.PreferredMinFPS = PreferredMinFps;
        driver.PreferredMaxFPS = PreferredMaxFps;
        driver.SelectCamera(cameraIndex);
        CameraName = driver.Name;
        CameraWidth = driver.Width;
        CameraHeight = driver.Height;
    }

    public void StartCamera()
    {
        if (!IsInitialize) return;
        driver.StartCamera();
    }
    public bool UpdateCamera()
    {
        if (!IsInitialize) return false;
        return driver.UpdateCamera();
    }

    public void StopCamera()
    {
        if (!IsInitialize) return;
        driver.StopCamera();
    }
    private LegacyCamera legacyCamera;
    public WebCamTexture GetWebTex()
    {
        if (driver != null && Type == ICameraType.Legacy)
            return legacyCamera.GetWebTex();
        else
            return null;
    }
}
