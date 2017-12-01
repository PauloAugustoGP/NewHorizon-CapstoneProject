using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    [Tooltip("Assign the object you want to control")]
    [SerializeField]
    private GameObject Interactable;
    [Tooltip("Script inside of the assigned Door, calls the toggleDoorState function")]
    private Doors actualDoor;
    private Elevator actualElevator;
    [Tooltip("Shows if the door is triggered or not")]
    [SerializeField]
    private bool _switchTriggered;
    [Tooltip("renderer that is being turned into green, after the switch is triggered")]
    MeshRenderer buttonMesh;

    void Start()
    {
        buttonMesh = GetComponent<MeshRenderer>();
        if(Interactable != null)
            {
            actualDoor = Interactable.GetComponentInChildren<Doors>();
            actualElevator = Interactable.GetComponent<Elevator>();
            }
        
    }
    private void OnTriggerEnter(Collider other)
    {   
        //Verifies if the projectile that hitted and if the button is not activated
        if (other.gameObject.tag == "Projectile" && !switchTriggered)
        {
            if (actualDoor)
            {
                switchTriggered = true;
                //actualDoor.toggleDoorState();
            }
            else if (actualElevator)
            {
                switchTriggered = true;
                //Call the elevatortriggered function and gives a reference to the button
                actualElevator.ElevatorTriggered(this);
            }
        }    
    }
    public bool switchTriggered
    {
        get { return _switchTriggered; }
        set
        {
            _switchTriggered = value;
            if (_switchTriggered)
            {
                buttonMesh.material.SetColor("_Color", Color.green);
            }
            else
            {
                buttonMesh.material.SetColor("_Color", Color.red);
            }
        }
    }
   

}
