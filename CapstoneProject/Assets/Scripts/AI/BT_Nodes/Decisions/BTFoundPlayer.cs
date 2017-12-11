using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* DECISION NODE */

public class BTFoundPlayer : BTDecision
{
    float lineOfSight;
    float angleConeOfSight;

    bool isEngaged;

    public BTFoundPlayer() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        lineOfSight = agent.GetDataFromEnemyTable("Line Of Sight");
        angleConeOfSight = agent.GetDataFromEnemyTable("Angle Cone Of Sight");

        isEngaged = false;
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
                if (RayCastToTarget(target.transform))
                {
                    isEngaged = true;
                    agent.SetIsSearching(false);
                    return 0; // SUCCESS
                }
            }
        }

        if(isEngaged)
        {
            isEngaged = false;
            agent.SetIsSearching(true);
        }

        return -1; // FAIL
    }

    /*
    bool LineCastToTarget(Transform targetLocation)
    {
        RaycastHit hit;
        
        if (Physics.Linecast(agentTransform.position, targetLocation.position, out hit))
        {
            if (hit.transform.tag == "Player")
                return true;
        }

        return false;
    }*/

    bool RayCastToTarget(Transform targetLocation)
    {
        Vector3 origin = agentTransform.position;
        Vector3 direction = (targetLocation.position - agentTransform.position);

        RaycastHit hit;

        
        //Debug.DrawRay(origin, origin + direction, Color.green);
        if(Physics.Raycast(origin, direction, out hit))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
