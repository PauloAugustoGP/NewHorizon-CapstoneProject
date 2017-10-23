using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRoot : BTSequence
{
	public BTRoot() : base()
    {
        SetRunCondition( "fail" );

        SetNodeID( 0, -1 );
    }
}