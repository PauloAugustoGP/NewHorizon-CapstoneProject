using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTLeaf : BTNode
{
    public BTLeaf() : base()
    {
    }

    public override void Init(GameObject character)
    {
        SetAgentAndTransform(character.GetComponent<Enemy>(), character.transform);
    }

    public override bool AddChild(BTNode newChild)
    {
        return false;
    }
}
