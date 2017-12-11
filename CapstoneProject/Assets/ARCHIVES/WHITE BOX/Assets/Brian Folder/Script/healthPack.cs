using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if (c.GetComponent<CharacterBase>().GetHealthRatio() != 100)
            {
                c.GetComponent<CharacterBase>().AddHealth(50);
                Destroy(gameObject);
            }
        }
    }
}