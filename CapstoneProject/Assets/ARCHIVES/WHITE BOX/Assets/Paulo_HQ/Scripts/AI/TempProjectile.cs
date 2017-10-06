using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProjectile : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] [Range(100, 5000)] float speed;
    [SerializeField] float damage;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed * Time.deltaTime;

        Destroy(gameObject, 10);
	}

    void OnTriggerEnter(Collider c)
    {
        /*if (c.gameObject.tag == "Player")
            c.gameObject.GetComponent<charControlScript>().ApplyDamage();*/

        Destroy(gameObject);
    }
}
