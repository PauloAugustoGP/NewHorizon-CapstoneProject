using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIdle : BTLeaf
{
    float timeRunning;
    float timeInState;
    
    public BTIdle() : base()
    {
        timeRunning = 0.0f;

        SetNodeID( 421000, 420000 );
    }

    public override void Init(GameObject character)
    {
        base.Init(character);

        timeInState = agent.GetDataFromEnemyTable("Time In Idle");
    }

    public override int Run( GameObject target )
    {
        agent.SetupNavMeshValues( 0.0f, 0.0f );

        timeRunning += Time.deltaTime;

        if(timeRunning >= timeInState)
        {
            timeRunning = 0.0f;

            agent.SetNextDestination();
            agent.SetHasPath(true);

            return 0;
        }

        return 1;
    }
}