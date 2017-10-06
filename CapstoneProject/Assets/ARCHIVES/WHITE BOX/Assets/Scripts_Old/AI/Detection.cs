using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField]AIState states;

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            states.SetTarget(c.transform);
            states.SetState(2);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            states.SetTarget(null);
            states.SetState(1);
        }
    }
}
