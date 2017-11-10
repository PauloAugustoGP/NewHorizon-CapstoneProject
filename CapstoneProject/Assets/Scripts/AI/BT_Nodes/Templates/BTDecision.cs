using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDecision : BTNode
{
    public BTDecision() : base()
    {
        CreateChildsList();
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        SetAgentAndTransform(character.GetComponent<Enemy>(), character.transform);

        SetNodeID(ID, parentID);
    }
}
