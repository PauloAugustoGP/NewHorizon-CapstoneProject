using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour {

    Level_Manager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.FindObjectOfType<Level_Manager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            levelManager.PlayerDeath();
        }
    }
}
