using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HeathBoxPickUp : MonoBehaviour {
    public GameObject textAbovePlayer;
    public GameObject theObject;
    public GameObject WorldCanvas;
    public bool thisObject;

    private void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<CharacterBehaviour>())
        {
            if (c.GetComponent<CharacterBehaviour>().atFullHealth == false)
            {
                c.GetComponent<CharacterBehaviour>().AddHealth(20);
                thisObject = true;
                textAbovePlayer.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (thisObject == true)
        {
            Destroy(theObject);
            WorldCanvas.gameObject.SetActive(false);
        }
    }

}
