using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
       /* if (other.GetComponent<charControlScript>())
        {
            //charControlScript c = other.GetComponent<charControlScript>();
            c.PlayerHealth += 3;
            Destroy(gameObject);
        }
        */
    }
}
