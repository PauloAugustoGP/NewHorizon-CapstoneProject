using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brianProjectile : MonoBehaviour
{

    public Rigidbody projectile;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pew Pew");

            if (projectile)
            {
                Rigidbody rb = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                rb.AddForce(projectileSpawnPoint.forward * projectileSpeed, ForceMode.Impulse);
            }
            else
                Debug.Log("No projectile prefab found.");
        }
    }
}
