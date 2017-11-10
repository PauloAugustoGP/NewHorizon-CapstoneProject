using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIsSearching : BTDecision
{
    public BTIsSearching() : base()
    {
    }

    public override int Run(GameObject target)
    {
        int nodeResult = 0;

        // CONDITION TO STOP
        if (!agent.GetIsSearching())
            return -1; // FAIL

        for (int i = 0; i < childs.Count; i++)
            nodeResult = childs[i].Run(target);

        return nodeResult;
    }
}
