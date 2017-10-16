using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlClass : MonoBehaviour {

    protected static float _MoveV;
    protected static bool moving;
    protected static bool isAlive;

    protected float Clockwise = 500.0f;
    protected float CounterClockwise = -500.0f;

    public static float MoveV
    {
        get { return _MoveV; }
        set
        {
            _MoveV = value;

            /*
            if (_MoveV > 400)
            {
                _MoveV = 400;
            }

            if (_MoveV < 0)
            {
                _MoveV = 0;
            }
            */
        }//set
    }//health

}
