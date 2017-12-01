using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTriggerBox : MonoBehaviour {

    public GameObject textToTrigger;
    public GameObject triggerBox;

    private void OnTriggerEnter(Collider other)
    {
        textToTrigger.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(triggerBox);
    }
}
