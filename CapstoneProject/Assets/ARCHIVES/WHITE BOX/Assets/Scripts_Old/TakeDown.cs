using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : MonoBehaviour {

    

    public float lifeTime; 

	// Use this for initialization
	void Start ()
    {
        lifeTime = 1;

        Destroy(gameObject, lifeTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
