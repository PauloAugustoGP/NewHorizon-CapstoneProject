using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastDoorOpen : MonoBehaviour {

    [SerializeField] Transform theDoor;
    [SerializeField] Transform player;
    Vector3 blastDoorOpen;
    Vector3 doorIsBlastedOpen;
    Vector3 startBlastedDoor;
    [SerializeField] [Range(1, 10)] float speed = 3f;
    [SerializeField] [Range(1, 10)] float distanceToOpen;
    [SerializeField] bool enable = false;

    // Use this for initialization
    void Start () {
        startBlastedDoor = theDoor.localPosition;
        blastDoorOpen = theDoor.localPosition;
        doorIsBlastedOpen = theDoor.localPosition;
        doorIsBlastedOpen.y += theDoor.localScale.y - 0.1f;

	}

    void Update()
    {
        if (enable)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= distanceToOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    public void EnableDoor() { enable = true; }

    private void OnTriggerEnter(Collider other)
    {
        OpenDoor();
    }

    void OpenDoor()
    {
        theDoor.localPosition = Vector3.MoveTowards(theDoor.localPosition, doorIsBlastedOpen, speed * Time.deltaTime);
    }
    void CloseDoor()
    {
        theDoor.localPosition = Vector3.MoveTowards(theDoor.localPosition, startBlastedDoor, speed * Time.deltaTime);
    }

}
