using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    // fake player, teleport point and radius objects assigned in the inspector
    [SerializeField] private GameObject fakePlayer;
    [SerializeField] private GameObject teleportPoint;
    [SerializeField] private GameObject radius;


    // minimum and maximum teleport distances
    [SerializeField] private float maxTeleportDistance = 5;
    [SerializeField] private float minTeleportDistance = 1;

    // used for cooldown of teleport
    [SerializeField] private bool isCooled;
    // used for making sure player cannot hold down shift while cooling down then not be able to see radius
    private bool isActive;


    // Use this for initialization
    void Start()
    {


        // make sure there is a teleport point in the scene
        if(!teleportPoint)
        {
            Debug.Log("No Teleport Point Prefab added to scene.");
        }

        // make sure there is a Player Shadow in the scene
        if(!fakePlayer)
        {
            Debug.Log("No player shadow prefab added to scene.");
        }

        // initializing variables
        isCooled = true;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // raycast creation
        RaycastHit hit;
        // raycast points to mouse location
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if raycast hits something
        if (Physics.Raycast(ray, out hit))
        {
            // check to see if the distance between hit and player is less than the max teleport distance
            if (Vector3.Distance(transform.position, hit.point) < maxTeleportDistance)
            {
                // move the teleport point to the hit location of raycast
                teleportPoint.transform.position = hit.point;
            }
        }
        else Debug.Log("Raycast not working properly!");

        // set position of fake player to the position of the teleport point
        fakePlayer.transform.position = teleportPoint.transform.position;

        // draw ray so we can see in inspector
        Debug.DrawRay(ray.origin, ray.direction, Color.green);

        // check to make sure cooldown is finished
        if (isCooled)
        {
            // initiate teleport with shift key
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // activate the fake player and radius for visuals
                fakePlayer.SetActive(true);
                radius.SetActive(true);
                //signal the teleport is active
                isActive = true;
            }
            // finish teleport by letting go of shift, also make sure teleport was activated
            if (Input.GetKeyUp(KeyCode.LeftShift) && isActive)
            {
                // deactivate visuals
                fakePlayer.SetActive(false);
                radius.SetActive(false);
                //start cool down 
                StartCoroutine(Cooldown());
                isCooled = false;
                // teleport finished
                isActive = false;

                // check if the teleport point is within acceptable range (max teleport distance and min teleport distance)
                if (Vector3.Distance(transform.position, teleportPoint.transform.position) < maxTeleportDistance && Vector3.Distance(transform.position, teleportPoint.transform.position) > minTeleportDistance)
                {
                    // set this objects position to the location of the teleport point
                    transform.SetPositionAndRotation(teleportPoint.transform.position, transform.rotation);
                }
            }

        }
        


    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2.0f);
        isCooled = true;
    }
}
