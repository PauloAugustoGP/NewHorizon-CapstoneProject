using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBrian : MonoBehaviour
{
    [SerializeField] Doors door;

    [SerializeField] Elevator elevator;

    bool hasTarget;
    //To determine if is activated or not
    private bool _isActivated;
    //Allows other scripts to acess and change the color of the switch
    public bool isActivated
    {
        get
        {
            return _isActivated;
        }
        set
        {
            _isActivated = value;
            if (_isActivated)
            {
                GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }
    }

    void Update()
    {
        if(hasTarget)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RunSwitch();
            }
        }
    }

    public void RunSwitch()
    {
        if (isActivated) return;
        if (door)
        {
            door.EnableDoor();
            isActivated = true;
            
        }
        if (elevator)
        {
            elevator.ElevatorTriggered(this);
            isActivated = true;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            hasTarget = true;
        }

        if(c.gameObject.CompareTag("Projectile"))
        {
            RunSwitch();
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
            RunSwitch();
        }
    }
}
