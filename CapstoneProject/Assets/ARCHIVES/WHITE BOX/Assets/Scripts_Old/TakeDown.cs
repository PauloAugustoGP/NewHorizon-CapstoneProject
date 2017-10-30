using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : Melee {

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == _enemyTag)
		{
			Debug.LogFormat("Hit Enemy. Stun for {0}", _stunTime);
		}
	}
}
