using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class charScript : MonoBehaviour
{
    [SerializeField]
    protected float JumpPower;

    [SerializeField]
    protected float GravityMult;

    [SerializeField]
    protected float TurnSpeed;

    [SerializeField]
    public float MoveSpeedMultiplier;

    [SerializeField]
    protected float AnimSpeedMultiplier;

    [SerializeField]
    protected float _health;

    [SerializeField]
    protected float _mana;

    protected Vector3 moveDirection = Vector3.zero;

    //public float speed;

    protected void moveSlow()
    {
        if (MoveSpeedMultiplier <= 1)
            MoveSpeedMultiplier = 3;
        else if (MoveSpeedMultiplier >= 3)
            MoveSpeedMultiplier = 1;
    }

    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health > 6)
            {
                _health = 6;
            }
            else if (_health < 0)
            {
                _health = 0;
            }
        }
    }

    public float Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;

            if (_mana > 6)
            {
                _mana = 6;
            }
            else if (_mana < 0)
            {
                _mana = 0;
            }
        }
    }
}


