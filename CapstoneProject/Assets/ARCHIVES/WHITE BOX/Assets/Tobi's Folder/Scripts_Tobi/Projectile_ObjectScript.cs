using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(ParticleSystem))]

public class Projectile_ObjectScript : MonoBehaviour {

	private Rigidbody _rb;
	private ParticleSystem _ps;

	private float _stunTime;
	private float _timeToDone;

	private float _projectileSpeed;
	private float _maxChargeTime;
	private float _deltaSize;
	private string _enemyTag;

	/// <summary>
	/// Returns length of time to be stunned
	/// </summary>
	public float stunTime
	{
		get { return _stunTime; }
	}

	// Use this for initialization
	void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_rb.useGravity = false;
	}

	public void StartCharge(float pProjectileSpeed, float pMaxChargeTime, float pDeltaSize, string pEnemyTag)
	{
		_projectileSpeed = pProjectileSpeed;
		_maxChargeTime = pMaxChargeTime;
		_deltaSize = pDeltaSize;
		_enemyTag = pEnemyTag;

		StartCoroutine(Charge());
	}

	public void Fire()
	{
		_rb.velocity = transform.forward * _projectileSpeed;
		StopAllCoroutines();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == _enemyTag)
		{
			Debug.Log("Stunned the enemy for " + _stunTime + " seconds.");
			Destroy(gameObject);
			//collision.gameObject.GetComponent<Enemy>().StunFunction();
		}
		else if (collision.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator Charge()
	{
		float deltaC = 0.1f;

		yield return new WaitForSeconds(deltaC);

		if(_timeToDone < _maxChargeTime)
		{
			_stunTime = _timeToDone += deltaC;

			Vector3 i = transform.localScale;
			
			i.x += (_deltaSize * deltaC);
			i.y += (_deltaSize * deltaC);
			i.z += (_deltaSize * deltaC);
			transform.localScale = i;
		
			StartCoroutine(Charge());
		}
		else
		{
			_timeToDone = 0.0f;
		}
	}
}
