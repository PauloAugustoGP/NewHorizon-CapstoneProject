using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Doors door;
    
    [SerializeField] Material on;

    bool hasTarget;

    void Update()
    {
        if(hasTarget)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                door.EnableDoor();
                GetComponent<Renderer>().material = on;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            hasTarget = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            hasTarget = false;
        }
    }
    
    void OnParticleCollision(GameObject particle)
    {
        Debug.Log("AAA");
        if (particle.CompareTag("Projectile"))
        {
            door.EnableDoor();
            GetComponent<Renderer>().material = on;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
