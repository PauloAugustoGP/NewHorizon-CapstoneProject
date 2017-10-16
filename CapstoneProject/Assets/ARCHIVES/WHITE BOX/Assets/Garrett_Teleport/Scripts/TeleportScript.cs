using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{

    public GameObject fakePlayerPrefab;
    public GameObject radiusPrefab;

    private GameObject fakePlayer;
    GameObject teleportPoint;
    private GameObject radius;

    Vector3 fakePlayerSpawnLocation;


    // Use this for initialization
    void Start()
    {
        teleportPoint = GameObject.FindGameObjectWithTag("TeleportLocation");
        if(!teleportPoint)
        {
            Debug.Log("No Teleport Point Prefab added to scene.");
        }

        fakePlayerSpawnLocation = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            

            Instantiate(fakePlayerPrefab, teleportPoint.transform.position + fakePlayerSpawnLocation, teleportPoint.transform.rotation);

            fakePlayer = GameObject.FindGameObjectWithTag("FakePlayer");

            radius = Instantiate(radiusPrefab, transform.position, radiusPrefab.transform.rotation);
                
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            Destroy(fakePlayer);
            Destroy(radius);

            if (Vector3.Distance(transform.position, teleportPoint.transform.position) < 5 && Vector3.Distance(transform.position, teleportPoint.transform.position) > 1)
            {
                transform.SetPositionAndRotation(teleportPoint.transform.position, transform.rotation);
            }
        }


    }
}
