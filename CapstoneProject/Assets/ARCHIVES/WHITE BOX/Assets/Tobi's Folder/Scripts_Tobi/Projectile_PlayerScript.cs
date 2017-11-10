﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_PlayerScript : MonoBehaviour
{

	/*
	Instructions:
	1: Add this script onto the player.
	2: Add an empty gameobject onto the player that will the projectile spawn point. Drag and drop that gameobject into the Projectile Spawn field.
	3: Drag and drop the camera into the Cam Controller field.
	4: Place the prefab called Play_Projectile into the projectile prefab field. 
	5: Default Projectile Settings are fine. If you want to change, deltasize is the change in size, projectile speed is how fast the projectile flies
		max charge time is the maximum amount of time you can charge the projectile.
	6: Enemy string is the tag the enemy will have.
	7: Drag every componenet you want disabled into the componenets to disable array. These componenets will be deactivated while this skill is being used.
	6: Finished :)
	*/

	[System.Serializable]
	public class DragDrop
	{
		[SerializeField] private Transform _projectileSpawn;
		[SerializeField] private CameraController _CamController;
		[SerializeField] private GameObject _projectilePrefab;

		public Transform projectileSpawn
		{
			get { return _projectileSpawn; }
		}
		public CameraController camController
		{
			get { return _CamController; }
		}
		public GameObject projectilePrefab
		{
			get { return _projectilePrefab; } 
		}
	}

	[System.Serializable]
	public class ProjSettings
	{
		[SerializeField] protected float _deltaSize = 0.05f;
		[SerializeField] protected float _projectileSpeed = 10.0f;
		[SerializeField] protected float _maxChargeTime = 10.0f;

		public float deltaSize
		{
			get { return _deltaSize; }
		}
		public float projectileSpeed
		{
			get { return _projectileSpeed; }
		}
		public float maxChargeTime
		{
			get { return _maxChargeTime; }
		}
	}

	private Projectile_ObjectScript currentProjectile;

	private bool _projSpawned = false;

	[SerializeField] private bool _useCoolDown = true;

	private float _coolDownTime = 1.0f;

	/// <summary>
	/// Returns Cool Down Time (time until power can be used again)
	/// </summary>
	public float coolDownTime
	{
		get{ return _coolDownTime; }
	}

	[SerializeField] private string _enemyTag = "Enemy";

	[SerializeField] private DragDrop dragAndDropVariables = new DragDrop ();
	[SerializeField] private ProjSettings projectileSettings = new ProjSettings ();

	[SerializeField] private MonoBehaviour[] componentsToDisable;

	void Start()
	{
		_projSpawned = false;

		if (!dragAndDropVariables.projectilePrefab)
			Debug.LogError("No prefab for projectile");
		if (!dragAndDropVariables.projectileSpawn)
			Debug.LogError("No spawnpoint for projectile");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) /*&& Time.time > rechargeTime*/)
		{
			ProjSpawn();
		}

		if(Input.GetKeyUp(KeyCode.Space)/* && Time.time > rechargeTime*/)
		{
			ProjFire();
		}
	}

	private void ProjSpawn()
	{
		if(!_projSpawned)
		{
			if(_useCoolDown)
				StartCoroutine(TimeCounter());

			var projectile = (GameObject) Instantiate (dragAndDropVariables.projectilePrefab, 
				dragAndDropVariables.projectileSpawn.position, 
				dragAndDropVariables.projectileSpawn.rotation,
				dragAndDropVariables.projectileSpawn);
			currentProjectile = projectile.GetComponent<Projectile_ObjectScript>();

			_projSpawned = true;

			foreach(MonoBehaviour script in componentsToDisable)
			{
				if(componentsToDisable.Length > 0)
					script.enabled = false;
			}

			currentProjectile.StartCharge(projectileSettings.projectileSpeed, 
				projectileSettings.maxChargeTime, 
				projectileSettings.deltaSize, 
				_enemyTag, 
				dragAndDropVariables.camController);
		}
	}
		
	public void ProjFire()
	{
		if(currentProjectile)
		{
			currentProjectile.Fire();

			if(_useCoolDown)
			{
				if(_coolDownTime < 1.0f)
					_coolDownTime = 1.0f;

				StopAllCoroutines();
				StartCoroutine(CoolDown());
				//_coolDownTime = 0.0f;
			}
			else
			{
				_projSpawned = false;
			}
		}

		foreach(MonoBehaviour script in componentsToDisable)
		{
			if(componentsToDisable.Length > 0)
				script.enabled = true;
		}	

		currentProjectile = null;
	}

	private IEnumerator CoolDown()
	{
		yield return new WaitForSeconds(0.1f);
		if(_coolDownTime <= 0.0f)
		{
			_projSpawned = false;
		}
		else
		{
			_coolDownTime -= 0.1f;
			StartCoroutine(CoolDown());
		}
	}

	private IEnumerator TimeCounter()
	{
		_coolDownTime += 1.0f; 
		yield return new WaitForSeconds(1.0f);
		if(_coolDownTime < 10)
		{
			StartCoroutine(TimeCounter());
		}
	}
		
}
