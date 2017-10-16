using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Projectile_PlayerScript : MonoBehaviour
{
	[System.Serializable]
	public class DragDrop
	{
		[SerializeField]
		private Transform _projectileSpawn;
		[SerializeField]
		private GameObject _projectilePrefab;

		public Transform projectileSpawn
		{
			get { return _projectileSpawn; }
		}
		public GameObject projectilePrefab
		{
			get { return _projectilePrefab; } 
		}
	}

	[System.Serializable]
	public class ProjSettings
	{
		[SerializeField]
		protected float _deltaSize = 0.05f;
		[SerializeField]
		protected float _projectileSpeed = 10.0f;
		[SerializeField]
		protected float _maxChargeTime = 10.0f;

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

	private bool projSpawned;
	private float rechargeTime;

	[SerializeField]
	private DragDrop dragAndDropVariables = new DragDrop ();
	[SerializeField]
	private ProjSettings projectileSettings = new ProjSettings ();

	void Start()
	{
		projSpawned = false;

		if (!dragAndDropVariables.projectilePrefab)
			Debug.LogError("No prefab for projectile");
		if (!dragAndDropVariables.projectileSpawn)
			Debug.LogError("No spawnpoint for projectile");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ProjSpawn();
		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			ProjFire();
		}
	}

	private void ProjSpawn()
	{
		if(!projSpawned)
		{
			var projectile = (GameObject) Instantiate (dragAndDropVariables.projectilePrefab, 
				dragAndDropVariables.projectileSpawn.position, 
				dragAndDropVariables.projectileSpawn.rotation);
			currentProjectile = projectile.GetComponent<Projectile_ObjectScript>();

			projSpawned = true;

			currentProjectile.StartCharge(projectileSettings.projectileSpeed, projectileSettings.maxChargeTime, projectileSettings.deltaSize);
		}
	}
		
	public void ProjFire()
	{
		if(currentProjectile)
			currentProjectile.Fire();
		

		projSpawned = false;
		currentProjectile = null;
	}
		
}
