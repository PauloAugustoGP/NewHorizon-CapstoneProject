using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPatrol : BTSequence
{
    public BTPatrol() : base()
    {
        SetRunCondition( "fail" );

        SetNodeID( 400000, 0 );
    }
}