using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firePro : MonoBehaviour {

    public float lifeT;

    void Start()
    {

        lifeT = 3.9f;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * 450 * Time.deltaTime);
        Destroy(gameObject, lifeT);
    }

    void OnCollisonEnter(Collision c)
    {
        Destroy(gameObject);
    }
}
