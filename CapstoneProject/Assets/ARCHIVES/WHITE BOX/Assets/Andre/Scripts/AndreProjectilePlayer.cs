using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndreProjectilePlayer : MonoBehaviour
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

    private bool _projSpawned = false;

    [SerializeField]
    private bool _useCoolDown = true;

    private float _coolDownTime;

    /// <summary>
    /// Returns Cool Down Time (time until power can be used again)
    /// </summary>
    public float coolDownTime
    {
        get { return _coolDownTime; }
    }

    [SerializeField]
    private string _enemyTag = "Enemy";

    [SerializeField]
    private MonoBehaviour[] componentsToDisable;



    [SerializeField]
    private DragDrop dragAndDropVariables = new DragDrop();
    [SerializeField]
    private ProjSettings projectileSettings = new ProjSettings();

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

        if (Input.GetKeyUp(KeyCode.Space)/* && Time.time > rechargeTime*/)
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

            var projectile = (GameObject)Instantiate(dragAndDropVariables.projectilePrefab,
                dragAndDropVariables.projectileSpawn.position,
                dragAndDropVariables.projectileSpawn.rotation);
            currentProjectile = projectile.GetComponent<Projectile_ObjectScript>();

            _projSpawned = true;

            foreach (MonoBehaviour script in componentsToDisable)
            {
                if (componentsToDisable.Length > 0)
                    script.enabled = false;
            }

            currentProjectile.StartCharge(projectileSettings.projectileSpeed, projectileSettings.maxChargeTime, projectileSettings.deltaSize, _enemyTag, dragAndDropVariables.projectileSpawn);
        }
    }

    public void ProjFire()
    {
        if (currentProjectile)
        {
            currentProjectile.Fire();

            foreach (MonoBehaviour script in componentsToDisable)
            {
                if (componentsToDisable.Length > 0)
                    script.enabled = true;
            }

            if (_useCoolDown)
            {
                StopAllCoroutines();
                StartCoroutine(CoolDown());
                //_coolDownTime = 0.0f;
            }
            else
            {
                _projSpawned = false;
            }
        }

        currentProjectile = null;
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1.0f);
        if (_coolDownTime <= 0.0f)
        {
            _projSpawned = false;
        }
        else
        {
            _coolDownTime -= 1.0f;
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator TimeCounter()
    {
        _coolDownTime += 1.0f;
        yield return new WaitForSeconds(1.0f);
        if (_coolDownTime < 10)
        {
            StartCoroutine(TimeCounter());
        }
    }

}
