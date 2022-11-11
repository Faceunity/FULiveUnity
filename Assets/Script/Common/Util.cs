using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Util
{
    /// <summary>
    /// 这里需要注意一下安卓以外平台上要加file协议。 
    /// </summary>
    /// <returns></returns>
    public static string GetStreamingAssetsPath()
    {
        string path = Application.streamingAssetsPath;
        if (Application.platform != RuntimePlatform.Android)
        {
            path = "file://" + path;
        }
        return path;
    }

    public static WaitForEndOfFrame end_of_frame = new WaitForEndOfFrame();
    public static WaitForFixedUpdate fixed_update = new WaitForFixedUpdate();

    /// <summary>
    /// 把一个bytes数组保存成一个文件
    /// </summary>
    /// <param name="data">要保存的bytes数组</param>
    /// <param name="path">保存路径</param>
    /// <param name="name_with_extension">文件名加后缀名，如：example.bundle</param>
    /// <returns>保存的文件全称</returns>
    public static string SaveBytesFile(byte[] data, string path, string name_with_extension)
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        string fullfilename = path + name_with_extension;
        File.WriteAllBytes(fullfilename, data);
        Debug.Log("保存了一个文件:" + fullfilename);
        return fullfilename;
    }

    public static byte[] ReadBytesFile(string path, string name_with_extension)
    {
        if (Directory.Exists(path) == false)
        {
            return null;
        }
        string fullfilename = path + name_with_extension;
        byte[] data = File.ReadAllBytes(fullfilename);
        Debug.Log("读取了一个文件:" + fullfilename);
        return data;
    }
    /// <summary>
    /// 加载Bundle
    /// </summary>
    /// <param name="path"></param>
    /// <param name="call_back"></param>
    /// <returns></returns>
    public static IEnumerator DownLoadBuff(string path, Action<UnityWebRequest> call_back)
    {
        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(path))
        {
            DownloadHandlerBuffer down_load_buff = new DownloadHandlerBuffer();
            webRequest.downloadHandler = down_load_buff;
            yield return webRequest.SendWebRequest();
            call_back?.Invoke(webRequest);
        }

    }
    /// <summary>
    /// 仅仅保存图片成png到固定路径，并不通知图库刷新，因此请用文件浏览器在对应路径打开图片
    /// </summary>
    /// <param name="tex"></param>
    public static void SaveTex2D(Texture2D tex)
    {
        byte[] bytes = tex.EncodeToPNG();
        if (Directory.Exists(Application.persistentDataPath + FuConst.PHOTOES_PATH) == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + FuConst.PHOTOES_PATH);
        }
        string name = Application.persistentDataPath + FuConst.PHOTOES_PATH + DateTime.Now.ToString(FuConst.YYYYMMDDHHMMSSFFFF) + ".png";
        File.WriteAllBytes(name, bytes);
        Debug.Log("保存了一张照片:" + name);
    }
    /// <summary>
    /// 截图
    /// </summary>
    /// <param name="cameras">要参与截图的unity相机</param>
    /// <param name="rect">要截图的范围，一般是全屏</param>
    /// <returns>计算好的纹理</returns>
    public static Texture2D CaptureCamera(Camera[] cameras, Rect rect)
    {
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);// 创建一个RenderTexture对象
        foreach (Camera cam in cameras)
        {
            cam.targetTexture = rt;                                               // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
            cam.Render();
        }
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);                                        // 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();
        foreach (Camera cam in cameras)
        {
            cam.targetTexture = null;                                             // 重置相关参数，以使用camera继续在屏幕上显示 
        }
        RenderTexture.active = null;                                              // JC: added to avoid errors  
        UnityEngine.Object.Destroy(rt);
        Debug.Log("截屏了一张照片");
        return screenShot;
    }
    /// <summary>
    /// untested function，将ARGB转化成NV21，仅用来测试
    /// </summary>
    /// <param name="yuv420sp">byte[] yuv = new byte[inputWidth * inputHeight * 3 / 2]</param>
    /// <param name="argb"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void EncodeYUV420SP(byte[] yuv420sp, int[] argb, int width, int height)
    {
        int frame_size = width * height;
        int uv_index = frame_size;
        int y_index = 0;
        int a, r, g, b, y, u, v;
        int index = 0;
        for (int height_index = 0; height_index < height; height_index++)
        {
            for (int width_index = 0; width_index < width; width_index++)
            {
                a = (int)(argb[index] & 0xff000000) >> 24;                        // a is not used obviously
                r = (argb[index] & 0xff0000) >> 16;
                g = (argb[index] & 0xff00) >> 8;
                b = (argb[index] & 0xff) >> 0;
                y = ((66 * r + 129 * g + 25 * b + 128) >> 8) + 16;                // well known RGB to YUV algorithm
                u = ((-38 * r - 74 * g + 112 * b + 128) >> 8) + 128;
                v = ((112 * r - 94 * g - 18 * b + 128) >> 8) + 128;
                yuv420sp[y_index++] = (byte)((y < 0) ? 0 : ((y > 255) ? 255 : y));// NV21 has a plane of Y and interleaved planes of VU each sampled by a factor of 2
                if (height_index % 2 == 0 && index % 2 == 0)                                 // meaning for every 4 Y pixels there are 1 V and 1 U.  Note the sampling is every other                          
                {                                                                 // pixel AND every other scanline.
                    yuv420sp[uv_index++] = (byte)((v < 0) ? 0 : ((v > 255) ? 255 : v));
                    yuv420sp[uv_index++] = (byte)((u < 0) ? 0 : ((u > 255) ? 255 : u));
                }
                index++;
            }
        }
    }

    public static string UTF8ToString(System.IntPtr ptr)
    {
        int len = 0;
        while (Marshal.ReadByte(ptr, len) != 0) ++len;
        byte[] buffer = new byte[len];
        Marshal.Copy(ptr, buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer);
    }
    
    public static int GetAndroidAPIVersion()
    {
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
    }
}
