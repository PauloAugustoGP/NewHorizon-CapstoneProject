using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAbovePlayer : MonoBehaviour {

    public GameObject PlayerTextCanvasText;

    private void Start()
    {
        CallText();
    }

    void CallText()
    {
        GameObject temp = Instantiate(PlayerTextCanvasText) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.Find("PlayerTextCanvas"));
        tempRect.transform.localPosition = PlayerTextCanvasText.transform.localPosition;
        tempRect.transform.localScale = PlayerTextCanvasText.transform.localScale;
        temp.transform.localRotation = PlayerTextCanvasText.transform.localRotation;
    }
}
