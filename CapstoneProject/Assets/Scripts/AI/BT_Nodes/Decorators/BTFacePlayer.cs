using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFacePlayer : BTDecorator
{
    public BTFacePlayer() : base()
    {
        currentType = DecoratorType.Rotator;
    }
}
