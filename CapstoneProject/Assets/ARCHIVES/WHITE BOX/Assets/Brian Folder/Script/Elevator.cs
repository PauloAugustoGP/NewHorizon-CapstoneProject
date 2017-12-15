using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool ElevatorON;
    private SwitchBrian button;
    [SerializeField]
    private float ElevatorUP;
    private float ElevatorDown;
    [SerializeField]
    private float direction;
    private Transform Player;


	// Use this for initialization
	void Start ()
    {
        ElevatorDown = transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ElevatorON)
        {
            transform.Translate(0, direction * Time.deltaTime, 0);
            //If the player is under elevator's height, the elevator will go to the lowest position.
            if(direction > 0 && transform.position.y > Player.position.y)
            {
                direction *= -1;
            }
            if(transform.position.y > ElevatorUP)
            {
                //elevator arrived at the top
                transform.position = new Vector3(transform.position.x, ElevatorUP, transform.position.z);
                ElevatorON = false;
                button.isActivated = false;
            }
            if (transform.position.y <= ElevatorDown)
            {
                //elevator arrived at the bottom
                transform.position = new Vector3(transform.position.x, ElevatorDown, transform.position.z);
                ElevatorON = false;
                button.isActivated = false;
            }
        }

    }
    public void ElevatorTriggered(SwitchBrian b)
    {
        button = b;
        ElevatorON = true;
        if(transform.position.y <= ElevatorDown)
        {
            //Forcing direction to be positive number
            direction = Mathf.Abs(direction);
        }
        else
        {
            //Forcing direction to be negative number
            direction = -Mathf.Abs(direction);
        }  
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.CompareTag("Player"))
        {
            Player = c.transform;
        }
    }
}
