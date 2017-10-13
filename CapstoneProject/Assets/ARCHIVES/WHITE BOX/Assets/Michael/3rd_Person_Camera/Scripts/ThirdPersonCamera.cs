using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    // Tutorial Vars
    public Transform MainCam;
    public Transform target;

    public float walkDist;
    public float runDist;
    public float heightDist;

    // Mouse vars
    private float mouseX;
    private float mouseY;

    private float rotX;
    private float rotY;

    // Use this for initialization
    void Start () {

        if(target == null)
        {
            Debug.LogWarning("Camera does not have a target");
        }

        // Set camera follow distance
        walkDist = 4f;
        runDist = 6f;
        heightDist = 2f;

        mouseX = 0f;
        mouseY = 0f;

        rotX = 250f;
        rotY = 120f;

        // Set Camera position behind and above player object
        MainCam.position = new Vector3(target.position.x, target.position.y + heightDist, target.position.z - walkDist);
        // Rotates Camera to look at CamTarget
        MainCam.LookAt(target);

    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseX += Input.GetAxis("Mouse X") * rotX * 0.02f;
            mouseY += Input.GetAxis("Mouse Y") * rotY * 0.02f;

            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Vector3 position = rotation * new Vector3(0f, 0f, -walkDist) + target.position;

            MainCam.rotation = rotation;
            MainCam.position = position;
        }
    }
}
