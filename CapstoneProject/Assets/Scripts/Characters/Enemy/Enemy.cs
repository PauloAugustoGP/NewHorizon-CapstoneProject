using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterBase
{
    [SerializeField] DataTable enemyData;

    [SerializeField] DataFile btData;

    NavMeshAgent agent;

    bool bHasPath;
    bool bIsAttacking;
    bool bIsSearching;
    bool bIsStunned;
    bool canTurn;

    Vector3 destination;
    float agentSpeed;
    float agentStopDistance;
    

    [SerializeField] List<Transform> milestones;
    [SerializeField] int currentMilestone;

    [SerializeField] GameObject meleeCollision;
    [SerializeField] ParticleSystem rangeCollision;
    
	void Start ()
    {
        gameObject.AddComponent<BTManager>().Init(btData);

        bHasPath = false;
        bIsAttacking = false;
        bIsStunned = false;
        canTurn = false;

        _maxHealth = 100.0f;
        _health = _maxHealth;

        currentMilestone = -1;

        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
		if(bHasPath)
        {
            agent.destination = destination;
            agent.speed = agentSpeed;
            agent.stoppingDistance = agentStopDistance;
        }
        else
        {
            agent.destination = transform.position;
            agent.speed = 0.0f;
            agent.stoppingDistance = 5.0f;
        }
	}

    public void SetNextDestination()
    {
        currentMilestone++;
        if (currentMilestone == milestones.Count) currentMilestone = 0;

        destination = milestones[currentMilestone].position;
    }

    public bool GetHasPath() { return bHasPath; }
    public bool GetIsAttacking() { return bIsAttacking; }
    public bool GetIsSearching() { return bIsSearching; }
    public bool GetIsStunned() { return bIsStunned; }
    public bool GetCanTurn() { return canTurn; }
    public Vector3 GetDestination() { return destination; }

    public void SetHasPath( bool newHasPath ) { bHasPath = newHasPath; }
    public void SetIsAttacking( bool newIsAttacking) { bIsAttacking = newIsAttacking; }
    public void SetIsSearching(bool newIsSearching) { bIsSearching = newIsSearching; }
    public void SetIsStunned(bool newIsStunned) { bIsStunned = newIsStunned; }
    public void SetCanTurn( bool newCanTurn ) { canTurn = newCanTurn; }
    public void SetDestination( Vector3 newDestination ) { destination = newDestination; }

    public void SetupNavMeshValues(float speed, float reachDistance)
    {
        agentSpeed = speed;
        agentStopDistance = reachDistance;
    }

    public float GetDataFromEnemyTable(string key)
    {
        return enemyData.GetTableValue(key);
    }

    public void EnableAttackCollision(string attackType)
    {
        switch(attackType)
        {
            case "Melee":
                {
                    meleeCollision.SetActive(true);
                    Invoke("DisableMelee", 1.0f);
                }break;

            case "Ranged":
                {
                    rangeCollision.Play();
                    Invoke("DisableRange", 1.0f);
                }
                break;
        }
    }

    public void DisableMelee() { meleeCollision.SetActive(false); }
    public void DisableRange() { rangeCollision.Stop(); }

    void OnParticleCollision(GameObject particle)
    {
        if (particle.name == "Player_Projectile")
            SetIsStunned(true);

        //particle.GetComponent<ParticleSystem>().main.startSize
    }
}