using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIdleStates : BTSelector
{
    int state;
    bool hasState;

    public BTIdleStates() : base()
    {
        state = 0;
        hasState = false;
    }

    public override int Run( GameObject target )
    {
        int nodeResult = 0;

        if (!hasState)
        {
            state = Random.Range(0, childs.Count);
            hasState = true;
        }

        nodeResult = childs[state].Run(target);

        if (nodeResult == 0)
            hasState = false;

        return nodeResult;
    }
}
