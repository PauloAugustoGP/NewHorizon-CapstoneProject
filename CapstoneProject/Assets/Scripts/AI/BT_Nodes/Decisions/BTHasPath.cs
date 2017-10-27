using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BTHasPath : BTDecision
{
    public BTHasPath() : base()
    {
        SetNodeID( 410000, 400000 );
    }

    public override int Run( GameObject target )
    {
        int nodeResult = 0;

        // CONDITION TO STOP
        if (!agent.GetHasPath())
            return -1; // FAIL

        for(int i = 0; i < childs.Count; i++)
            nodeResult = childs[i].Run(target);

        return nodeResult;
    }
}