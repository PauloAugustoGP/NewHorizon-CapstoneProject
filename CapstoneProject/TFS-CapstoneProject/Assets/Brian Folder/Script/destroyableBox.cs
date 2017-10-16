using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyableBox : MonoBehaviour
{

    public int health;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (health <= 0)
        {
            health = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {

            health--;

            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
