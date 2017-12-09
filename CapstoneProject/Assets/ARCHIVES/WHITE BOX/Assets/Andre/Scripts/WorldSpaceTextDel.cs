using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceTextDel : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<HeathBoxPickUp>().thisObject == true)
        {
            Destroy(gameObject);
        }
    }
}
