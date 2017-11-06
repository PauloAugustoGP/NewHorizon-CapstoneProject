using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int Health;

    void OnTriggerEnter(Collider other)
    {
            if (other.GetComponent<CharacterBehaviour>().CompareTag("Player"))
            {
                Health += 50;
                Destroy(gameObject);
            }

        /*
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterBehaviour>())
            {
                 Change the Damage health function to public and create a heal function so I can add health to the player.
                other.GetComponent<CharacterBehaviour>().heal(50);


            }
        }
        */
    }
}


