﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterBehaviour : CharacterBase
{
    protected int Lives;

    public bool isCrouching;

    Animator anim;

    HUD pHud;


    /// <summary>
    /// Programmer Var
    /// 

    public bool ValueDebuger;

    /// </summary>

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pHud = GetComponent<HUD>();

        _maxHealth = 100;
        _health = _maxHealth;

        TeleportResource = 1;

        isAlive = true;

        

        if (Lives == 0)
            Lives = 3;
	}
	
	void Update ()
    {
        //Upon 0 or Low Health
        if (_health <= 0)
        {
            //SoundManager.instance.SoundCaller("Death", 0.2f);
            Died();
        }

        if (TeleportResource < 100)
        {
            RecoveryRate(1);
        }

        ///Temp Spot //wanted to move to class
        if (moving)
        {
            slowed = isCrouching;

            if (!slowed)
            {
                _MoveV = 380;
            }

            if (slowed)
            {
                _MoveV = 100;
            }
        }

        else if (!moving)
        {
            _MoveV = 0;
        }

        if (isAlive)
        {
            //Forward
            if ((Input.GetKey(KeyCode.W)))
            {
                anim.SetFloat("Speed", MoveV); 
                Forward();
            }

            else if ((Input.GetKeyUp(KeyCode.W)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
                rb.velocity = Vector3.zero;
            }

            //Back
            if ((Input.GetKey(KeyCode.S)))
            {
                anim.SetFloat("Speed", -MoveV);
                Backward();
            }

            else if ((Input.GetKeyUp(KeyCode.S)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
                rb.velocity = Vector3.zero;
            }

            //strafeRight
            if ((Input.GetKey(KeyCode.D)))
            {
                anim.SetFloat("Speed", MoveV);
                StrafeRight();
            }

            else if ((Input.GetKeyUp(KeyCode.D)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
                rb.velocity = Vector3.zero;
            }

            //strafeLeft
            if ((Input.GetKey(KeyCode.A)))
            {
                anim.SetFloat("Speed", -MoveV);
                StrafeLeft();
            }

            else if ((Input.GetKeyUp(KeyCode.A)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
                rb.velocity = Vector3.zero;
            }

            /*
            //Turning
            if (Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                RotateLeft();
            }*/

            //Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = !isCrouching;

                if (!isCrouching)
                {
                    anim.SetBool("Crouching", isCrouching);
                }
                if (isCrouching)
                {
                    anim.SetBool("Crouching", isCrouching);
                }
            }
        }

        if (ValueDebuger)
        {
            //Debug.Log(MoveV);
            //Debug.Log(slowed);
            //Debug.Log(Health);
            //Debug.Log(TeleportResource);
            Debug.Log(_health);
        }

    }//Update

    //On Death call
    protected void Died()
    {
        Lives--;

        if (Lives == 0)
        {
            SceneManager.LoadScene("lose");
        }
        else
        {
            //Load current level
            //SceneManager.LoadScene(******);
        }     
    }

    protected void Damage(int value)
    {

        _health -= value;
    }

    void OnCollisionEnter(Collision c)
    {
        //collision timer
        if (c.gameObject.tag == "Enemy")
        {
            
            Damage(20);
            
        }
    }

}
