using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSearch : BTLeaf
{
    float speed;
    float distance;
    float timeSearching;
    float currentTime;

    bool isSetup;

    public BTSearch() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        base.Init(character, ID, parentID);

        speed = agent.GetDataFromEnemyTable("Search Speed");
        distance = agent.GetDataFromEnemyTable("Search Distance");
        timeSearching = agent.GetDataFromEnemyTable("Time Searching");

        currentTime = 0.0f;

        isSetup = false;
    }

    public override int Run(GameObject target)
    {
        if (!isSetup)
        {
            agent.SetupNavMeshValues(speed, distance);
            agent.SetDestination(target.transform.position);
            
            agent.ResumeAgent();

            isSetup = true;
        }
        
        if (Vector3.Distance(agentTransform.position, agent.GetDestination()) <= distance)
        {
            agent.StopAgent();
            
            currentTime += Time.deltaTime;
            if (currentTime >= timeSearching)
            {
                currentTime = 0.0f;
                agent.SetIsSearching(false);
                return 0; // SUCCESS
            }
        }

        return 1; // RUNNING
    }
}
