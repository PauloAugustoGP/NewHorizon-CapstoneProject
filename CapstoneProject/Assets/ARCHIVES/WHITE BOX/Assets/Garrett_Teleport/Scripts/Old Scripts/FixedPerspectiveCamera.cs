using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPerspectiveCamera : MonoBehaviour {

    GameObject player;

    Vector3 cameraDisplacement;
    Quaternion cameraRotation;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
        cameraRotation = Quaternion.Euler(20, 180, 0);
	}
	
	// Update is called once per frame
	void Update () {
        cameraDisplacement = new Vector3(player.transform.position.x + 0.0f, player.transform.position.y + 3.0f, player.transform.position.z + 5.0f);
        transform.SetPositionAndRotation(cameraDisplacement, cameraRotation);
    }
}
