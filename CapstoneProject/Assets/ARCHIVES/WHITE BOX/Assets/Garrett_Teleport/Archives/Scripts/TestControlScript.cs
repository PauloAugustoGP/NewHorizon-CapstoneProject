using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControlScript : MonoBehaviour {

    float rotationSpeed = 4.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 10 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -10 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            // transform.Translate(new Vector3(-10 * Time.deltaTime, 0, 0));
            transform.Rotate(0, -rotationSpeed, 0);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            // transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));
            transform.Rotate(0, rotationSpeed, 0);
        }
    }
}
