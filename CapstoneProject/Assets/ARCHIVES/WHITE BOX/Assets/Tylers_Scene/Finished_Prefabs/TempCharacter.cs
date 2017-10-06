using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharacter : MonoBehaviour
{
    public Transform projectileSpawn;
    public GameObject projectilePrefab;

	public Projectile currentProjectile;

	bool projSpawned;
	public float rechargeTime;
   
    void Start()
    {
		projectilePrefab = Resources.Load("StunProjectile") as GameObject;
		projectileSpawn = transform.Find("RayOrigin").gameObject.transform;
		projSpawned = false;

        if (!projectilePrefab)
            Debug.LogError("No prefab for projectile");
        if (!projectileSpawn)
            Debug.LogError("No spawnpoint for projectile");
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Space) /*&& Time.time > rechargeTime*/)
		{
			if(!projSpawned)
			{
				var projectile = (GameObject) Instantiate (projectilePrefab, 
					projectileSpawn.position, 
					projectileSpawn.rotation);
				currentProjectile = projectile.GetComponent<Projectile>();

				projSpawned = true;
			}

			currentProjectile.Charge();
			//rechargeTime += (1.0f * Time.deltaTime);
            
			//SoundManger.instance.SoundCaller("Fire");
			//Debug.Log("You pressed fire");
        }

		if(Input.GetKeyUp(KeyCode.Space)/* && Time.time > rechargeTime*/)
		{
			//rechargeTime += Time.time;
			currentProjectile.Fire();
			projSpawned = false;
			currentProjectile = null;
		}
    }
}
