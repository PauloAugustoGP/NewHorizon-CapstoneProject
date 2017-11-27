using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HeathBoxPickUp : MonoBehaviour {
    public GameObject textAbovePlayer;

    private void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<CharacterBehaviour>())
        {
            if (c.GetComponent<CharacterBehaviour>().atFullHealth == false)
            {
                c.GetComponent<CharacterBehaviour>().Heal(20);
                Destroy(gameObject);
                textAbovePlayer.gameObject.SetActive(true);
            }
        }
    }
 
}
