using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    //this script should be on the projectile prefab

    private Rigidbody _rb;
    private Collider _col;
    private ParticleVisual _pvs;
    private CameraController _cam;

    private float _stunTime = 1.0f;
    private float _timeToDone;

    private bool _charging;

    private float _projectileSpeed;
    private float _maxChargeTime;
    private float _deltaSize;
    private string _enemyTag;
    private Vector3 _projectileRayOrigin;
    private LayerMask _ignorePlayer;
    /// <summary>
    /// Returns length of time to be stunned
    /// </summary>
    public float stunTime
    {
        get { return _stunTime; }
    }

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        _pvs = GetComponentInChildren<ParticleVisual>();
        _rb.useGravity = false;
        _col.enabled = false;
        _rb.isKinematic = true;
    }

    public void StartCharge(float pProjectileSpeed, float pMaxChargeTime, float pDeltaSize, string pEnemyTag, CameraController pCam, Vector3 pProjectileRayOrigin, LayerMask pIgnorePlayer)
    {
        _projectileSpeed = pProjectileSpeed;
        _maxChargeTime = pMaxChargeTime;
        _deltaSize = pDeltaSize;
        _enemyTag = pEnemyTag;
        _cam = pCam;
        _projectileRayOrigin = pProjectileRayOrigin;
        _ignorePlayer = pIgnorePlayer;

        _charging = true;

        StartCoroutine(Charge());
    }

    public void Fire()
    {
        Destroy(gameObject, 20.0f);
        _charging = false;

        Vector3 direction = Vector3.zero;
        direction = _cam.GetCentreView(_projectileRayOrigin, _ignorePlayer) - transform.position;

        _rb.isKinematic = false;

        _rb.velocity = direction.normalized * _projectileSpeed;
        _col.enabled = true;
        transform.parent = null; ;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider c)
    {
        //Debug.Log(c.ClosestP

        if (c.gameObject.tag == _enemyTag)
        {
            Debug.Log("Stunned the enemy for " + _stunTime + " seconds.");
            c.GetComponent<Enemy>().SetIsStunned(true);
            Destroy(gameObject);
            //collision.gameObject.GetComponent<Enemy>().StunFunction();
        }
        else if (c.gameObject.GetComponent<Collider>().isTrigger)
        {
            Debug.Log("Ignore");
            return;
        }
        else if (c.gameObject.tag != "Player")
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }

    private IEnumerator Charge()
    {
        float deltaC = 0.1f;

        yield return new WaitForSeconds(deltaC);

        if (_timeToDone < _maxChargeTime)
        {
            _stunTime += deltaC;
            _timeToDone += deltaC;
            _pvs.UpdateSize(deltaC);
            StartCoroutine(Charge());
        }
        else
        {
            _timeToDone = 0.0f;
        }
    }
}
