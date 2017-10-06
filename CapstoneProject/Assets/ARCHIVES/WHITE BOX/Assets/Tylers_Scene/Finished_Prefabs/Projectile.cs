using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Projectile : MonoBehaviour
{
    
    private Rigidbody rb;
    public float projectileSpeed;
	public float deltaStunTime;
	public float deltaSize;
	public float stunTime;
	public float timeToDone;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectileSpeed = 10.0f;

        rb.useGravity = false;
        //rb.velocity = transform.forward * projectileSpeed;
		deltaStunTime = 1.0f;
		deltaSize = 0.05f;

		timeToDone = Time.time + 10.0f;
    }

    // Update is called once per frame
    public void Charge()
    {
		if(timeToDone > Time.time)
		{
			stunTime += (deltaStunTime * Time.deltaTime);
			Vector3 i = transform.localScale;
			i.x += (deltaSize * Time.deltaTime);
			i.y += (deltaSize * Time.deltaTime);
			i.z += (deltaSize * Time.deltaTime);
			transform.localScale = i;
		}
    }

	public void Fire()
	{
		rb.velocity = transform.forward * projectileSpeed;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
			Destroy(gameObject);
        }
    }
}
