using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTest : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.8f);
    }
}
