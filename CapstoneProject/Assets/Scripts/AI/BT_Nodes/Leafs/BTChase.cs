using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChase : BTLeaf
{
    float rangeDistance;
    float chaseSpeed;

    public BTChase() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        rangeDistance = agent.GetDataFromEnemyTable("Ranged Attack Range");
        chaseSpeed = agent.GetDataFromEnemyTable("Chase Speed");
    }

    public override int Run(GameObject target)
    {
        agent.SetupNavMeshValues(chaseSpeed, rangeDistance);

        if (Vector3.Distance(agentTransform.position, target.transform.position) <= rangeDistance)
        {
            agent.SetHasPath(false);
            return 0; // SUCCESS
        }

        return 1; // RUNNING
    }
}