using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class charControlScript : charScript {

    protected CharacterController cc;
    protected Rigidbody rb;
    protected Animator anim;

    protected float _PlayerHealth;

    public int playerState = 2;

    public float regSpeed;

    MoveTeleport tel;

    /*protected AnimatorStateInfo curStat;

    protected static int idleState = Animator.StringToHash("Base Layer.idle");*/

    //float hi = 1.5f;


    void Start () {

        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        tel = GetComponent<MoveTeleport>();

        JumpPower = 2f;

        GravityMult = 2f;

        TurnSpeed = 1.5f;

        regSpeed = 2f;
   
        //MoveSpeedMultiplier = new Vector3(0,0,12.5f);

        AnimSpeedMultiplier = 1f;

        PlayerHealth = 1;

        Mana = 100;
    }
	
	// Update is called once per frame
	void Update () {

        if (Health <= 0)
        {
            SceneManager.LoadScene("lose");
        }

        if (Mana < 100)
        {
            Mana = Mana + 1;
        }

        if (playerState == 1)
        {
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical") * MoveSpeedMultiplier);

                transform.Rotate(0, Input.GetAxis("Horizontal") * TurnSpeed, 0);

                moveDirection = transform.TransformDirection(moveDirection);

                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    //anim.SetTrigger("isCrouch");
                    //crouch();
                }

            }//isGrounded

            moveDirection.y -= GravityMult * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);

        }//type == 1

        if (playerState == 2)
        {
            if (cc.isGrounded)
            {
                //var x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeedMultiplier;

                //var z


                if (Input.GetKey(KeyCode.W))
                {
                    Debug.Log("Forward");

                    rb.velocity = transform.forward * MoveSpeedMultiplier;
                      
                    //transform.Translate(0,0, 10 * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    Debug.Log("back");

                    rb.velocity = -transform.forward * MoveSpeedMultiplier;

                    //transform.Translate(0, 0, -10 * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    Debug.Log("left");

                    rb.velocity = transform.right * MoveSpeedMultiplier; 

                    //transform.Translate(10 * Time.deltaTime, 0,0 );
                }

                if (Input.GetKey(KeyCode.D))
                {
                    Debug.Log("Right");

                    rb.velocity = -transform.right * MoveSpeedMultiplier;

                    //transform.Translate(-10 * Time.deltaTime, 0, 0);
                }

                moveDirection = transform.TransformDirection(moveDirection);
                
                //loo
               
            }

            moveDirection.y -= GravityMult * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);

        }//playerstate == 2

        if (playerState == 3)
        {
            if (cc.isGrounded)
            {
                float moveHorizontal = Input.GetAxisRaw("Horizontal");
                float moveVertical = Input.GetAxisRaw("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                transform.rotation = Quaternion.LookRotation(movement);


                transform.Translate(movement * MoveSpeedMultiplier * Time.deltaTime, Space.World);
              
            }

            moveDirection.y -= GravityMult * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);
        }



            
}

    public float PlayerHealth
    {
        get { return Health; }
        set
        {
            Health = value;

            if (Health > 6)
            {
                Health = 6;
            }
            else if (Health < 0)
            {
                Health = 0;
            }
        }
    }

    protected void crouch()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Damage(1);
        }

        if (c.gameObject.tag == "goal")
        {
            SceneManager.LoadScene("Winning");
        }
                
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "DeathBox")
        {            
            Level_Manager.instance.PlayerDeath();
            
        }
    }

    void Damage(int value)
    {
        Health -= value;
    }
}
