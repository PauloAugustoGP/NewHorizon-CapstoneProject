using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateTo : MonoBehaviour
{
    public GameObject target;
    
    void Update()
    {
        if (!target)
        {
            return;
        }
        var theCamOrPlayer= transform.position;
        var theObject = target.transform.position;
        transform.rotation = Quaternion.LookRotation(theCamOrPlayer - theObject, Vector3.up);
    }
}