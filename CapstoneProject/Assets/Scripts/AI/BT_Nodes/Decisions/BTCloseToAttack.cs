using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* DECISION NODE */

public class BTCloseToAttack : BTDecision
{
    float rangeDistance;

    public BTCloseToAttack() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);
        
        rangeDistance = agent.GetDataFromEnemyTable("Ranged Attack Range");
    }

    public override int Run(GameObject target)
    {
        int nodeResult = 0;

        float distance = Vector3.Distance(agentTransform.position, target.transform.position);
        
        //Check if is in range
        if (distance <= rangeDistance)
            return 0;
        
        // Else chase the player
        for (int i = 0; i < childs.Count; i++)
            nodeResult = childs[i].Run(target);

        return nodeResult;
    }
}
