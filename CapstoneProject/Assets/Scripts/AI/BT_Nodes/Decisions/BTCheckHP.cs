using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckHP : BTDecision
{
    public BTCheckHP() : base()
    {
    }

    public override int Run(GameObject target)
    {
        int nodeResult = 0;

        return -1; //REMOVE THIS LATER

        // CONDITION TO STOP
        if (agent.GetHealthRatio() > 70f)
            return -1; // FAIL

        for (int i = 0; i < childs.Count; i++)
            nodeResult = childs[i].Run(target);

        return nodeResult;
    }
}
