using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {                 
            c.GetComponent<CharacterBase>().AddHealth(50);
            Destroy(gameObject);
        }
    }
}