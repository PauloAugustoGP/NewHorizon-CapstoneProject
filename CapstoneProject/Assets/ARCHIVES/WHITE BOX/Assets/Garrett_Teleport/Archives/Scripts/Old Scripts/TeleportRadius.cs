using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRadius : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
	}
}
