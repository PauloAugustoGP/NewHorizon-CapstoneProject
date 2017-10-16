using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : MonoBehaviour {


    //meleeScript goes on the character
    //takedown script goes on the meleePrefab

    public float lifeTime; 

	// Use this for initialization
	void Start ()
    {
        lifeTime = 0.05f;

        Destroy(gameObject, lifeTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
