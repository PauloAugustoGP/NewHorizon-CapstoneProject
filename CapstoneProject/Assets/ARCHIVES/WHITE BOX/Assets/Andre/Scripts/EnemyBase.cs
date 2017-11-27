using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Wandering,
    Chase,
    Attack,
    Dead
}


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class EnemyBase : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private Vector3 ranDest;

    [SerializeField]
    private EnemyState enemyState = EnemyState.Wandering;

    public float wanderRadius = 5.0f;
    public float attackRange = 2.0f;
    public float lookAngle = 10.0f;

    public float pickupOffset = 0.0f;

    public List<GameObject> objectDrop;
    public int depth;
    public float EnemyMaxHealth = 100;
    public float EnemyHealth = 100;
    public int moreDamage;

    private GameObject target;
    [SerializeField]
    private int PlayerAttackPower = 10;
    [SerializeField]
    private float attackSpeed = 3.0f;
    private float attackTimer;

    private Transform[] patrolPaths; //array to store where we can walk to for patrolling 
    public int currentNode;

    public SpawnRandom OwningSpawner;
    public ParticleSystem HitBlood;


    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ranDest = transform.position;
        attackTimer = attackSpeed;
    }

    private void Start()
    {
        EnemyPaths ep = FindObjectOfType<EnemyPaths>();
        if (ep)
        {
            //Debug.Log("Enemy Paths Found!");
            patrolPaths = ep.GetPaths();
            // Debug.Log(patrolPaths.Length);
            //Debug.Log(currentNode);
        }
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        switch (enemyState)
        {
            case EnemyState.Wandering:
                {
                    //check if we have reached our desitnation
                    if ((ranDest - transform.position).magnitude < 2.0f)
                    {
                        //Debug.Log("At Target Node");
                        //Pick a random path between the next 3 in the array
                        currentNode = (currentNode + Random.Range(1, 3)) % (patrolPaths.Length - 1);
                        //Go to next node in array
                        //currentNode = (currentNode + 1);
                        // NavMeshPath path = new NavMeshPath();
                        //do
                        //{
                        //***For wandering to random locations***///
                        //if we have reached our destination then pick a new  random position to move to
                        //ranDest = new Vector3(transform.position.x + Random.Range(-wanderRadius, wanderRadius), transform.position.y, transform.position.z + Random.Range(-wanderRadius, wanderRadius));
                        //***For wandering to random locations***///
                        ranDest = patrolPaths[currentNode].position;
                        navAgent.SetDestination(ranDest);
                        //} while (!navAgent.CalculatePath(ranDest, path));
                    }
                    else
                    {
                        navAgent.SetDestination(ranDest);
                    }

                    //otherwise do nothing (let the enemy keep moving)
                }
                break;

            //we have found an enemy!
            case EnemyState.Chase:
                {
                    //as long as target isn't dead
                    if (target)
                    {
                        //if close enough to target then attack!
                        if ((target.transform.position - transform.position).magnitude < attackRange)
                        {
                            enemyState = EnemyState.Attack;
                        }
                        else
                        {
                            //set destination towards our enemy!
                            navAgent.SetDestination(target.transform.position);
                        }
                    }
                }
                break;

            case EnemyState.Attack:
                {
                    if ((target.transform.position - transform.position).magnitude < attackRange)
                    {

                        //if we are in range
                        if (target && attackTimer <= 0)
                        {
                            //attack
                            Animator anim = GetComponent<Animator>();
                            if (anim)
                            {
                                anim.SetBool("Attack", true);
                            }
                            target.GetComponent<AndreHUD>().TakeDamage();

                            //reset attack timer
                            attackTimer = attackSpeed;
                        }
                    }
                    else
                    {
                        Animator anim = GetComponent<Animator>();
                        if (anim)
                        {
                            anim.SetBool("Attack", false);
                        }
                        enemyState = EnemyState.Chase;
                    }
                }
                break;
            case EnemyState.Dead:
                {
                    navAgent.Stop();
                }
                break;
        }

    }

    protected virtual void OnTriggerEnter(Collider c)
    {
        //can also check the object type (i.e. get component<>())
        if (c.tag == "Player")
        {
            //set our target to the player
            target = c.gameObject;
            //switch the enemy state to chasing 
            enemyState = EnemyState.Chase;

        }
    }

    protected virtual void OnTriggerExit(Collider c)
    {
        //check if player is outside our radius (optional) 
        if (c.tag == "Player")
        {
            //change state back to wandering 
            enemyState = EnemyState.Wandering;

            //clear enemy target
            target = null;
        }
    }

    public virtual void TakeDamage(int proAttackPower)
    {
        ParticleSystem ps = Instantiate(HitBlood, transform.position, Quaternion.identity);
        ps.Play();
        GameObject RanDrop = objectDrop[Random.Range(0, objectDrop.Count)];
        if (EnemyHealth <= 0)
        {
            enemyState = EnemyState.Dead;
            Animator anim = GetComponent<Animator>();
            if (anim)
            {
                anim.SetBool("Dead", true);
            }
            if (RanDrop)
            {
                Instantiate(RanDrop, gameObject.transform.position + Vector3.up * pickupOffset, Quaternion.identity);
            }
            //tell owning spawner we died 
            if (OwningSpawner)
            {
                OwningSpawner.EnemyDied();
            }
            //GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<PlayerUI>().hideEnemyBar();
            //do animation before destroy
            Invoke("Kill", 3f);
        }
        else
        {
            EnemyHealth -= proAttackPower + moreDamage;

            Debug.Log("Enemy Health: " + EnemyHealth);
        }
        //*****The player script should update the UI in the TakeDamage function
        //***The player should have a reference to the UI associated with it (i.e. player health bars) 
        GameObject camera = GameObject.FindGameObjectWithTag("PlayerCamera");

        if (camera)
        {
            TextUI player = camera.GetComponent<TextUI>();
            if (player)
            {
               // player.ShowEnemyBar(EnemyHealth / EnemyMaxHealth);
            }
            else { Debug.Log("Player UI not found"); }
        }
        else
        { Debug.Log("Camera Not found"); }

        //GameObject.Find("Camera").GetComponent<PlayerUI>().ShowEnemyBar(EnemyHealth / EnemyMaxHealth);
    }
    public void Kill()
    {
        Destroy(gameObject);
    }

}