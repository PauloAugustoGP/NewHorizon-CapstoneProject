using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public GameObject meleePrefab;
    public Transform meleeSpawn;

    //meleeScript goes on the character
    //takedown script goes on the meleePrefab

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.M) /*&& isCrouched*/)
        {
            Instantiate(meleePrefab, meleeSpawn.position, meleeSpawn.rotation);
            //animation for grabbing for the enemy
        }
    }


    /*
     on the enemy script, add the code for the animation on taking the enemy down with the meleePrefab, and the code for damage to the enemy
     */
}
