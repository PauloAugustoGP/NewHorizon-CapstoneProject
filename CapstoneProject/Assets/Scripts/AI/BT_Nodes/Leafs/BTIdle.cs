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
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

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
            agent.ResumeAgent();
            agent.SetCanTurn(true);

            return 0;
        }

        return 1;
    }
}