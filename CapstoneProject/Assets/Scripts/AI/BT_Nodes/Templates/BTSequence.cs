using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* SEQUENCE TEMPLATE */

public class BTSequence : BTNode
{
    int stopCondition;

    public BTSequence() : base()
    {
        CreateChildsList();

        stopCondition = 0; // Success by Default
    }

    public override int Run( GameObject target )
    {
        int nodeResult = 0;

        for (int i = 0; i < childs.Count; i++)
        {
            nodeResult = childs[i].Run(target);

            if (nodeResult != stopCondition)
                break;
        }

        return nodeResult;
    }

    ///<summary>
    ///Setup run condition for node sequence.
    ///Parameters should be:
    /// 0 = "success" or
    /// -1 = "fail"
    ///</summary>
    public void SetRunCondition(int newConditiokn) { stopCondition = newConditiokn; }

    ///<summary>
    ///Setup run condition for node sequence.
    ///Parameters should be:
    /// "success" or
    /// "fail"
    ///</summary>
    public void SetRunCondition(string newCondition)
    {
        switch(newCondition)
        {
            case "success":
                stopCondition = 0;
                break;

            case "fail":
                stopCondition = -1;
                break;
        }
    }
}
