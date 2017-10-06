using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    [SerializeField] bool DebugMode;
    //Rigidbody rb;

    NavMeshAgent agent;

    Transform target;

    bool isChasing;

    [Header("General Variables")]
    [SerializeField] GameObject eye;
    
    [SerializeField] float lineOfSight;
    [SerializeField] int angleConeOfSight;

    [SerializeField] [Range(0, 20)] float turnRate;

    enum StatesIndex
    {
        Idle,
        Walk,
        Chase,
        Attack,
        Search,
        Stun,
        Dead
    }
    [SerializeField]StatesIndex currentState;

    //IDLE VARIABLES
    [Header("Idle State Variables")]
    [SerializeField] float waitTimeInIdle;
    float currentTimeInIdle;

    // WALK VARIABLES
    [Header("Walk State Variables")]
    [SerializeField] List<Transform> pathMilestone;
    int currentMilestone;
    [SerializeField] bool isCycle;
    int direction;
    Vector3 goal;
    [SerializeField] [Range(0, 15)] float speedWalk;

    // CHASE VARIABLES
    [Header("Chase State Variables")]
    [SerializeField] float attackRange;
    [SerializeField] [Range(0, 15)] float speedChase;
    [SerializeField] float timeChasing;
    float timeInChase;

    // ATTACK VARIABLES
    [Header("Attack State Variables")]
    [SerializeField] bool isMelee;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] TempProjectile bullet; // REPLACE FOR A PARTICLE SYSTEM
    [SerializeField] float cooldownFire;
    float timeInAttack;
    [SerializeField] int ammo;
    int currentAmmo;
    [SerializeField] float reloadTime;
    float timeInReload;
    [SerializeField] bool findForCover;
    List<GameObject> cover;
    bool foundCover;

    // SEARCH STATE
    [Header("Search State Variables")]
    [SerializeField] [Range(0, 15)] float speedSearch;
    [SerializeField] float searchRange;
    [SerializeField] float timeSearching;
    float timeInSearch;

    // STUN STATE
    [Header("Stun State Variables")]
    [SerializeField] float timeLookAround;
    float timeInLookAround;
    bool isStunned;
    [SerializeField] float timeStunned;
    float timeInStunned;

    // DEAD STATE
    [Header("Dead State Variables")]
    [SerializeField] float timeDead;
    float timeInDead;


    void Start ()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("Player").transform;

        isChasing = false;

        currentState = StatesIndex.Idle;

        currentTimeInIdle = 0.0f;

        currentMilestone = 0;
        direction = 1;

        timeInChase = 0.0f;

        timeInAttack = 0.0f;
        currentAmmo = ammo;
        timeInReload = 0.0f;

        cover = new List<GameObject>();
        foundCover = false;

        timeInSearch = 0.0f;
        
    }
	
	void Update ()
    {
        /*if(!isChasing && !isStunned)
            CheckTargetDetection();*/

        switch(currentState)
        {
            #region IDLE STATE

            case StatesIndex.Idle:
                {
                    // play idle animation

                    agent.speed = 0.0f;

                    CheckTargetDetection();

                    currentTimeInIdle += Time.deltaTime;
                    if(currentTimeInIdle >= waitTimeInIdle)
                    {
                        currentTimeInIdle = 0.0f;

                        goal = pathMilestone[currentMilestone].position;

                        currentState = StatesIndex.Walk;
                    }
                }break;

            #endregion

            #region WALK STATE

            case StatesIndex.Walk:
                {
                    // play walk animation

                    agent.destination = goal;
                    agent.speed = speedWalk;
                    agent.stoppingDistance = 0.1f;

                    CheckTargetDetection();

                    if (Vector3.Distance(transform.position, goal) <= 0.1f)
                    {
                        if (isCycle)
                        {
                            currentMilestone++;
                            if (currentMilestone == pathMilestone.Count) currentMilestone = 0;
                        }
                        else
                        {
                            currentMilestone += direction;
                            if (currentMilestone == pathMilestone.Count)
                            {
                                currentMilestone--;
                                direction = -1;
                            }
                            if (currentMilestone == -1)
                            {
                                currentMilestone++;
                                direction = 1;
                            }
                        }

                        currentState = StatesIndex.Idle;
                    }
                }break;

            #endregion

            #region CHASE STATE

            case StatesIndex.Chase:
                {
                    // play chase animation

                    agent.destination = target.position;
                    agent.speed = speedChase;
                    agent.stoppingDistance = attackRange;
                    
                    if (Vector3.Distance(transform.position, target.position) <= attackRange)
                    {
                        if (LineCastToTarget())
                        {
                            isChasing = true;
                            timeInChase = 0.0f;
                            currentState = StatesIndex.Attack;
                        }
                    }

                    timeInChase += Time.deltaTime;
                    if(timeInChase >= timeChasing)
                    {
                        currentState = StatesIndex.Search;
                        goal = target.position;
                        timeInChase = 0.0f;
                    }
                }
                break;

            #endregion

            #region ATTACK STATE

            case StatesIndex.Attack:
                {
                    if (isMelee)
                    {
                        //play animation to melee attack
                    }
                    else
                    {
                        if (findForCover && !foundCover)
                        {
                            Collider[] potentialCover = Physics.OverlapSphere(transform.position, Vector3.Distance(transform.position, target.position));

                            for (int i = 0; i < potentialCover.Length; i++)
                                if (potentialCover[i].gameObject.name == "CoverObject") // CHANGE TO TAG
                                    if (Vector3.Distance(potentialCover[i].transform.position, target.position) <= attackRange)
                                        cover.Add(potentialCover[i].gameObject);


                            if (cover.Count > 0 && !foundCover)
                            {
                                int coverIndex = Random.Range(0, cover.Count);

                                Transform[] coverPositions = cover[coverIndex].GetComponentsInChildren<Transform>();
                                for (int i = 0; i < coverPositions.Length; i++)
                                {
                                    if (!LineCastToTarget(coverPositions[i]))
                                    {
                                        agent.destination = coverPositions[i].position;
                                        agent.speed = speedWalk;
                                        agent.stoppingDistance = 0.05f;
                                        foundCover = true;
                                        break;
                                    }
                                }
                            }

                            if (Vector3.Distance(transform.position, agent.destination) <= 0.05f && foundCover)
                            {
                                agent.speed = 0.0f;
                                agent.stoppingDistance = 0.0f;
                            }
                        }

                        Vector3 direction = (target.position - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnRate * Time.deltaTime);

                        if (currentAmmo > 0)
                        {
                            timeInAttack += Time.deltaTime;
                            if (timeInAttack >= cooldownFire)
                            {
                                timeInAttack = 0.0f;
                                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                                currentAmmo--;
                            }
                        }
                        else
                        {
                            timeInReload += Time.deltaTime;
                            if (timeInReload >= reloadTime)
                            {
                                timeInReload = 0.0f;
                                currentAmmo = ammo;
                            }
                        }
                    }

                    if(Vector3.Distance(transform.position, target.position) >= attackRange)
                    {
                        currentState = StatesIndex.Chase;
                        goal = target.position;
                        timeInSearch = 0.0f;
                    }

                    if(!LineCastToTarget())
                        currentState = StatesIndex.Search;
                }
                break;

            #endregion

            #region SEARCH STATE

            case StatesIndex.Search:
                {
                    // play search animation

                    agent.destination = goal;
                    agent.speed = speedSearch;
                    agent.stoppingDistance = searchRange;

                    isChasing = false;

                    CheckTargetDetection();

                    if (Vector3.Distance(transform.position, goal) <= searchRange)
                    {
                        timeInSearch += Time.deltaTime;
                        if (timeInSearch >= timeSearching)
                        {
                            currentState = StatesIndex.Idle;
                            timeInSearch = 0.0f;
                        }
                    }
                }
                break;

            #endregion

            #region STUN STATE

            case StatesIndex.Stun:
                {
                    timeInStunned += Time.deltaTime;
                    if(timeInStunned >= timeStunned)
                    {
                        isStunned = false;
                    }

                    if(!isStunned)
                    {
                        CheckTargetDetection();
                        // play look around animation
                        timeInLookAround += Time.deltaTime;
                        if (timeInLookAround >= timeLookAround)
                        {
                            isStunned = false;
                            currentState = StatesIndex.Idle;
                        }
                    }
                    //else
                        // play stun animation 
                }break;

            #endregion

            #region DEAD STATE

            case StatesIndex.Dead:
                {
                    // play animation dead
                    timeInDead += Time.deltaTime;
                    if (timeInDead >= timeDead)
                        Destroy(gameObject);
                }break;

            #endregion
        }
	}

    void CheckTargetDetection()
    {
        if (DebugMode)
        {
            /* CONE OF SIGHT */
            float mag = -(lineOfSight / Mathf.Sin(angleConeOfSight));
            Transform cone = eye.transform;
            cone.localRotation = Quaternion.Euler(cone.transform.localRotation.x, cone.transform.localRotation.y + angleConeOfSight, cone.transform.localRotation.z);

            Debug.DrawLine(eye.transform.position, eye.transform.position + cone.forward * mag, Color.red);

            cone.localRotation = eye.transform.localRotation;
            cone.localRotation = Quaternion.Euler(cone.transform.localRotation.x, cone.transform.localRotation.y - angleConeOfSight, cone.transform.localRotation.z);
            Debug.DrawLine(eye.transform.position, eye.transform.position + cone.forward * mag, Color.red);

            /* LINE OF SIGHT */
            cone.localRotation = Quaternion.identity;
            Debug.DrawLine(eye.transform.position, eye.transform.position + cone.forward  * lineOfSight, Color.green);
        }

        if (Vector3.Distance(transform.position, target.position) <= lineOfSight)
        {
            Vector3 directionToTarget = (target.position - transform.position);
            
            if (Vector3.Angle(transform.forward, directionToTarget) <= angleConeOfSight)
            {
                if(LineCastToTarget())
                    currentState = StatesIndex.Chase;
            }
        }
    }

    bool LineCastToTarget()
    {
        RaycastHit hit;

        if(DebugMode)
            Debug.DrawLine(eye.transform.position, target.position, Color.red);

        if (Physics.Linecast(eye.transform.position, target.position, out hit))
        {
            if (hit.transform.tag == "Player")
                return true;
        }

        return false;
    }

    bool LineCastToTarget(Transform checkPosition)
    {
        RaycastHit hit;

        if(DebugMode)
            Debug.DrawLine(checkPosition.position, target.position, Color.green);

        if (Physics.Linecast(checkPosition.position, target.position, out hit))
        {
            if (hit.transform.tag == "Player")
                return true;
        }

        return false;
    }

    public void StunEnemy(float newTimeStunned)
    {
        isStunned = true;
        timeInLookAround = 0.0f;
        timeInStunned = 0.0f;
        timeStunned = newTimeStunned;
        currentState = StatesIndex.Stun;
    }
}
