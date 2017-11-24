// Teleport Mechanic
// Author: Garrett May

// Instructions for Use:
// Attach this script to the player
// Attach Teleport Point Prefab (Garrett's Folder/Prefabs) into the scene as a child of the player
// Attach Player Shadow Prefab (Garrett's Folder/Prefabs) into the scene as a child of the player
// Attach Teleport Radius (Garrett's Folder/Prefabs) into the scene as a child of the player
// Drag the newly added Teleport point prefab into the Serialized Field "Teleport Point" that appears on this script in the inspector
// Drag the newly added Player Shadow prefab into the Serialized Field "Fake Player" that appears on this script in the inspector
// Drag the newly added Teleport Radius prefab into the Serialized Field "Radius" that appears on this script in the inspector
// Check each newly added prefabs transform positions and make sure they are all set to (0,0,0);
// Check to make sure that the camera in the scene is tagged as "MainCamera"




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    // fake player, teleport point and radius objects assigned in the inspector
    [SerializeField] private GameObject _fakePlayer;
    [SerializeField] private GameObject _teleportPoint;
    [SerializeField] private GameObject _radius;


    // minimum and maximum teleport distances
    /*[SerializeField]*/ private float _maxTeleportDistance = 10;
    [SerializeField] private float _minTeleportDistance = 1;

    // used for cooldown of teleport
    [SerializeField] private bool _isCooled;
    // used for making sure player cannot hold down shift while cooling down then not be able to see radius
    private bool _isActive;

    // layermask for Raycast Collision
    [SerializeField]
    LayerMask _layerMask;

    private Transform _playerShadowPosition;

    // Use this for initialization
    void Start()
    {
        // make sure there is a teleport point in the scene
        if(!_teleportPoint)
        {
            Debug.Log("No Teleport Point Prefab added to Player.");
            _teleportPoint = GameObject.Find("TeleportPoint");
        }
        // make sure there is a Player Shadow in the scene
        if (!_fakePlayer)
        {
            Debug.Log("No player shadow prefab added to scene.");
            _fakePlayer = GameObject.Find("PlayerShadow");
        }
        // make sure there is a teleport radius in the scene
        if (!_radius)
        {
            Debug.Log("No teleport radius added to scene");
            _radius = GameObject.Find("TeleportRadius");
        }

        // initializing variables
        _isCooled = true;
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // raycast creation
        RaycastHit tempHit;
        // raycast points to mouse location
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if raycast hits something
        if (Physics.Raycast(tempRay, out tempHit, 1000, _layerMask))
        {
            // set the teleport points position to the raycasts hit point    
            _teleportPoint.transform.position = tempHit.point;
        }
        else Debug.Log("Raycast not working properly!");

        // set position of fake player to the position of the teleport point
        if (Vector3.Distance(transform.position, _teleportPoint.transform.position) < _maxTeleportDistance)
            _fakePlayer.transform.position = _teleportPoint.transform.position;
        else
        {
            _fakePlayer.transform.localPosition = Vector3.Normalize(_teleportPoint.transform.localPosition) * _maxTeleportDistance;
        }

        // draw ray so we can see in inspector
        Debug.DrawRay(tempRay.origin, tempRay.direction, Color.green);

        // check to make sure cooldown is finished
        if (_isCooled)
        {
            // initiate teleport with shift key
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // activate the fake player and radius for visuals
                _fakePlayer.SetActive(true);
                _radius.SetActive(true);
                //signal the teleport is active
                _isActive = true;
            }

            if(Input.GetKeyDown(KeyCode.Mouse1) && _isActive)
            {
                _fakePlayer.SetActive(false);
                _radius.SetActive(false);
                _isCooled = true;
                _isActive = false;
            }
            // finish teleport by letting go of shift, also make sure teleport was activated
            if (Input.GetKeyUp(KeyCode.LeftShift) && _isActive)
            {
                // deactivate visuals
                _fakePlayer.SetActive(false);
                _radius.SetActive(false);
                //start cool down 
                StartCoroutine(Cooldown());
                _isCooled = false;
                // teleport finished
                _isActive = false;

                // set this objects position to the fake player position
                // currently limiting movement in y to the players current y value
                // transform.SetPositionAndRotation(new Vector3(_fakePlayer.transform.position.x, transform.position.y, _fakePlayer.transform.position.z), transform.rotation);

                // delayed teleport testing
                _playerShadowPosition = _fakePlayer.transform;
                StartCoroutine(DelayedTeleport(_playerShadowPosition));
            }
        }
    }

    public bool isActive
    {
        get { return _isActive; }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2.0f);
        _isCooled = true;
    }

    private IEnumerator DelayedTeleport(Transform tempTeleportPosition)
    {
        yield return new WaitForSeconds(0.2f);
        transform.SetPositionAndRotation(new Vector3(tempTeleportPosition.position.x, transform.position.y, tempTeleportPosition.position.z), transform.rotation);
    }
}
