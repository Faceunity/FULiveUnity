
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace FaceUnity
{
    public enum ICameraType
    {
        Legacy,
        Native
    }

    public struct CameraDevice
    {
        public string Name;
        public bool IsFrontFacing;
    }

    public struct CameraOutput
    {
        public UpdateDataMode updateDataMode;
        public IntPtr bufferPtr;
        public int texID;
    }

    public abstract class ICamera
    {
        protected ICameraType type;

        protected int currentCameraIndex;

        protected string name;
        protected bool isFrontFacing;
        protected int rotation;

        protected int width;
        protected int height;
        protected float horizontalFieldOfView;
        protected CameraOutput cameraOutput;


        protected int preferredWidth = 1280;
        protected int preferredHeight = 720;

        protected float preferredMinFPS = 30.0f;
        protected float preferredMaxFPS = 120.0f;

        protected float defaultHorizontalFieldOfView = 70.428f;


        public ICameraType Type { get { return type; } }
        public int CurrentCameraIndex { get { return currentCameraIndex; } }
        public string Name { get { return name; } }
        public bool IsFrontFacing { get { return isFrontFacing; } }
        public int Rotation { get { return rotation; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public float HorizontalFieldOfView { get { return horizontalFieldOfView; } }
        public CameraOutput CameraOutput { get { return cameraOutput; } }

        public int PreferredWidth { get { return preferredWidth; } set { preferredWidth = value; } }
        public int PreferredHeight { get { return preferredHeight; } set { preferredHeight = value; } }
        public float PreferredMinFPS { get { return preferredMinFPS; } set { preferredMinFPS = value; } }
        public float PreferredMaxFPS { get { return preferredMaxFPS; } set { preferredMaxFPS = value; } }
        public float DefaultHorizontalFieldOfView { get { return defaultHorizontalFieldOfView; } set { defaultHorizontalFieldOfView = value; } }

        public delegate void PreviewCallback();
        public event PreviewCallback OnSelect;
        public event PreviewCallback OnStart;
        public event PreviewCallback OnUpdate;
        public event PreviewCallback OnStop;


        public abstract int CameraCount();
        public abstract CameraDevice[] EnumerateCamera();

        public virtual void SelectCamera(int index)
        {
            OnSelect?.Invoke();
        }
        public virtual void StartCamera()
        {
            OnStart?.Invoke();
        }
        public virtual bool UpdateCamera()
        {
            OnUpdate?.Invoke();
            return true;
        }
        public virtual void StopCamera()
        {
            OnStop?.Invoke();
        }

        public abstract void OpenSettingsDialog();
    }

}
