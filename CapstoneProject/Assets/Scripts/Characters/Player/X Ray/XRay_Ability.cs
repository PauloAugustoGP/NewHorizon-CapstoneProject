using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay_Ability : MonoBehaviour
{
    [SerializeField] float cooldown;
    float currentTimeInCooldown = 0.0f;
    bool inCooldown = false;

    [SerializeField] List<MonoBehaviour> components;

    bool canXRay = false;

    bool inXRay = false;
	
	void Update ()
    {
        if(inCooldown)
        {
            RunCooldown();
        }

		if(canXRay)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                DisableComponents();
                inXRay = true;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                EnableComponents();
                inXRay = false;
                canXRay = false;
                inCooldown = true;
            }
        }
	}

    public bool GetIsInXRay() { return inXRay; }

    public bool EnableXRay(bool xray)
    {
        if(!inCooldown)
            canXRay = xray;

        return canXRay;
    }

    void DisableComponents()
    {
        foreach (MonoBehaviour comp in components)
            comp.enabled = false;
    }

    void EnableComponents()
    {
        foreach (MonoBehaviour comp in components)
            comp.enabled = true;
    }

    public int GetCooldownRatio()
    {
        return (int)((currentTimeInCooldown / cooldown) * 100);
    }

    void RunCooldown()
    {
        currentTimeInCooldown += Time.deltaTime;

        if(currentTimeInCooldown >= cooldown)
        {
            currentTimeInCooldown = 0.0f;
            inCooldown = false;
        }
    }
}
