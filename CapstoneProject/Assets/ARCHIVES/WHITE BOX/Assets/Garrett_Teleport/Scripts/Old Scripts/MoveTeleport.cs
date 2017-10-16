using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTeleport : MonoBehaviour {

    GameObject location;

	// Use this for initialization
	void Start () {
		location = GameObject.FindGameObjectWithTag("TeleportLocation");
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = location.transform.position;
    }
}
