using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTStun : BTLeaf
{
    float timeStunned;
    float currentTime;

    public BTStun() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        timeStunned = agent.GetDataFromEnemyTable("Time Stunned");
        currentTime = 0.0f;
    }

    public override int Run(GameObject target)
    {
        agent.StopAgent();

        currentTime += Time.deltaTime;
        if(currentTime >= timeStunned)
        {
            currentTime = 0.0f;
            agent.SetIsStunned(false);
            agent.SetIsSearching(true);
            return 0; // SUCCESS
        }

        return 1; // RUNNING
    }
}
