using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform player;
    public float speed;
    public float rotSpeed;

	// Use this for initialization
	void Start () {

        speed = 10f;
        rotSpeed = 150f;

	}
	
	// Update is called once per frame
	void Update () {

        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotSpeed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        

	}
}
