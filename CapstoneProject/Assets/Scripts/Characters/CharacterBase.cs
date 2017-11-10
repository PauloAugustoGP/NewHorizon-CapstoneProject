using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected static bool isAlive;

    protected float _maxHealth;
    protected float _health;
    protected static int _mana;

    protected static float _MoveV;

    public static bool moving;
    public static bool Rested;
    public static bool slowed;

    protected Rigidbody rb;

    //CharacterBehaviour.Health

    /*protected static int Health
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
    }//health*/

    public float GetHealth()
    { return (_health / _maxHealth) * 100; }

    public void SetHealth(float newHealth)
    { _health = newHealth; }


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
