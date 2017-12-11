using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BTHasPath : BTDecision
{
    public BTHasPath() : base()
    {
    }

    public override int Run( GameObject target )
    {
        int nodeResult = 0;

        // CONDITION TO STOP
        if (agent.GetAgentStatus())
            return -1; // FAIL

        for(int i = 0; i < childs.Count; i++)
            nodeResult = childs[i].Run(target);

        return nodeResult;
    }
}