/// ------------------------
/// CAMERA CONTROLLER SCRIPT
/// Michael Nardone
/// ------------------------
/// SETUP:
/// 1) Add script to player
/// 2) Add Main Camera to variable Main Cam (_mainCam) variable in Inspector
/// 3) Make Main Camera a child of player
/// ------------------------
/// NOTES:
/// -If you modify horDist or vertDist, must change bumperDistanceCheck as well (must be at least 0.5 larger)
/// -To change sensitivity or inversion values, change in Inspector before running
/// -damping and rotationDamping should be based off the player's movement speed (how fast the camera pushes away from collisions)
/// ------------------------
/// TO DO:
/// -Wall behaviour
/// ------------------------
/// WISHLIST:
/// -Dynamic inversion toggles
/// -Dynamic sensitivity modifiers
/// -Offset camera height when looking up/down
/// ------------------------

using UnityEngine;

public class CameraController : MonoBehaviour {

    // Reference to the Main Camera
    [SerializeField]
    private Transform _mainCam;

    // Camera follow distance, local position relative to Player position
    [SerializeField]
    private float _zDist;   // Horizontal distance (on Z axis) camera is from player
    [SerializeField]
    private float _yDist;   // Vertical distance camera is from player
    [SerializeField]
    private float _xDist;    // Horizontal distance (on X axis) camera is from player

    // Used for calculating camera local position while rotating view
    private float _mouseX;
    private float _mouseY;

    // Vertical angle clamps while rotating camera
    [SerializeField]
    private float _yAngleMin;
    [SerializeField]
    private float _yAngleMax;

    // Represents whether the camera is interacting with a wall or not
    private bool _wallBumperOn;

    // Length of raycast checking for walls
    [SerializeField]
    private float bumperDistanceCheck;

    // Height adjustment when wall detected with raycast
    [SerializeField]
    private float bumperCameraHeight;

    // Speed of camera motion when avoiding wall collisions
    [SerializeField]
    private float damping;
    [SerializeField]
    private float rotationDamping;

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

        _zDist = -5f;
        _yDist = 2f;
        _xDist = 0f;
        _mainCam.localPosition = new Vector3(_xDist, _yDist, _zDist);

        _yAngleMax = 35f;
        _yAngleMin = -10f;

        _wallBumperOn = false;

        bumperDistanceCheck = 5.5f;
        bumperCameraHeight = 2f;

        damping = 5f;
        rotationDamping = 10f;

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
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            // Finds the angle (in degrees) the new mouse position is making with original orientation
            _mouseX += Input.GetAxis("Mouse X") * _rotSpeedX * 0.02f * _invertX;
            _mouseY += Input.GetAxis("Mouse Y") * _rotSpeedY * 0.02f * _invertY;
            // Clamp vertical rotation
            _mouseY = Mathf.Clamp(_mouseY, _yAngleMin, _yAngleMax);
            // Convert the angles to a Quaternion value
            Quaternion rotation = Quaternion.Euler(_mouseY, _mouseX, 0f);

            // If near a wall, cache the desired location of the camera in world space
            Vector3 followPosition = this.transform.TransformPoint(_xDist, _yDist, _zDist);
            // Check if there is any object behind Player
            RaycastHit hit;
            Vector3 back = this.transform.TransformDirection(new Vector3(0f, 0f, -1f));

            // Check behind player, 
            if (Physics.Raycast(this.transform.position, back, out hit, bumperDistanceCheck))
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.red);
                _wallBumperOn = true;

                float xBuffer = 1f;
                float zBuffer = 1f;

                if (this.transform.position.x - hit.point.x < 0)
                {
                    xBuffer = -1f;
                }

                if (this.transform.position.z - hit.point.z < 0)
                {
                    zBuffer = -1f;
                }

                followPosition.x = hit.point.x + xBuffer;
                followPosition.z = hit.point.z + zBuffer;
                followPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, followPosition.y, Time.deltaTime * damping);
            }
            else
            {
                _wallBumperOn = false;
                followPosition = this.transform.TransformPoint(_xDist, _yDist, _zDist);
            }


            // Position the camera away from the wall, based on followPosition
            _mainCam.position = Vector3.Lerp(_mainCam.position, followPosition, Time.deltaTime * damping);

            if (_wallBumperOn)
            {
                // Find the correct rotation for the camera
                Quaternion wantedRotation = Quaternion.LookRotation(this.transform.position - _mainCam.position, this.transform.up);
                _mainCam.rotation = Quaternion.Slerp(_mainCam.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }

            // Apply horizontal rotation to player
            this.transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
            // Apply horizontal and vertical rotation to camera
            _mainCam.rotation = rotation;
        }

    }
}
