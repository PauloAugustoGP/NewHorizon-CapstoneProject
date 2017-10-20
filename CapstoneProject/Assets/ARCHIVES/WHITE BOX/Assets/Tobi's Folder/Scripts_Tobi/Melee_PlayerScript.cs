using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_PlayerScript : MonoBehaviour {

	[SerializeField]
	private float _stunTime = 15.0f;
	[SerializeField]
	private string _enemyTag = "Enemy";

	private bool _attacking = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			MeleeStun();
		}
	}

	private void MeleeStun()
	{
		_attacking = true;
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == _enemyTag && _attacking)
		{
			Debug.Log("Hit and stun enemy for " + _stunTime + " seconds.");
		}
	}
}
