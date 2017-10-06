using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distractObject1 : MonoBehaviour
{
    public bool activated;
    public Transform Ball;
    float Timer;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (activated)
        {
            //PingPong is a match function to variate between 0 and the given value(in this case 1).
            Timer += Time.deltaTime;
            Vector3 v = Ball.localPosition;
            v.y = -1 - Mathf.PingPong(Timer, 1);
            if (Timer >= 2)
            {
                activated = false;
                v.y = -1;
            }
            Ball.localPosition = v;
        }
        
	}

    void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activated = true;
            Timer = 0;
        }
    }
}
