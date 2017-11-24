using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    [Tooltip("Assign the door you want to open")]
    [SerializeField]
    private GameObject door;
    [Tooltip("Script inside of the assigned Door, calls the toggleDoorState function")]
    private Doors actualDoor;
    [Tooltip("Shows if the door is triggered or not")]
    [SerializeField]
    private bool _switchTriggered;

    MeshRenderer buttonMesh;

    void Start() {
        buttonMesh = GetComponent<MeshRenderer>();
        actualDoor = door.GetComponentInChildren<Doors>();
    }
    private void OnTriggerEnter(Collider other) {   
        if (other.gameObject.tag == "Projectile") {
            buttonMesh.material.SetColor("_Color", Color.green);
            switchTriggered = true;
            actualDoor.doorEnabled = true;
            actualDoor.toggleDoorState();
        }
            
    }
    public bool switchTriggered {
        get { return _switchTriggered; }
        set { _switchTriggered = value; }
    }
}
