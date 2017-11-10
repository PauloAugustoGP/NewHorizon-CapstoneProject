using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : MonoBehaviour {

	//this projectile should be on the hitbox gameobject

	private string _enemyTag;
	private float _stunTime;

	public void SetUp(string pEnemyTag, float pStunTime)
	{
		_enemyTag = pEnemyTag;
		_stunTime = pStunTime;
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == _enemyTag)
		{
			Debug.LogFormat("Hit Enemy. Stun for {0}", _stunTime);
		}
	}
}
