/// CAMERA CONTROL SCRIPT
/// ---------------------
/// 3rd person perspective
/// Free View, right click to enter, user can rotate camera freely horizontally, angles clamped vertically (-10, 35)
/// ---------------------
/// Things which can be changed:
/// -Right click control (SEE: Beginning of LateUpdate() )
/// -walkDist (If changed, requires yAngleMin and yAngleMax to be tweaked)
/// -heightDist (Currently, if changed, causes camera snapping behaviour)
/// -heightDamping, rotationDamping, rotSpeedX, rotSpeedY

using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    #region Initialization - Variable definitions and initial values
    // Cachce main camera and target transforms
    [SerializeField]
    private Transform _mainCam;
    [SerializeField]
    private Transform _target;
    // Camera mount distances from _target
    // Using mainCam.LookAt(_target) command, so this also decides camera's natural angle towards player
    [SerializeField]
    private float _walkDist;
    [SerializeField]
    private float _heightDist;
    // Controls speed of camera transition after user exits Free View
    [SerializeField]
    private float _heightDamping;
    [SerializeField]
    private float _rotationDamping;
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
    // True means user is in Free View camera, false otherwise
    private bool _cameraOn;

    private void Awake()
    {
        if (_mainCam == null)
        {
            Debug.LogWarning("Main Camera Transform not found, finding component.");
            _mainCam = GetComponent<Transform>();
        }

        if (_target == null)
        {
            Debug.LogWarning("Target Transform not found, finding component.");
            _target = GameObject.Find("Player").GetComponent<Transform>();
        }

        _walkDist = 4f;
        _heightDist = 1f;

        _heightDamping = 2f;
        _rotationDamping = 3f;

        _mouseX = 0f;
        _mouseY = 0f;

        _yAngleMin = -10;
        _yAngleMax = 35;

        _rotSpeedX = 250f;
        _rotSpeedY = 120f;

        _cameraOn = false;
    }
    #endregion

    void Start () {
        CameraFollowPosition();
    }

    private void LateUpdate()
    {
        // Toggle Free View camera on
        if (Input.GetMouseButtonDown(1))
        {
            _cameraOn = true;
            // Prevents snapping behaviour in camera.
            // There is a slight shift when entering Free View, unsure how to remove.
            _mouseX = _mainCam.eulerAngles.y;
            _mouseY = _mainCam.eulerAngles.x;
        }
        // Toggle Free View camera off
        if (Input.GetMouseButtonUp(1))
        {
            _cameraOn = false;
        }

        if (_cameraOn)
        {
            // Calculate the X and Y pos to transition the camera to,
            // multiplied by rotation speed and standard value of Time.fixedDeltaTime
            _mouseX += Input.GetAxis("Mouse X") * _rotSpeedX * 0.02f;
            _mouseY += Input.GetAxis("Mouse Y") * _rotSpeedY * -0.02f; // Negative value inverts Y-axis control
            // Clamp vertical rotation of camera
            _mouseY = Mathf.Clamp(_mouseY, _yAngleMin, _yAngleMax);

            // Convert euler angles to Unity Quaternion
            Quaternion rotation = Quaternion.Euler(_mouseY, _mouseX, 0);
            // 
            Vector3 position = rotation * new Vector3(0f, 0f, -_walkDist) + _target.position;

            _mainCam.rotation = rotation;
            _mainCam.position = position;
        }
        else
        {
            // This reset of X and Y pos prevents a snap-transition to the last position the previous time user entered Free View
            // Also forces Free View to start at the camera mount position, even it is still in transition. NEEDS WORK!
            _mouseX = 0f;
            _mouseY = 0f;
            // -----From SmoothFollow standard asset script-----
            // This section may change once I better understand commands such as Mathf.Lerp and Quaternions
            SmoothFollow();
        }
    }

    private void CameraFollowPosition()
    {
        // Set Camera position based on walkDist and heightDist
        _mainCam.position = new Vector3(0f, _heightDist, -_walkDist) + _target.position;
        // Rotates Camera to look at CamTarget
        _mainCam.LookAt(_target);
    }

    private void SmoothFollow()
    {
        // Calculate the current and desired rotation and height of the camera
        float wantedRotationAngle = _target.eulerAngles.y;
        float wantedHeight = _target.position.y + _heightDist;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;
        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);
        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);
        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        _mainCam.position = _target.position;
        _mainCam.position -= currentRotation * new Vector3(0f, 0f, 1f) * _walkDist;
        // Set the height of the camera
        _mainCam.position = new Vector3(_mainCam.position.x, currentHeight, _mainCam.position.z);
        // Always look at the target
        _mainCam.LookAt(_target);
    }
}
