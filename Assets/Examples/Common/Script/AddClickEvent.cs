using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//给所在物体添加Click事件
public class AddClickEvent : MonoBehaviour,IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public delegate void GoDelegate(GameObject go);
    public GoDelegate onClick;
    public GoDelegate onPointerDown;
    public GoDelegate onPointerUp;

    public void AddListener(GoDelegate callBack)
    {
        onClick += callBack;
    }

    public void RemoveListener(GoDelegate callBack)
    {
        onClick -= callBack;
    }

    public void RemoveAllListener()
    {
        onClick = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(gameObject);
    }

    public void AddPointerDownListener(GoDelegate callBack)
    {
        onPointerDown += callBack;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(onPointerDown != null)
            onPointerDown(gameObject);
    }

    public void AddPointerUpListener(GoDelegate callBack)
    {
        onPointerUp += callBack;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(onPointerUp != null)
            onPointerUp(gameObject);
    }
}
