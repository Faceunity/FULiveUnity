using UnityEngine;
using UnityEngine.UI;

//显示当前帧数
public class FPSDisplay : MonoBehaviour
{
	void Awake()
    {
		//w = Screen.width, h = Screen.height;
		if (fpsTxt != null)
			fpsTxt.color = color;
	}

	float deltaTime = 0.0f;

	public Text fpsTxt;

	int w;
	int h;
	Color color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
	float fps = 0f;
	void Update()
	{
		if (fpsTxt == null)
			return;

		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		fps = 1.0f / deltaTime;
		fpsTxt.text = string.Format("{0:0.} FPS", fps);
	}

    //void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(w * 0.45f, 0, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 2 / 100;
    //    style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    //    //float msec = deltaTime * 1000.0f;
    //    float fps = 1.0f / deltaTime;
    //    string text = string.Format("{0:0.} FPS", fps);
    //    GUI.Label(rect, text, style);
    //}
}

