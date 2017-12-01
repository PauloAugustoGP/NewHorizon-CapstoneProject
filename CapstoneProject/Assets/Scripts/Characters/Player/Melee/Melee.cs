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
    [SerializeField] private CharacterBehaviour _CB; // CharacterBehaviour Script from player
    [SerializeField] private TeleportScript _TS; // TeleportScript from player



    private WaitForSeconds _meleeWait = new WaitForSeconds(0.1f);
    private WaitForSeconds _zeroWait = new WaitForSeconds(0.0f);


    void Start()
	{
		/*if(!_meleeHitBox)
			_meleeHitBox = null;
		else*/
			_meleeHitBox.SetActive(false);

		_meleeHitBox.GetComponent<TakeDown>().SetUp(_enemyTag, _stunTime);

        _CB.GetComponent<CharacterBehaviour>();

        _TS.GetComponent<TeleportScript>();
	}

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
			StartCoroutine(Attack());
		}
    }

	private IEnumerator Attack()
	{
        if (_CB.IsCrouching && !_TS.isActive)
        {

            _meleeHitBox.SetActive(true);
            yield return _meleeWait;
            _meleeHitBox.SetActive(false);
        }
        /*else
            yield return _zeroWait;*/
	}
}
