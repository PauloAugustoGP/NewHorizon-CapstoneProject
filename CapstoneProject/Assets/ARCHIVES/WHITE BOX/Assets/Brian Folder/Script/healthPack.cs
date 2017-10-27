using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int Health;

    void Start()
    {
        //Health = CharacterBehaviour.Health;
    }
    //I need to figure it out a Way to add health to the player's health without using the get component script within the player.
    void OnTriggerEnter(Collider other)
    {
///<<<<<<< HEAD
        if (other.CompareTag("Player"))
        {
            Health += 3;
///=======
       /* if (other.GetComponent<charControlScript>())
        {
            //charControlScript c = other.GetComponent<charControlScript>();
            c.PlayerHealth += 3;
>>>>>>> 97e934e2fc3dd959d58c94232f83d4d911b98bcb
            Destroy(gameObject);
        }
        */
		}
	}
}
	
