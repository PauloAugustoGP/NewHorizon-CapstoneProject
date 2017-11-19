using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* DECISION NODE */

public class BTFoundPlayer : BTDecision
{
    float lineOfSight;
    float angleConeOfSight;

    public BTFoundPlayer() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        lineOfSight = agent.GetDataFromEnemyTable("Line Of Sight");
        angleConeOfSight = agent.GetDataFromEnemyTable("Angle Cone Of Sight");
    }

    public override int Run(GameObject target)
    {
        if (agent.GetIsStunned())
            return -1; // FAIL
        
        if (Vector3.Distance(agentTransform.position, target.transform.position) <= lineOfSight)
        {
            Vector3 directionToTarget = (target.transform.position - agentTransform.position);

            if (Vector3.Angle(agentTransform.forward, directionToTarget) <= angleConeOfSight)
            {
                if (LineCastToTarget(target.transform))
                {
                    Debug.Log("FOUND PLAYER");
                    agent.SetHasPath(false);
                    agent.SetIsAttacking(true);
                    agent.SetIsSearching(false);
                    return 0;
                }
            }
        }

        if(agent.GetIsAttacking())
        {
            agent.SetIsAttacking(false);
            agent.SetIsSearching(true);
        }

        return -1;
    }

    bool LineCastToTarget(Transform targetLocation)
    {
        RaycastHit hit;
        //Debug.DrawLine(agentTransform.position, new Vector3(targetLocation.position.x, targetLocation.position.y + 1.0f, targetLocation.position.z));
        if (Physics.Linecast(agentTransform.position, new Vector3(targetLocation.position.x, targetLocation.position.y+1.0f, targetLocation.position.z), out hit))
        {
            if (hit.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }
}
