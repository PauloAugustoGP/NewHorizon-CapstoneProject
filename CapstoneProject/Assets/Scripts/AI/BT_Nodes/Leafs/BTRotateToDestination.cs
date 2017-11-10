using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRotateToDestination : BTLeaf
{
    float speedRotation;

    public BTRotateToDestination() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        speedRotation = agent.GetDataFromEnemyTable("Turn Rate");
    }

    public override int Run( GameObject target )
    {
        if (!agent.GetCanTurn()) return 0;

        agent.SetupNavMeshValues(0.0f, 0.0f);

        Vector3 direction = (new Vector3(agent.GetDestination().x, 0f, agent.GetDestination().z) - new Vector3(agentTransform.position.x, 0f, agentTransform.position.z)).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        agentTransform.rotation = Quaternion.Slerp(agentTransform.transform.rotation, lookRotation, Time.deltaTime * speedRotation);

        if (Quaternion.Angle(lookRotation, agentTransform.rotation) < 5.0f)
        {
            agent.SetCanTurn(false);
            return 0;
        }
        else return 1;
    }
}
