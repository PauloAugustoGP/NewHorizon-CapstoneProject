using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

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
	}

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0))
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
