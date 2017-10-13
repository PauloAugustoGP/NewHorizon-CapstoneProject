using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTeleport : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject playerTeleportPrefab;

    public GameObject fakePlayer;
    GameObject teleportPoint;
  

	// Use this for initialization
	void Start () {
        teleportPoint = GameObject.FindGameObjectWithTag("TeleportLocation");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            

           

            Instantiate(playerTeleportPrefab, teleportPoint.transform.position, teleportPoint.transform.rotation);
            fakePlayer = GameObject.FindGameObjectWithTag("TeleportCharacter");
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            
            Destroy(fakePlayer);

            if (Vector3.Distance(transform.position, teleportPoint.transform.position) < 5 && Vector3.Distance(transform.position, teleportPoint.transform.position) > 1)
            {
                transform.SetPositionAndRotation(teleportPoint.transform.position, transform.rotation);
            }
        }


	}
}
