using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxTriggerDel : MonoBehaviour {

    public GameObject HealthPackCanvas;

    private void OnTriggerExit(Collider c)
    {
        Destroy(HealthPackCanvas);
    }
}
