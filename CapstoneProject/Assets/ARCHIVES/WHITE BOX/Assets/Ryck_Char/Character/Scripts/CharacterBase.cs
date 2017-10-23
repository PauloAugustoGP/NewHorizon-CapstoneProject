using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected static bool isAlive;

    protected static int _health;
    protected static int _mana;

    protected static float _MoveV;

    protected static bool moving;
    protected static bool Rested;
    protected static bool slowed;

    protected float Clockwise = 500.0f;
    protected float CounterClockwise = -500.0f;

    protected Rigidbody rb;

    protected void Forward()
    {
        moving = true;
        rb.velocity = transform.forward * Time.deltaTime * MoveV;
    }

    protected void Backward()
    {
        moving = true;
        rb.velocity = -transform.forward * Time.deltaTime * MoveV;
    }

    protected void RotateRight()
    {
        moving = true;
        transform.Rotate(0, Time.deltaTime * Clockwise, 0);
    }

    protected void RotateLeft()
    {
        moving = true;
        transform.Rotate(0, Time.deltaTime * CounterClockwise, 0);
    }

    protected void StrafeRight()
    {
        moving = true;
        rb.velocity = transform.right * Time.deltaTime * (MoveV / 2);
    }

    protected void StrafeLeft()
    {
        moving = true;
        rb.velocity = -transform.right * Time.deltaTime * (MoveV / 2);
    }

    public static int Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health > 100)
            {
                _health = 100;
            }

            if (_health < 0)
            {
                _health = 0;
            }
        }//set
    }//health

    public static int TeleportResource
    {
        get { return _mana; }
        set
        {
            _mana = value;

            if (_mana > 100)
            {
                _mana = 100;
            }

            if (_mana < 0)
            {
                _mana = 0;
            }
        }//set
    }//Mana

    public static float MoveV
    {
        get { return _MoveV; }
        set
        {
            _MoveV = value;
        }//set
    }//MoveV

    public void Damage(int value)
    {
        
        Health -= value;
    }

    protected void RecoveryRate(int value)
    {
        if (Rested)
        {
            TeleportResource += value * 4;
        }

        if (!Rested)
        {
            TeleportResource += value;
        }
    }
}
