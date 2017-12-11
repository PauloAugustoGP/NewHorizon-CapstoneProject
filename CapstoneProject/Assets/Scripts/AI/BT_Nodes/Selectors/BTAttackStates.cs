using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ATTACKS : int
{
    MELEE,
    RANGE,
    SPECIAL,
    GRANADE,
    Size
}

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
            return childs[(int)ATTACKS.MELEE].Run(target);
        else if (distance > meleeDistance && distance <= rangeDistance) // Range attack
            return childs[(int)ATTACKS.RANGE].Run(target);
        //else if()

        return -1;
    }
}
