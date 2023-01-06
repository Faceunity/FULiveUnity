using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ISlider : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public GameObject dataGo;
    public Slider slider;
    public Text value;
    public GameObject midGo;
    public delegate void GoDelegate(GameObject go);
    public GoDelegate onPointerDown;
    public GoDelegate onPointerUp;
    public GoDelegate onDrag;
    public GoDelegate onBeginDrag;
    public GoDelegate onEndDrag;

    [HideInInspector]
    public float disablevalue = 0f;

    void Awake()
    {
        //slider = transform.GetComponent<Slider>();
        //value = transform.GetComponentInChildren<Text>();
        Debug.Log("ISlider Awake");
        registerUIEvents();
    }

    private void registerUIEvents()
    {
        if (slider == null || value == null || dataGo == null)
            return;
        Debug.Log("ISlider registerUIEvents");
        onPointerDown += (go) =>
        {
            Debug.Log("ISlider onPointerDown");
            dataGo.SetActive(true);
            value.text = string.Format("{0:0}", (slider.value - disablevalue) / slider.maxValue * 100);
        };

        onPointerUp += (go) =>
        {
            Debug.Log("ISlider onPointerUp");
            dataGo.SetActive(false);
        };

        onDrag += (go) =>
        {
            Debug.Log("ISlider onDrag");
            dataGo.SetActive(true);
            value.text = string.Format("{0:0}", (slider.value - disablevalue) / slider.maxValue * 100);
        };

        onBeginDrag += (go) =>
        {
            Debug.Log("ISlider onBeginDrag");
            dataGo.SetActive(true);
            value.text = string.Format("{0:0}", (slider.value - disablevalue) / slider.maxValue * 100);
        };

        onEndDrag += (go) =>
        {
            Debug.Log("ISlider onEndDrag");
            dataGo.SetActive(false);
        };
    }

    public void SetDisableValue(float v)
    {
        disablevalue = v;
        if(midGo != null)
            midGo.SetActive(disablevalue == 0.5);
    }

    public void AddPointerDownListener(GoDelegate callBack)
    {
        onPointerDown += callBack;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null)
            onPointerDown(gameObject);
    }

    public void AddPointerUpListener(GoDelegate callBack)
    {
        onPointerUp += callBack;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null)
            onPointerUp(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(onDrag != null)
            onDrag(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
            onBeginDrag(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(onEndDrag != null)
            onEndDrag(gameObject);
    }
}
