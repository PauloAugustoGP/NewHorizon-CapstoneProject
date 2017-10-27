using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMovement : BTSequence
{
    public BTMovement() : base()
    {
        SetRunCondition("success");

        SetNodeID( 411000, 410000 );
    }
}
