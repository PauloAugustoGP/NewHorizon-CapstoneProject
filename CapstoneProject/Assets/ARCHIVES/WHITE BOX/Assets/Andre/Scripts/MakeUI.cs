using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MakeUI : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] GameObject uiToInstant;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 screenPos = new Vector3(eventData.position.x, eventData.position.y, eventData.pointerPressRaycast.distance);
        Vector3 instantiatePos = eventData.pressEventCamera.ScreenToWorldPoint(screenPos);
        GameObject clone = (GameObject)Instantiate(uiToInstant, instantiatePos, eventData.pressEventCamera.transform.rotation);
        clone.transform.SetParent(transform);
    }
}
