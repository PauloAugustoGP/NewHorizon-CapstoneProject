using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTeleportScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        // currentPosition = transform.position;
        // playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // setting up new variable raycasts hit for raycast to return its hit point to
        RaycastHit hit;

        // setting up new raycast to point from camera to a certain point (mouse position)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if the raycast returns a value to its hit variable
        if (Physics.Raycast(ray, out hit))
        {
            //if (hit.transform.gameObject.layer == 9)
            //{
                transform.position = hit.point;
            //}
        }
        else Debug.Log("Something very weird happened!");

        Debug.DrawRay(ray.origin, ray.direction, Color.green);
    }
}
