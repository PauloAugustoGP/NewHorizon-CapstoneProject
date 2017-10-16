using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AjControls : ControlClass {

    protected static int _health;
    protected static int _mana;

    protected int Lives;

    protected bool isCrouching;

    Rigidbody rb;
    Animator anim;

    void Start () {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        Health = 400;
        Mana = 100;

        isAlive = true;

        if (Lives == 0)
            Lives = 3;

	}
	
	
	void Update () {

        if (Health <= 0)
        {
            SceneManager.LoadScene("lose");
        }

        if (Mana < 100)
        {
            Mana = Mana + 1;
        }


        if (isAlive)
        {
            //Forward
            if ((Input.GetKeyDown(KeyCode.W)))
            {
                MoveV = 3;
                anim.SetFloat("Speed", MoveV);
                //rb.velocity = transform.forward * MoveV;
                transform.position += transform.forward * Time.deltaTime * MoveV;
            }

            else if ((Input.GetKeyUp(KeyCode.W)))
            {
                MoveV = 0;
                anim.SetFloat("Speed", MoveV);
                //rb.velocity = Vector3.zero;
            }

            //back
            if ((Input.GetKeyDown(KeyCode.S)))
            {
                MoveV = 1.5f;
                anim.SetFloat("Speed", -MoveV);
                //rb.velocity = -transform.forward * MoveV;
                transform.position -= transform.forward * Time.deltaTime * MoveV;
            }

            else if ((Input.GetKeyUp(KeyCode.S)))
            {
                MoveV = 0;
                anim.SetFloat("Speed", MoveV);
                //rb.velocity = Vector3.zero;
            }

            /*
            //strafeLeft
            if ((Input.GetKeyDown(KeyCode.Q)))
            {
                MoveV = 3;
                anim.SetFloat("Speed", -MoveV);
                //rb.velocity = -transform.right * MoveV;
                transform.position -= transform.right * Time.deltaTime * MoveV;
            }

            else if ((Input.GetKeyUp(KeyCode.Q)))
            {
                MoveV = 0;
                anim.SetFloat("Speed", MoveV);
                //rb.velocity = Vector3.zero;
            }

            //strafeRight
            if ((Input.GetKeyDown(KeyCode.E)))
            {
                MoveV = 3;
                anim.SetFloat("Speed", MoveV);
                //rb.velocity = transform.right * MoveV;
                transform.position += transform.right * Time.deltaTime * MoveV;
            }

            else if ((Input.GetKeyUp(KeyCode.E)))
            {
                MoveV = 0;
                anim.SetFloat("Speed", MoveV);
                //rb.velocity = Vector3.zero;
            }
            */
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, Time.deltaTime * Clockwise, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, Time.deltaTime * CounterClockwise, 0);
            }

            if (Input.GetKey(KeyCode.LeftControl))
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

        if (Health <= 0)
        {
            //SoundManager.instance.SoundCaller("Death", 0.2f);
            Died();
        }

    }//update

    public void Damage(int value)
    {
        Health -= value;
    }

    public void Died()
    {
        Lives--;

        /*
        if (Lives == 0)
        {
            SceneManager.LoadScene("");
        }
        else
        {
            SceneManager.LoadScene("End");
        }
        */
    }

    public static int Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health > 1)
            {
                _health = 1;
            }

            if (_health < 0)
            {
                _health = 0;
            }
        }//set
    }//health

    public static int Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;

            if (_mana > 1)
            {
                _mana = 1;
            }

            if (_mana < 0)
            {
                _mana = 0;
            }
        }//set
    }//health
}
