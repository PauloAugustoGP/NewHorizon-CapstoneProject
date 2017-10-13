using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Projectile_ObjectScript : MonoBehaviour {

	private Rigidbody rb;
	private float deltaStunTime;
	private float stunTime;
	private float timeToDone;

	private float projectileSpeed;
	private float maxChargeTime;
	private float deltaSize;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		rb.useGravity = false;
		deltaStunTime = 1.0f;

		timeToDone = 0.0f;
	}

	public void StartCharge(float _projectileSpeed, float _maxChargeTime, float _deltaSize)
	{
		projectileSpeed = _projectileSpeed;
		maxChargeTime = _maxChargeTime;
		deltaSize = _deltaSize;

		StartCoroutine(Charge());
	}

	public void Fire()
	{
		rb.velocity = transform.forward * projectileSpeed;
		StopAllCoroutines();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator Charge()
	{
		float deltaC = 0.1f;

		yield return new WaitForSeconds(deltaC);

		if(timeToDone < maxChargeTime)
		{
			timeToDone += deltaC;

			Vector3 i = transform.localScale;
			i.x += (deltaSize * deltaC);
			i.y += (deltaSize * deltaC);
			i.z += (deltaSize * deltaC);
			transform.localScale = i;
		
			StartCoroutine(Charge());
		}
		else
		{
			timeToDone = 0.0f;
		}
	}
}
