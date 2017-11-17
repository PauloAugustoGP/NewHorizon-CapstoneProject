using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerDel : MonoBehaviour {

    public GameObject TextToDelete;
    public GameObject TextToSetActive;
    public GameObject TextToSetActiveIfTrue;
    public GameObject PlayerThatMightBeTrue;

    private void OnTriggerEnter(Collider c)
    {
        if (PlayerThatMightBeTrue == true)//add in get component to Player the might be true to get the bool that is or is not true
        { 
            TextToDelete.gameObject.SetActive(false);
            TextToSetActiveIfTrue.gameObject.SetActive(true);
        }
        else
        {
            Destroy(TextToDelete);
            TextToSetActive.gameObject.SetActive(true);
        }
    }
}
