/// ------------------------
/// CAMERA CONTROLLER SCRIPT
/// Michael Nardone
/// ------------------------
/// SETUP:
/// 1) Add script to player object
/// 2) Add Main Camera to variable Main Cam (_mainCam) variable in Inspector
/// 3) Make Main Camera a child of player
/// ------------------------
/// NOTES:
/// -If you modify zDist *while running*, must change bumperHorizontalCheck as well (must be 0.5 larger than zDist)
/// -To change sensitivity or inversion values, change in Inspector before running
///     -invertX and invertY can *only* be -1 or 1, I will change this to a boolean value later
/// -damping and rotationDamping should be based off the player's movement speed (how fast the camera pushes away from collisions)
/// ------------------------
/// WISHLIST:
/// -Inversion as boolean toggles
/// -Dynamic inversion toggles
/// -Dynamic sensitivity modifiers
/// -Offset camera height when looking up/down
/// -Dynamic bumper check
/// ------------------------

using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Camera")]
    // Reference to the Main Camera
    [SerializeField]
    private Transform _mainCam;

    [Header("Camera Follow Position")]
    // Camera follow distance, local position relative to Player position
    [SerializeField]
    private float _zDist;   // Horizontal distance (on Z axis) camera is from player
    [SerializeField]
    private float _yDist;   // Vertical distance camera is from player
    [SerializeField]
    private float _xDist;   // Horizontal distance (on X axis) camera is from player

    // Used for calculating camera local position while rotating view
    private float _mouseX;
    private float _mouseY;

    [Header("Camera Vertical Angle Clamps")]
    // Vertical angle clamps while rotating camera
    [SerializeField]
    private float _yAngleMin;
    [SerializeField]
    private float _yAngleMax;

    // Represents whether the camera is interacting with a wall or not
    private bool _wallBumperOn;

    [Header("Camera Bumper")]
    // Length of raycast checking for walls
    [SerializeField]
    private float _bumperHorizontalCheck;

    // Height adjustment when wall detected with raycast
    [SerializeField]
    private float _bumperCameraHeight;

    // Speed of camera motion when avoiding wall collisions
    [SerializeField]
    private float _damping;
    [SerializeField]
    private float _rotationDamping;

    [Header("Camera Sensitivity and Control")]
    // Controls the speed of the camera rotation
    // Sensitivity must be set prior to runtime
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityX;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityY;
    //[SerializeField]
    private float _rotSpeedX;
    //[SerializeField]
    private float _rotSpeedY;

    // Invert camera rotation (change values to -1 or 1 ONLY)
    [SerializeField]
    private int _invertX;
    [SerializeField]
    private int _invertY;

    // Toggle camera movement/rotation on/off, intended for testing
    [SerializeField]
    private bool _freezeCamera;

    private XRay_PlayerScript _xrayRef;

    void Start ()
    {
        if (_mainCam == null)
        {
            Debug.LogWarning("Main Camera transform not found. Finding component.");
            _mainCam = GameObject.Find("Main Camera").GetComponent<Transform>();
        }

        _zDist = 3f;
        _yDist = 1f;
        _xDist = 0f;

        _yAngleMin = -10f;
        _yAngleMax = 35f;

        _mainCam.localPosition = new Vector3(_xDist, _yDist, -_zDist);

        _bumperHorizontalCheck = _zDist + 0.5f;
        _bumperCameraHeight = 1.5f;

        _damping = 5f;
        _rotationDamping = 10f;

        _sensitivityX = 0.5f;
        _sensitivityY = 0.4f;

        _rotSpeedX = 500f * _sensitivityX;
        _rotSpeedY = 500f * _sensitivityY;

        _invertX = 1;
        _invertY = -1;

        _xrayRef = GetComponent<XRay_PlayerScript>();
        if (!_xrayRef)
        {
            Debug.LogWarning("Xray reference not found.");
        }
	}
	
	void LateUpdate ()
    {
        // This is primarily for testing purposes, and freezes camera controls
        if (Input.GetKeyUp(KeyCode.P))
        {
            _freezeCamera = !_freezeCamera;
        }

        // Left Shift - Teleport control, freezes camera while held down
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
            Vector3 followPosition = this.transform.TransformPoint(_xDist, _yDist, -_zDist);
            // Check if there is any object behind Player
            RaycastHit hit;
            Vector3 back = this.transform.TransformDirection(new Vector3(0f, 0f, -1f));

            if (Physics.Raycast(this.transform.position, back, out hit, _bumperHorizontalCheck))
            {
                //Debug.DrawLine(this.transform.position, hit.point, Color.red);
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
                followPosition.y = Mathf.Lerp(hit.point.y + _bumperCameraHeight, followPosition.y, Time.deltaTime * _damping);
            }
            else
            {
                _wallBumperOn = false;
            }


            // Position the camera away from the wall, based on followPosition
            _mainCam.position = Vector3.Lerp(_mainCam.position, followPosition, Time.deltaTime * _damping);

            if (!_freezeCamera)
            {
                if (_wallBumperOn)
                {
                    // Find the correct rotation for the camera
                    Quaternion wantedRotation = Quaternion.LookRotation(this.transform.position - _mainCam.position, this.transform.up);
                    _mainCam.rotation = Quaternion.Slerp(_mainCam.rotation, wantedRotation, Time.deltaTime * _rotationDamping);
                }
                // Apply horizontal and vertical rotation to camera
                _mainCam.rotation = rotation;

                //Apply horizontal rotation to player, if Xray is _not_ active
                if (!_xrayRef.xrayActive)
                {
                    this.transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
                }

                // If using a Player prefab WITHOUT Xray feature, use this line instead of above.
                //this.transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
            }
        }
    }

    // Raycasts from the camera's centre of view and returns the first point of collision
	public Vector3 GetCentreView(Vector3 pRayOrigin)
	{
		float xPos = Screen.width / 2f;
		float yPos = Screen.height / 2f;

		float maxDistance = 50.0f;

		//Vector3 rayOrigin = Camera.main.ViewportToScreenPoint(new Vector3(xPos, yPos, _mainCam.position.z));
		RaycastHit hit;

		//Ray rayScreen = Camera.main.ScreenPointToRay(new Vector3(xPos, yPos));

		// This ray *should* represent where the the centre of the camera view is.
		// Ignore the length of the ray in this Debug statement.
		// hit.point represents the first collision from the centre of the camera.
		//Debug.DrawRay(rayScreen.origin, _mainCam.forward * 100, Color.red);

		if (Physics.Raycast(pRayOrigin, _mainCam.forward, out hit, maxDistance))
		{ 
			return hit.point;
		}
		else
		{
			return pRayOrigin + (_mainCam.forward * maxDistance);
		}
	}
}
