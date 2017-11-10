using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	/*
	Instructions:
	1: Add this script onto the player.
	2: Enemy tag is the string that an enemy would be tagged in.
	3: Stun time is how long a hit enemy will be stunned. Default time is 5 seconds.
	4: Add an empty gameobject to the player. Add a collider that will be your hitbox. Adjest the hitbox size to be however big you want. 
		Set is trigger to true. Add the TakeDown script to the hitbox gameobject. Drag and drop that gameobject into the melee hit box field. 
	5: Finished :)
	*/

	[Header("Definitions")]
	[SerializeField] protected string _enemyTag = "Enemy";
	[SerializeField] protected float _stunTime = 5.0f;

	[Header("Drag and Drop Variables")]
	[SerializeField] private GameObject _meleeHitBox;

	void Start()
	{
		if(!_meleeHitBox)
			_meleeHitBox = null;
		else
			_meleeHitBox.SetActive(false);

		_meleeHitBox.GetComponent<TakeDown>().SetUp(_enemyTag, _stunTime);
	}

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.M))
		{
			StartCoroutine(Attack());
		}
    }

	private IEnumerator Attack()
	{
		_meleeHitBox.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		_meleeHitBox.SetActive(false);
	}
}
