using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    [Tooltip("Assign the door you want to open")]
    public GameObject door;
    [Tooltip("Script inside of the assigned Door, calls the toggleDoorState function")]
    private Door actualDoor;
    [Tooltip("Shows if the door is triggered or not")]
    public bool _switchTriggered;

    void Start()
    {
        actualDoor = door.GetComponentInChildren<Door>();
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
