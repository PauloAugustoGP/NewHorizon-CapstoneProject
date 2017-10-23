using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRotateToDestination : BTLeaf
{
    float speedRotation;

    public BTRotateToDestination() : base()
    {
        SetNodeID( 411100, 411000 );
    }

    public override void Init(GameObject character)
    {
        base.Init(character);

        speedRotation = agent.GetDataFromEnemyTable("Turn Rate");
    }

    public override int Run( GameObject target )
    {
        // TODO

        return 0;
    }
}
