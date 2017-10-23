using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] DataTable enemyData;

    [SerializeField] DataFile btData;

    NavMeshAgent agent;

    bool bHasPath;
    bool bHasTarget;
    bool bIsAttacking;

    Vector3 destination;
    float agentSpeed;
    float agentStopDistance;

    [SerializeField] List<Transform> milestones;
    [SerializeField] int currentMilestone;
    
	void Start ()
    {
        gameObject.AddComponent<BTManager>().Init(btData);

        bHasPath = false;
        bHasTarget = false;
        bIsAttacking = false;

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
	}

    public void SetNextDestination()
    {
        currentMilestone++;
        if (currentMilestone == milestones.Count) currentMilestone = 0;

        destination = milestones[currentMilestone].position;
    }

    public bool GetHasPath() { return bHasPath; }
    public bool GetHasTarget() { return bHasTarget; }
    public bool GetIsAttacking() { return bIsAttacking; }
    public Vector3 GetDestination() { return destination; }

    public void SetHasPath( bool newHasPath ) { bHasPath = newHasPath; }
    public void SetHasTarget( bool newHasTarget ) { bHasTarget = newHasTarget; }
    public void SetIsAttacking( bool newIsAttacking) { bIsAttacking = newIsAttacking; }
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
}