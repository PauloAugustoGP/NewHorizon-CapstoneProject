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
                _MoveV = 400;
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
            float ft = 0;
            float rt = 0;

            if (Input.GetKey(KeyCode.W))
            {
                //forward
                moving = true;
                ft = 1;
                anim.SetFloat("Speed", MoveV);
            }

            if (Input.GetKey(KeyCode.S))
            {
                //back
                slowed = true;
                moving = true;
                ft = -1;
                anim.SetFloat("Speed", -MoveV);
            }

            if (Input.GetKey(KeyCode.D))
            {
                //right
                moving = true;
                rt = 1;
                anim.SetFloat("Speed", MoveV);
            }

            if (Input.GetKey(KeyCode.A))
            {
                //left
                moving = true;
                rt = -1;
                anim.SetFloat("Speed", MoveV);
            }

            if ((Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.W))
                || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.D)))
            {
                moving = false;
                anim.SetFloat("Speed", 0);
            }

            rb.velocity = (transform.forward * ft + transform.right * rt) * MoveV * Time.deltaTime;

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
            //Debug.Log(_health);
            //Debug.Log(_maxHealth);
            //Debug.Log(TeleportResource);
            //Debug.Log(_health);
            //Debug.Log("Forward T : " + ft + " Right T : " + rt);
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

    public void Damage(int value)
    {
        _health -= value;
    }

    public void Heal(int value)
    {
        _health += value;
    }

    void OnCollisionEnter(Collision c)
    {
        //collision timer

        if (c.gameObject.name == "Enemy")
        {
            
            Damage(20);
       
        }
    }

}
