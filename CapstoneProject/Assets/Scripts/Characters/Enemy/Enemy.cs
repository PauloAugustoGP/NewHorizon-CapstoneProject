using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterBase
{
    [SerializeField] DataTable enemyData;

    [SerializeField] DataFile btData;

    NavMeshAgent agent;
    
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
        
        bIsStunned = false;
        canTurn = false;

        _maxHealth = 100.0f;
        _health = _maxHealth;

        currentMilestone = -1;

        agent = GetComponent<NavMeshAgent>();
        StopAgent();
    }
	
	void Update ()
    {
		/*if(bHasPath)
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
        }*/
	}

    public void SetNextDestination()
    {
        currentMilestone++;
        if (currentMilestone == milestones.Count) currentMilestone = 0;

        destination = milestones[currentMilestone].position;
    }
    
    public bool GetIsSearching() { return bIsSearching; }
    public bool GetIsStunned() { return bIsStunned; }
    public bool GetCanTurn() { return canTurn; }
    public Vector3 GetDestination() { return destination; }
    
    public void SetIsSearching(bool newIsSearching) { bIsSearching = newIsSearching; }
    public void SetIsStunned(bool newIsStunned) { bIsStunned = newIsStunned; }
    public void SetCanTurn( bool newCanTurn ) { canTurn = newCanTurn; }
    public void SetDestination( Vector3 newDestination ) { destination = newDestination; }

    public void SetupNavMeshValues(float speed, float reachDistance)
    {
        agent.destination = destination;
        agentSpeed = speed;
        agentStopDistance = reachDistance;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    public void ResumeAgent()
    {
        agent.isStopped = false;
    }

    public bool GetAgentStatus() { return agent.isStopped; }

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
    }
}