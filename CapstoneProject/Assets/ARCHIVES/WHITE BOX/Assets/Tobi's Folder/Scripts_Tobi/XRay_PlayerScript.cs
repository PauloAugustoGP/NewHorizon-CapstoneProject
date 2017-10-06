using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_PlayerScript : MonoBehaviour {

	static private string XRay_Tag = "XRay";

	private Transform rayOrigin;

	private bool isTouched;
	private bool xrayActive;

	private Renderer currentObject;
	private Shader currentShader;
	private Shader transparent;

	// Use this for initialization
	void Start () 
	{
		rayOrigin = transform.Find("RayOrigin").gameObject.transform;
		currentObject = null;

		transparent = Shader.Find("Transparent");

		isTouched = false;
		xrayActive = false;
	}

	// Update is called once per frame
	void Update ()
	{
		Ray ray = new Ray (rayOrigin.position, transform.forward);
		Debug.DrawRay (ray.origin, ray.direction, Color.black);

		RaycastHit hit;
		if (Input.GetKey (KeyCode.E) && Physics.Raycast (ray, out hit, 0.5f)) 
		{
			if (hit.collider.tag == XRay_Tag) {
				currentObject = hit.collider.GetComponent<Renderer> ();
				if(!isTouched)
				{
					currentShader = currentObject.material.shader;
					isTouched = true;
				}
				currentObject.material.shader = transparent;
				xrayActive = true;
			}
		} 
		else
		{
			xrayActive = false;
		}

		if (!xrayActive && currentObject) 
		{
			currentObject.material.shader = currentShader;
			isTouched = false;
			currentObject = null;
			currentShader = null;
		}
	}
}