using System.Collections;
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

        Health = 100;
        TeleportResource = 1;

        isAlive = true;

        

        if (Lives == 0)
            Lives = 3;
	}
	
	void Update ()
    {
        //Upon 0 or Low Health
        if (Health <= 0)
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
            }

            //strafeRight
            /*if ((Input.GetKey(KeyCode.E)))
            {
                anim.SetFloat("Speed", MoveV);
                StrafeRight();
            }

            else if ((Input.GetKeyUp(KeyCode.E)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
            }

            //strafeLeft
            if ((Input.GetKey(KeyCode.Q)))
            {
                anim.SetFloat("Speed", -MoveV);
                StrafeLeft();
            }

            else if ((Input.GetKeyUp(KeyCode.Q)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
            }*/

            //Turning
            if (Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                RotateLeft();
            }

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
            Debug.Log(Health);
        }

    }//Update

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

        Health -= value;
    }

    void OnCollisionEnter(Collision c)
    {
        //collision timer
        if (c.gameObject.tag == "Enemy")
        {
            Debug.LogWarning("Collision with en");
            Damage(20);
        }
    }

}
