using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public Image bg;
    public Text text;
    private float displayTime = 2.0f;
    private float leftTime = 0f;
    private bool isEnble = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnble)
            TurnTransparent(Time.deltaTime);
    }

    public void ShowAndSetText(string str)
    {
        text.text = str;
        isEnble = true;
        leftTime = displayTime;
        this.gameObject.SetActive(true);
    }

    private void TurnTransparent(float delta)
    {
        leftTime -= delta;
        if(leftTime <= 0f)
        {
            isEnble = false;
            leftTime = 0f;
            this.gameObject.SetActive(false);
        }
    }
}
