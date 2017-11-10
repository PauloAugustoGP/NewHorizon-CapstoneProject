using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
   // [SerializeField]
    public GameObject door;
    private Door actualDoor;

    void Start()
    {
        actualDoor = door.GetComponentInChildren<Door>();
        Debug.Log(actualDoor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            actualDoor.toggleDoorState();
        }
    }
    
        private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Is it working?");
        
        if (other.gameObject.tag == "Projectile")
        {
            actualDoor.toggleDoorState();
            Debug.Log("Door Opened!");
        }
            
    }
   

}
