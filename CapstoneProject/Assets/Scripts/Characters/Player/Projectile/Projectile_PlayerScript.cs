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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_PlayerScript : MonoBehaviour
{
    [Header("Definitions")]
    [SerializeField]
    protected float _deltaSize = 0.05f;
    [SerializeField] protected float _projectileSpeed = 10.0f;
    [SerializeField] protected float _maxChargeTime = 10.0f;
    [SerializeField] private bool _useCoolDown = true;
    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private LayerMask _ignorePlayer;

    [Header("Drag and Drop")]
    [SerializeField]
    private Transform _projectileSpawn;
    [SerializeField] private CameraController _camController;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileRayOrigin;

    [Header("Disable")]
    [SerializeField]
    private MonoBehaviour[] componentsToDisable;

    private WaitForSeconds _timeCountWait = new WaitForSeconds(1.0f);
    private Projectile_ObjectScript _currentProjectile;

    private bool _projSpawned = false;
    private float _coolDownTime = 1.0f;

    /// <summary>
    /// Returns Cool Down Time (time until power can be used again)
    /// </summary>
    public float coolDownTime
    {
        get { return _coolDownTime; }
    }

    void Start()
    {
        _projSpawned = false;

        if (!_projectilePrefab)
            Debug.LogError("No prefab for projectile");
        if (!_projectileSpawn)
            Debug.LogError("No spawnpoint for projectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) /*&& Time.time > rechargeTime*/)
        {
            ProjSpawn();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) /* && Time.time > rechargeTime*/)
        {
            ProjFire();
        }
    }

    private void ProjSpawn()
    {
        if (!_projSpawned)
        {
            if (_useCoolDown)
                StartCoroutine(TimeCounter());

            var projectile = (GameObject)Instantiate(_projectilePrefab,
                _projectileSpawn.position,
                _projectileSpawn.rotation,
                _projectileSpawn);

            _currentProjectile = projectile.GetComponent<Projectile_ObjectScript>();

            _projSpawned = true;

            foreach (MonoBehaviour script in componentsToDisable)
            {
                if (componentsToDisable.Length > 0)
                    script.enabled = false;
            }

            _currentProjectile.StartCharge(_projectileSpeed,
                _maxChargeTime,
                _deltaSize,
                _enemyTag,
                _camController,
                _projectileRayOrigin.position,
                _ignorePlayer);
        }
    }

    public void ProjFire()
    {
        if (_currentProjectile)
        {
            _currentProjectile.Fire();

            if (_useCoolDown)
            {
                if (_coolDownTime < 1.0f)
                    _coolDownTime = 1.0f;

                StopAllCoroutines();
                StartCoroutine(CoolDown());
            }
            else
            {
                _projSpawned = false;
            }
        }

        foreach (MonoBehaviour script in componentsToDisable)
        {
            if (componentsToDisable.Length > 0)
                script.enabled = true;
        }

        _currentProjectile = null;
    }

    private void SetComponents(bool pState)
    {
        if (componentsToDisable.Length <= 0)
            return;

        foreach (MonoBehaviour script in componentsToDisable)
        {
            script.enabled = pState;
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDownTime);

        _coolDownTime = 0.0f;
        _projSpawned = false;
    }

    private IEnumerator TimeCounter()
    {
        _coolDownTime += 1.0f;
        yield return _timeCountWait;
        if (_coolDownTime < 10)
        {
            StartCoroutine(TimeCounter());
        }
    }

}
