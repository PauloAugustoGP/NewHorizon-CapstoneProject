using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    private Door actualDoor;
    private bool _switchTriggered;

    void Start()
    {
        actualDoor = door.GetComponentInChildren<Door>();
        Debug.Log(actualDoor);
    }

        private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Projectile")
        {
            switchTriggered = true;
            actualDoor.toggleDoorState();
        }
            
    }

    public bool switchTriggered
    {
        get { return _switchTriggered; }
        set { _switchTriggered = value; }
    }
   

}
