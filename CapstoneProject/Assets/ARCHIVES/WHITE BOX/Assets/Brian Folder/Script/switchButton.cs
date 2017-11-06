using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
        void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<Projectile_ObjectScript>())
        {
            Debug.Log("Door Opened!");
        }
            
    }
   

}
