/// ------------------------
/// CAMERA CONTROLLER SCRIPT
/// Michael Nardone
/// 
/// ------------------------
/// SETUP:
/// (1) Drag CameraController script on to Player object
/// (2) Drag Main Camera into Main Cam variable
/// (3) Drag Player into Target variable
/// (4) In Layer Mask - Projectile, select Everything and deselect Actors 
/// 
/// ------------------------
/// NOTES:
/// -Changing Z Dist while in Play mode will not modify the bumper distance check
/// -STANDARD FOLLOW POSTION = (0, 2, 1.5)
/// 
/// ------------------------
/// TODO:
/// -Crosshair does not convey correct information about projectile's course
/// -Improve wall bumper behaviour using SphereCast
/// -Prevent camera from rendering through walls
/// ------------------------
/// WISHLIST:
/// -Inversion as boolean toggles
/// -Dynamic inversion toggles
/// -Dynamic sensitivity modifiers
/// -Offset camera height when looking up/down
/// -Dynamic bumper check
/// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera and Target Transform")]
    [SerializeField]
    private Transform _mainCam;
    [SerializeField]
    private Transform _target;

    [Header("Camera Follow Distance")]
    // Camera follow distance, local position relative to Player position
    [SerializeField]
    [Tooltip("Shoulder offset value. Value of 0 centres camera behind player.")]
    private float _xDist = 0f;
    [SerializeField]
    [Tooltip("Height above target's pivot.")]
    private float _yDist = 2f;   // Height above the floor (Player pivot point) camera will be
    [SerializeField]
    [Tooltip("Distance behind target's pivot.")]
    private float _zDist = 1.5f;   // Distance between camera and player (pivot point)

    // Used for calculating camera local position while rotating view
    private float _mouseX;
    private float _mouseY;

    [Header("Range of Vertical Rotation")]
    // Vertical angle clamps while rotating camera
    [SerializeField]
    private float _yAngleMin = -10f;
    [SerializeField]
    private float _yAngleMax = 35f;

    // Represents whether the camera is interacting with a wall or not
    private bool _wallBumperOn;

    // Length of raycast checking for walls
    private float _bumperHorizontalCheck;

    // Height adjustment when wall detected with raycast
    private float _bumperCameraHeight;

    [Header("Camera Movement and Rotation Speed")]
    // Speed of camera motion when avoiding wall collisions
    [SerializeField]
    private float _damping = 5f;
    [SerializeField]
    private float _rotationDamping = 10f;

    [Header("Camera Sensitivity")]
    // Controls the speed of the camera rotation
    // Sensitivity must be set prior to runtime (currently)
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityX = 0.5f;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float _sensitivityY = 0.4f;

    private float _rotSpeedX;
    private float _rotSpeedY;

    // Invert camera rotation (change values to -1 or 1 ONLY)
    private int _invertX;
    private int _invertY;

    // Toggle camera movement/rotation on/off
    private bool _freezeCamera;

    private XRay_Ability _xrayRef;

    private TeleportScript _tpRef;

    [Header("Layer Mask - Projectile")]
    [SerializeField]
    private LayerMask _playerIgnoreLM;

    // Pause behaviour for Cursor
    private bool _inGame = true;

    public bool inGame
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
            _target = GetComponent<Transform>();
        }

        //_zDist = 2f;
        //_yDist = 2f;
        //_xDist = 0.25f;

        _mainCam.SetParent(_target);
        _mainCam.localPosition = new Vector3(_xDist, _yDist, -_zDist);

        //_yAngleMin = -10f;
        //_yAngleMax = 35f;

        _bumperHorizontalCheck = _zDist + 0.5f;
        _bumperCameraHeight = _yDist + 0.5f;

        //_damping = 5f;
        //_rotationDamping = 10f;

        //_sensitivityX = 0.5f;
        //_sensitivityY = 0.4f;

        _rotSpeedX = 500f * _sensitivityX;
        _rotSpeedY = 500f * _sensitivityY;

        _invertX = 1;
        _invertY = -1;

        _xrayRef = GetComponent<XRay_Ability>();
        if (!_xrayRef)
        {
            _xrayRef = GetComponent<XRay_Ability>();
        }

        if (!_tpRef)
        {
            _tpRef = GetComponent<TeleportScript>();
        }

        Cursor.lockState = CursorLockMode.Locked;

        Camera.main.nearClipPlane = 0.1f;
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
            if (!_xrayRef.GetIsInXRay())
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
        RaycastHit hitInfo;
        Vector3 rayDir = -_target.forward;
        Vector3 rayOrigin = _target.position + _target.up;
        //Vector3 rayLeft = _target.position + _target.up + _target.right * -0.5f;
        //Vector3 rayRight = _target.position + _target.up + _target.right * 0.5f;

        //if (Physics.Raycast(rayLeft, rayDir, out hitInfo, _bumperHorizontalCheck, _playerIgnoreLM, QueryTriggerInteraction.Ignore)
        //    || Physics.Raycast(rayRight, rayDir, out hitInfo, _bumperHorizontalCheck, _playerIgnoreLM, QueryTriggerInteraction.Ignore))

        Debug.DrawRay(rayOrigin, rayDir * _bumperHorizontalCheck, Color.red);

        if (Physics.Raycast(rayOrigin, rayDir, out hitInfo, _bumperHorizontalCheck, _playerIgnoreLM, QueryTriggerInteraction.Ignore))
        {
            //Debug.DrawRay(rayLeft, rayDir * _bumperHorizontalCheck, Color.red);
            //Debug.DrawRay(rayRight, rayDir * _bumperHorizontalCheck, Color.green);

            _wallBumperOn = true;
            Debug.DrawLine(rayOrigin, hitInfo.point, Color.green);
    
            followPosition.x = hitInfo.point.x;

            //if (hitInfo.point.x < _mainCam.position.x)
            //{
            //    followPosition.x += 0.4f;
            //}
            //else if (hitInfo.point.x > _mainCam.position.x)
            //{
            //    followPosition.x -= 0.4f;
            //}

            followPosition.z = hitInfo.point.z;

            if (hitInfo.point.z < _mainCam.position.z)
            {
                followPosition.z += 0.5f;
            }
            else if (hitInfo.point.z > _mainCam.position.z)
            {
                followPosition.z -= 0.5f;
            }
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
        
        if (_xrayRef.GetIsInXRay())
        {
            _mouseX = Mathf.Clamp(_mouseX, _target.localEulerAngles.y - 90f, _target.localEulerAngles.y + 90f);
        }

        return Quaternion.Euler(_mouseY, _mouseX, 0f);
    }

    //This list of for the GetCenterView function!!
    List<Collider> collidersHit = new List<Collider>();

    // Raycasts from the camera's centre of view and returns the first point of collision
    public Vector3 GetCentreView(Vector3 pRayOrigin, LayerMask pIgnorePlayer)
    {
        bool finished = false;

        float xPos = Screen.width / 2f;
        float yPos = Screen.height / 2f;

        float maxDistance = 50.0f;

        RaycastHit hit;
        Ray ray = new Ray(pRayOrigin, _mainCam.forward);

        while (!finished)
        {
            if (Physics.Raycast(ray, out hit, maxDistance, pIgnorePlayer))
            {
                if (Vector3.Distance(hit.point, pRayOrigin) <= 2.0f)
                {
                    collidersHit.Add(hit.collider);
                    hit.collider.enabled = false;

                    return GetCentreView(pRayOrigin, pIgnorePlayer);
                }
                else
                {
                    if (collidersHit.Count > 0)
                    {
                        foreach (Collider c in collidersHit)
                            c.enabled = true;
                        collidersHit.Clear();
                    }

                    return hit.point;
                }
            }
            else
            {
                if (collidersHit.Count > 0)
                {
                    foreach (Collider c in collidersHit)
                        c.enabled = true;
                    collidersHit.Clear();
                }

                return pRayOrigin + (_mainCam.forward * maxDistance);
            }
        }

        return pRayOrigin + (_mainCam.forward * maxDistance);
    }
}
