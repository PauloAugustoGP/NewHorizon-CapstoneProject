using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastDoorOpen : MonoBehaviour {

    [SerializeField] Transform theDoor;
    Vector3 blastDoorOpen;
    Vector3 doorIsBlastedOpen;
    [SerializeField] [Range(1, 10)] float speed = 3f;

    // Use this for initialization
    void Start () {
        blastDoorOpen = theDoor.localPosition;
        doorIsBlastedOpen = theDoor.localPosition;
        doorIsBlastedOpen.y += theDoor.localScale.y - 0.1f;

	}

    private void OnTriggerEnter(Collider other)
    {
        theDoor.localPosition = Vector3.MoveTowards(theDoor.localPosition, doorIsBlastedOpen, speed * Time.deltaTime);
    }
}
