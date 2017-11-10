using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerDel : MonoBehaviour {

    public GameObject TextToDelete;
    public GameObject TextToSetActive;

    private void OnTriggerEnter(Collider c)
    {
        Destroy(TextToDelete);
        TextToSetActive.gameObject.SetActive(true);
    }
}
