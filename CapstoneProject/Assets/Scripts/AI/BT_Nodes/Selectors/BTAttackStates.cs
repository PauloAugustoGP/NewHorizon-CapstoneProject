using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAttackStates : BTSelector
{
    float meleeDistance;
    float rangeDistance;

    public BTAttackStates() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        meleeDistance = agent.GetDataFromEnemyTable("Melee Attack Range");
        rangeDistance = agent.GetDataFromEnemyTable("Ranged Attack Range");
    }

    public override int Run(GameObject target)
    {
        float distance = Vector3.Distance(agentTransform.position, target.transform.position);

        if (distance <= meleeDistance) // Melee attack
            return childs[0].Run(target);
        else if (distance > meleeDistance && distance <= rangeDistance) // Range attack
            return childs[1].Run(target);
        //else if()

        return -1;
    }
}
