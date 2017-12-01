using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    enum Type
    {
        Vertical,
        Horizontal
    }

    [SerializeField] Type doorType;

    [SerializeField] Transform leftDoor;
    Vector3 startLeftDoor;
    Vector3 openLeftDoor;

    [SerializeField] Transform rightDoor;
    Vector3 startRightDoor;
    Vector3 openRightDoor;

    [SerializeField] Transform player;
    [SerializeField] [Range(1, 10)] float distanceToOpen;

    [Tooltip("Door status. Open = true, Closed = false")]
    [SerializeField] bool enable = false;
    
    [SerializeField] [Range(1, 10)] float speed = 3f;

    void Start()
    {
        startLeftDoor = leftDoor.localPosition;
        openLeftDoor = leftDoor.localPosition;

        startRightDoor = rightDoor.localPosition;
        openRightDoor = rightDoor.localPosition;

        switch (doorType)
        {
            case Type.Vertical:
                openLeftDoor.y += leftDoor.localScale.y -0.1f;
                openRightDoor.y += rightDoor.localScale.y -0.1f;
                break;

            case Type.Horizontal:
                openLeftDoor.x -= leftDoor.localScale.x - 0.1f;
                openRightDoor.x += rightDoor.localScale.x - 0.1f;
                break;
        }
    }

    void Update()
    {
        if(enable)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if( distance <= distanceToOpen )
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

    void OpenDoor()
    {
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, openLeftDoor, speed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, openRightDoor, speed * Time.deltaTime);
    }

    void CloseDoor()
    {
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, startLeftDoor, speed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, startRightDoor, speed * Time.deltaTime);
    }
    

    /*
    public virtual IEnumerator MoveDoor(Quaternion dest) {
        yield return null;
    }
    //Brian's code
    public virtual void toggleDoorState() { }
    public virtual IEnumerator Open() {
        yield return null;
    }
    public virtual IEnumerator Close() {
        yield return null;
    }*/
}
