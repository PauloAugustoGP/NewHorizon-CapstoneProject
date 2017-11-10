using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWalk : BTLeaf
{
    float speed;
    float reachDistance;
    float isCycle;
    
    public BTWalk() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        speed = agent.GetDataFromEnemyTable("Walk Speed");
        reachDistance = agent.GetDataFromEnemyTable("Reach Distance");
        isCycle = agent.GetDataFromEnemyTable("Cycle Walking");
    }

    public override int Run( GameObject target )
    {
        agent.SetupNavMeshValues( speed, reachDistance );

        if (Vector3.Distance(agentTransform.position, agent.GetDestination()) <= reachDistance)
        {
            agent.SetHasPath(false);

            return 0; // SUCCESS
        }

        return 1; // RUNNING
    }
}