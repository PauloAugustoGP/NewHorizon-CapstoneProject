using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	[Header("Definitions")]
	[SerializeField] protected string _enemyTag = "Enemy";
	[SerializeField] protected float _stunTime = 5.0f;

	[Header("Drag and Drop Variables")]
	[SerializeField] protected GameObject _meleeHitBox;

	void Start()
	{
		if(!_meleeHitBox)
			Debug.LogError("Error: No Hit Box gameobject attached");
		else
			_meleeHitBox.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.M))
		{
			Attack();
		}
    }

	private IEnumerator Attack()
	{
		_meleeHitBox.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		_meleeHitBox.SetActive(false);
	}
}
