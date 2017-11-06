using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterBehaviour>())
            {                 
                other.GetComponent<CharacterBehaviour>().Heal(50);
                Destroy(gameObject);
            }
        }
        
}


