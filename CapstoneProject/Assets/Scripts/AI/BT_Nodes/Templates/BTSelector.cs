using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTNode
{
    public BTSelector() : base()
    {
        CreateChildsList();
    }

    public override void Init(GameObject character)
    {
        SetAgent(character.GetComponent<Enemy>());
    }
}
