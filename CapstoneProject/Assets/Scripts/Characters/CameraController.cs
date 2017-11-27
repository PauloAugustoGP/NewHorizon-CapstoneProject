/// ------------------------
/// CAMERA CONTROLLER SCRIPT
/// Michael Nardone
/// ------------------------
/// SETUP:
///  
/// TBD
/// 
/// ------------------------
/// NOTES:
/// -If you modify zDist *while running*, must change bumperHorizontalCheck as well (must be 0.5 larger than zDist)
/// -To change sensitivity or inversion values, change in Inspector before running
///     -invertX and invertY can *only* be -1 or 1, I will change this to a boolean value later
/// -damping and rotationDamping should be based off the player's movement speed (how fast the camera pushes away from collisions)
/// ------------------------
/// TODO:
/// -Modify variables from SERIALIZED to PUBLIC
/// -Pause check
/// -FIX bumper behaviour with Player Prefab
/// -Projectile and camera ==> appearance of looking straight when actually looking down, ADD crosshair
/// ------------------------
/// WISHLIST:
/// -Inversion as boolean toggles
/// -Dynamic inversion toggles
/// -Dynamic sensitivity modifiers
/// -Offset camera height when looking up/down
/// -Dynamic bumper check
/// ------------------------

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _mainCam;
    [SerializeField]
    private Transform _target;

    // Camera follow distance, local position relative to Player position
    [SerializeField]
    [Tooltip("Shoulder offset value. Value of 0 centres camera behind player.")]
    private float _xDist;
    [SerializeField]
    [Tooltip("Height above target's pivot.")]
    private float _yDist;   // Height above the floor (Player pivot point) camera will be
    [SerializeField]
    [Tooltip("Distance behind target's pivot.")]
    private float _zDist;   // Distance between camera and player (pivot point)

    // Used for calculating camera local position while rotating view
    private float _mouseX;
    private float _mouseY;

    // Vertical angle clamps while rotating camera
    private float _yAngleMin;
    private float _yAngleMax;

    // Represents whether the camera is interacting with a wall or not
    private bool _wallBumperOn;

    // Length of raycast checking for walls
    private float _bumperHorizontalCheck;

    // Height adjustment when wall detected with raycast
    private float _bumperCameraHeight;

    // Speed of camera motion when avoiding wall collisions
    [SerializeField]
    private float _damping;
    [SerializeField]
    private float _rotationDamping;

    [Header("Camera Sensitivity and Control")]
    // Controls the speed of the camera rotation
    // Sensitivity must be set prior to runtime (currently)
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityX;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityY;

    private float _rotSpeedX;
    private float _rotSpeedY;

    // Invert camera rotation (change values to -1 or 1 ONLY)
    private int _invertX;
    private int _invertY;

    // Toggle camera movement/rotation on/off, intended for testing, CONTROL: [P]
    private bool _freezeCamera;

    private XRay_PlayerScript _xrayRef;

    private TeleportScript _tpRef;

    private CursorLockMode wantedMode;

    [Header("Layer Mask - Projectile")]
    [SerializeField]
    private LayerMask _playerIgnoreLM;

    // Pause behaviour for Cursor
    private bool _inGame = true;

    public bool inGame  // can remove extra code here
    {
        get
        {
            return _inGame;
        }
        set
        {
            _inGame = value;
        }
    }

    void Start()
    {
        if (!_mainCam)
        {
            _mainCam = GameObject.Find("Main Camera").GetComponent<Transform>();
        }

        if (!_target)
        {
            _target = GameObject.Find("Player").GetComponent<Transform>();
        }

        _zDist = 2f;
        _yDist = 2f;
        _xDist = 0.25f;

        _mainCam.SetParent(_target);
        _mainCam.localPosition = new Vector3(_xDist, _yDist, -_zDist);

        _yAngleMin = -10f;
        _yAngleMax = 35f;

        _bumperHorizontalCheck = _zDist + 1f;
        _bumperCameraHeight = _yDist + 0.5f;

        _damping = 5f;
        _rotationDamping = 10f;

        _sensitivityX = 0.5f;
        _sensitivityY = 0.4f;

        _rotSpeedX = 500f * _sensitivityX;
        _rotSpeedY = 500f * _sensitivityY;

        _invertX = 1;
        _invertY = -1;

        _xrayRef = GameObject.Find("Player").GetComponent<XRay_PlayerScript>();

        _tpRef = GameObject.Find("Player").GetComponent<TeleportScript>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        // Sets Cursor state (locked/visible/not) and Camera state (freeze/not)
        CheckGameState();

        if (!_freezeCamera)
        {
            // Move the camera to the desired position
            _mainCam.position = Vector3.Lerp(_mainCam.position, FindPosition(), 0.02f * _damping);

            // Apply horizontal and vertical rotation to camera
            _mainCam.rotation = Quaternion.Lerp(_mainCam.rotation, FindRotation(), 0.02f * _rotationDamping);

            //Apply horizontal rotation to player, if Xray is _not_ active
            if (!_xrayRef.xrayActive)
            {
                // This adds a slight lag behind for the player's rotation
                _target.rotation = Quaternion.Lerp(_target.rotation, Quaternion.Euler(0f, _mouseX, 0f), 0.02f * _rotationDamping);

                // This will remove the slight lag and the player's rotation is hard-set, might cause jittery rotations
                //this.transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);
            }
        }
    }

    private void CheckGameState()
    {
        if (_inGame)
        {
            if (_tpRef.isActive)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                _freezeCamera = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                _freezeCamera = false;
            }
        }
        else if (!_inGame)
        {
            _freezeCamera = true;
        }
    }

    private Vector3 FindPosition()
    {
        // Cache the desired location of the camera in world space
        Vector3 followPosition = _target.TransformPoint(_xDist, _yDist, -_zDist);

        // Check if there is any object behind Player
        RaycastHit hit;
        Vector3 back = -_target.forward;
        Ray ray = new Ray(_target.position + new Vector3(0f, 1f, 0f), back);

        if (Physics.Raycast(ray, out hit, _bumperHorizontalCheck, _playerIgnoreLM, QueryTriggerInteraction.Ignore))
        {
            _wallBumperOn = true;

            followPosition.x = hit.point.x;
            followPosition.z = hit.point.z;
            //followPosition.y = Mathf.Lerp(hit.point.y + _bumperCameraHeight, followPosition.y, Time.deltaTime * _damping);
        }
        else
        {
            _wallBumperOn = false;
        }

        return followPosition;
    }

    private Quaternion FindRotation()
    {
        // Finds the angle (in degrees) the new mouse position is making with original orientation
        _mouseX += Input.GetAxis("Mouse X") * _rotSpeedX * _invertX * 0.02f;
        _mouseY += Input.GetAxis("Mouse Y") * _rotSpeedY * _invertY * 0.02f;

        // Clamp vertical rotation
        _mouseY = Mathf.Clamp(_mouseY, _yAngleMin, _yAngleMax);

        return Quaternion.Euler(_mouseY, _mouseX, 0f);
    }

    // Raycasts from the camera's centre of view and returns the first point of collision
    public Vector3 GetCentreView(Vector3 pRayOrigin)
    {
        float xPos = Screen.width / 2f;
        float yPos = Screen.height / 2f;

        float maxDistance = 50.0f;

        //Vector3 rayOrigin = Camera.main.ViewportToScreenPoint(new Vector3(xPos, yPos, _mainCam.position.z));
        RaycastHit hit;

        Ray ray = new Ray(pRayOrigin, _mainCam.forward);

        // This ray *should* represent where the the centre of the camera view is.
        // Ignore the length of the ray in this Debug statement.
        // hit.point represents the first collision from the centre of the camera.
        //Debug.DrawRay(rayScreen.origin, _mainCam.forward * 100, Color.red);

        if (Physics.Raycast(ray, out hit, maxDistance, _playerIgnoreLM))
        {
            return hit.point;
        }
        else
        {
            return pRayOrigin + (_mainCam.forward * maxDistance);
        }
    }
}
