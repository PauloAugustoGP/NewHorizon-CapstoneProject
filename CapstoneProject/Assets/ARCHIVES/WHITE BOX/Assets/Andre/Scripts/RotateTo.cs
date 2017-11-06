using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateTo : MonoBehaviour
{
    public GameObject target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            return;
        }
        var me = transform.position;
        var him = target.transform.position;
        transform.rotation = Quaternion.LookRotation(me - him, Vector3.up);
    }
}