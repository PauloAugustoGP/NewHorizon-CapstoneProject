using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HeathBoxPickUp : MonoBehaviour {


    private void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<CharacterBehaviour>())
        {
            c.GetComponent<CharacterBehaviour>().Heal(20);
            Destroy(gameObject);
        }
    }
 
}
