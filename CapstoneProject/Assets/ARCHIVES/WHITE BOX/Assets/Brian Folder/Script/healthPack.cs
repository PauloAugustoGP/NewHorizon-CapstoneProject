using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int Health;

    void Start()
    {
        Health = CharacterBehaviour.Health;
    }
    //I need to figure it out a Way to add health to the player's health without using the get component script within the player.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health += 3;
            Destroy(gameObject);
        }

    }
}
