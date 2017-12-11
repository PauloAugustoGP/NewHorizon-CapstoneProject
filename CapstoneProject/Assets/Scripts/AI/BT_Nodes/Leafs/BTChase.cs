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
        agent.SetDestination(target.transform.position);
        agent.SetupNavMeshValues(chaseSpeed, rangeDistance);
        agent.ResumeAgent();

        if (Vector3.Distance(agentTransform.position, target.transform.position) <= rangeDistance)
        {
            agent.StopAgent();
            return 0; // SUCCESS
        }

        return 1; // RUNNING
    }
}