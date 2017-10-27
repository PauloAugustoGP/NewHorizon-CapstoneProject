/// ------------------------
/// CAMERA CONTROLLER SCRIPT
/// Michael Nardone
/// ------------------------
/// TO DO:
/// -Wall behaviour
/// ------------------------
/// WISHLIST:
/// -Free View (does not rotate character while in use)
/// -Dynamic inversion toggles
/// -Dynamic sensitivity modifiers
/// ------------------------

using UnityEngine;

public class CameraController : MonoBehaviour {

    // Reference to the Main Camera
    [SerializeField]
    private Transform _mainCam;

    // Camera follow distance, local position relative to Player position
    [SerializeField]
    private float _horDist;
    [SerializeField]
    private float _vertDist;

    // Used for calculating camera local position while rotating view
    private float _mouseX;
    private float _mouseY;

    // Vertical angle clamps while rotating camera
    [SerializeField]
    private float _yAngleMin;
    [SerializeField]
    private float _yAngleMax;

    // Controls the speed of the camera rotation
    // Sensitivity must be set prior to runtime
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityX;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityY;
    [SerializeField]
    private float _rotSpeedX;
    [SerializeField]
    private float _rotSpeedY;

    // Invert camera rotation (change values to -1 or 1 ONLY)
    [SerializeField]
    private float _invertX;
    [SerializeField]
    private float _invertY;

    void Start () {

        if (_mainCam == null)
        {
            Debug.LogWarning("Main Camera transform not found. Finding component.");
            _mainCam = GameObject.Find("Main Camera").GetComponent<Transform>();
        }

        _horDist = -5f;
        _vertDist = 2f;
        _mainCam.localPosition = new Vector3(0f, _vertDist, _horDist);

        _yAngleMax = 35f;
        _yAngleMin = -10f;

        // Simple check to see if a value was input in the Inspector
        if (_sensitivityX == 0f)
        {
            _sensitivityX = 0.5f;
        }

        if (_sensitivityY == 0f)
        {
            _sensitivityY = 0.4f;
        }

        _rotSpeedX = 500f * _sensitivityX;
        _rotSpeedY = 500f * _sensitivityY;

        _invertX = 1f;
        _invertY = -1f;
	}
	
	void Update () {

        // Finds the angle (in degrees) the new mouse position is making with original orientation
        _mouseX += Input.GetAxis("Mouse X") * _rotSpeedX * 0.02f * _invertX;
        _mouseY += Input.GetAxis("Mouse Y") * _rotSpeedY * 0.02f * _invertY;
        // Clamp vertical rotation
        _mouseY = Mathf.Clamp(_mouseY, _yAngleMin, _yAngleMax);
        // Convert the angles to a Quaternion value
        Quaternion rotation = Quaternion.Euler(_mouseY, _mouseX, 0f);

        // Insert wall behaviour fixes here

        // Cache the desired location of the camera in world space
        Vector3 followPosition = this.transform.TransformPoint(0f, _vertDist, -_horDist);
        // Check if there is any object behind Player
        RaycastHit hit;
        Vector3 back = this.transform.TransformDirection(new Vector3(0f, 0f, -1f));

        // Raycast
        // -need bumper variables
        // -need buffer variables

        // End wall behaviour

        // Apply horizontal rotation to player
        this.transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
        // Apply horizontal and vertical rotation to camera
        _mainCam.rotation = rotation;

    }
}
