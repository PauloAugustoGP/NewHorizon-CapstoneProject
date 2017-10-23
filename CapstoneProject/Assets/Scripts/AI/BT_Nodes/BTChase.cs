using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChase : BTNode
{
    public BTChase() : base()
    {
    }

    public override int Run(GameObject target)
    {
        /*Vector3 characterLocation = character.transform.position;
        Vector3 targetLocation = target.transform.position;
        float characterAttackRange = character.GetComponent<Enemy>().GetAttackRange();

        character.GetComponent<Enemy>().SetStopDistance(characterAttackRange);

        if ( Vector3.Distance(characterLocation, targetLocation) <= characterAttackRange )
        {
            character.GetComponent<Enemy>().SetHasPath(false);
            character.GetComponent<Enemy>().SetHasTarget(false);
            character.GetComponent<Enemy>().SetIsAttacking(true);

            return 0; // SUCCESS
        }*/

        return 1; // RUNNING
    }
}