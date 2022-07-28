using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//给所在物体添加Click事件
public class AddClickEvent : MonoBehaviour,IPointerClickHandler
{
    public delegate void GoDelegate(GameObject go);
    public GoDelegate onClick;

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
}
