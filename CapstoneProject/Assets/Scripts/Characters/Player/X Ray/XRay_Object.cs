using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_Object : MonoBehaviour
{
    [SerializeField] Shader defaultShader;
    [SerializeField] Shader xrayShader;

    bool hasTarget = false;
	
	void Update ()
    {
		if(hasTarget)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ChangeShader();
            }

            if(Input.GetKeyUp(KeyCode.E))
            {
                ResetShader();
            }
        }
	}

    void ChangeShader()
    {
        GetComponent<Renderer>().material.shader = xrayShader;
    }

    void ResetShader()
    {
        GetComponent<Renderer>().material.shader = defaultShader;
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            
            hasTarget = c.gameObject.GetComponent<XRay_Ability>().EnableXRay(true);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            hasTarget = false;
        }
    }

}
