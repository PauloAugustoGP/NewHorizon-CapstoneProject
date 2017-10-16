/// CAMERA CONTROL SCRIPT
/// ---------------------
/// 3rd person perspective
/// Free View, right click to enter, user can rotate camera freely horizontally, clamped vertically
/// 
/// -Right click control can be changed
/// -walkDist, heightDist, damping, vertical angle limits, and rotation speeds can all be changed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform mainCam;
    public Transform target;
    // Camera mount positions
    [SerializeField]
    private float walkDist;
    [SerializeField]
    private float heightDist;
    // Controls speed of camera transition after user exits Free View
    [SerializeField]
    private float heightDamping;
    [SerializeField]
    private float rotationDamping;
    // Used for calculating camera position while in Free View
    private float _mouseX;
    private float _mouseY;
    // Vertical angle clamp values in Free View
    [SerializeField]
    private float _yAngleMin;
    [SerializeField]
    private float _yAngleMax;
    // Controls the speed of the camera rotation in Free View
    [SerializeField]
    private float _rotSpeedX;
    [SerializeField]
    private float _rotSpeedY;
    // True means user is in Free View, false otherwise
    private bool _cameraOn;

    private void Awake()
    {
        walkDist = 4f;
        heightDist = 1f;

        heightDamping = 2f;
        rotationDamping = 3f;

        _mouseX = 0f;
        _mouseY = 0f;

        _yAngleMin = -15;
        _yAngleMax = 35;

        _rotSpeedX = 250f;
        _rotSpeedY = 120f;

        _cameraOn = false;
    }

    // Use this for initialization
    void Start () {

        if (target == null)
        {
            Debug.LogWarning("Camera does not have a target");
        }
        else
        {
            CameraFollowPosition();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _cameraOn = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _cameraOn = false;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            if (_cameraOn)
            {
                // Calculate the X and Y pos to transition the camera to,
                // multiplied by rotation speed and standard value of Time.fixedDeltaTime
                _mouseX += Input.GetAxis("Mouse X") * _rotSpeedX * 0.02f;
                _mouseY += Input.GetAxis("Mouse Y") * _rotSpeedY * -0.02f; // Negative value inverts Y-axis control
                // Clamp vertical rotation of camera
                _mouseY = Mathf.Clamp(_mouseY, _yAngleMin, _yAngleMax);

                // This version helps remove the snap-to movement of the camera upon first right-clicking
                // It also drastically changes how fast the camera rotation movement occurs. NEEDS WORK!
                //Quaternion rotation = Quaternion.Euler(_mouseY + mainCam.eulerAngles.x, _mouseX, 0);

                // Convert euler angles to Unity Quaternion
                Quaternion rotation = Quaternion.Euler(_mouseY, _mouseX, 0);
                // 
                Vector3 position = rotation * new Vector3(0f, 0f, -walkDist) + target.position;

                mainCam.rotation = rotation;
                mainCam.position = position;
            }
            else
            {
                // This reset of X and Y pos prevents a snap-transition to the last position the previous time user entered Free View
                // Also forces Free View to start at the camera mount position, even it is still in transition. NEEDS WORK!
                _mouseX = 0f;
                _mouseY = 0f;
                // -----From SmoothFollow standard asset script-----
                // This section will change once I better understand commands such as Mathf.Lerp
                SmoothFollow();
            }
        }
    }

    private void CameraFollowPosition()
    {
        // This section will change once the values for walkDist, heightDist, and the angle of the camera are decided

        // Set Camera position based on walkDist and heightDist
        mainCam.position = new Vector3(0f, heightDist, -walkDist) + target.position;
        // Rotates Camera to look at CamTarget
        mainCam.LookAt(target);
    }

    private void SmoothFollow()
    {
        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + heightDist;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;
        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        mainCam.position = target.position;
        mainCam.position -= currentRotation * new Vector3(0f, 0f, 1f) * walkDist;
        // Set the height of the camera
        mainCam.position = new Vector3(mainCam.position.x, currentHeight, mainCam.position.z);
        // Always look at the target
        mainCam.LookAt(target);
    }
}
