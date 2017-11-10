using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTLeaf : BTNode
{
    public BTLeaf() : base()
    {
    }

    public override void Init(GameObject character, int ID, int parentID)
    {
        SetAgentAndTransform(character.GetComponent<Enemy>(), character.transform);

        SetNodeID(ID, parentID);
    }

    public override bool AddChild(BTNode newChild)
    {
        return false;
    }
}
