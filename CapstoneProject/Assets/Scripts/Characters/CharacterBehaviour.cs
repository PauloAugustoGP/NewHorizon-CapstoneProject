using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterBehaviour : CharacterBase
{
    protected int Lives;
    protected bool isCrouching;
    protected bool encumbered;

    [Header("Test Bools")]//LIVE TEST VALUES
    [SerializeField] protected bool ValueDebuger;
    [SerializeField] protected bool Invincibility;

    [Header("Drag and Drop")]//DRAG AND DROPS
    [SerializeField] DataTable DT;
    [SerializeField] Transform StartPosition;

    [SerializeField] LayerMask LM;

    Animator anim;
    //HUD pHud;
    CapsuleCollider cc;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //pHud = GetComponent<HUD>();
        cc = GetComponent<CapsuleCollider>();

        //DT.GetTableValue("MaxHealth"); //ON HOLD FOR TESTING

        _maxHealth = 100;
        SetHealth(_maxHealth);

        TeleportResource = 1;

        isAlive = true;

        if (Lives == 0)
            Lives = 3;

    }

    void Update ()
    {
        RaycastHit OverHeadHit;

        if (Physics.Raycast(transform.position, transform.up, out OverHeadHit, (length + (length * 1.5f)), LM))
        {
            encumbered = true;
        }
        else
        {
            encumbered = false;
        }

        //Upon 0 or Low Health
        if (_health <= 0)
        {
            if (!Invincibility)
            {
                //SoundManager.instance.SoundCaller("Death", 0.2f); //First Option Implementation
                Died();
            }
            else if (Invincibility)
            {
                //SoundManager.instance.SoundCaller("Death", 0.2f); //First Option Implementationa
                FakeDied();
            }
        }

        //NOT USED ANY WHERE CAN BE REFERENCED
        if (TeleportResource < 100)
        {
            RecoveryRate(1);
        }

        //Temp Spot //wanted to move to class
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
        }//Moving : True
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

            //Movement
            rb.velocity = ((transform.forward * ft + transform.right * rt).normalized * MoveV * Time.deltaTime);

            //Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = !isCrouching;

                if (!isCrouching && !encumbered)
                {
                    anim.SetBool("Crouching", isCrouching);
                    cc.height = StandardHeight;
                    cc.center = new Vector3(0, 1f, 0);
                }

                if (isCrouching)
                {
                    anim.SetBool("Crouching", isCrouching);
                    cc.height = CrouchedHeight;
                    cc.center = new Vector3(0, 0.5f, 0);
                }
                
            }//LeftControl
        }//isAlive

        //TEMP AREA check mark in INSPECTOR to view log
        if (ValueDebuger)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Heal(20);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Damage(5);
            }

            //Debug.LogFormat("Encombered? {0}",encumbered);
            //Debug.Log(MoveV);
            //Debug.Log(slowed);
            //Debug.Log("Health: " + _health);
            //Debug.Log("Lives: " + Lives);
            //Debug.Log("Get Health Function: " + GetHealth());
            //Debug.Log("Vertical Raycast" +!Physics.Raycast(posUp, Vector3.up,length));
            //Debug.Log(_maxHealth);
            //Debug.Log(isCrouching);
            //Debug.Log(TeleportResource);
            //Debug.Log(_health);
            //Debug.Log("Forward T : " + ft + " Right T : " + rt);
        }//Debuger
    }//Update

    //On Death Call
    protected void Died()
    {
        Lives--;

        if (Lives == 0)
        {
            //They do the same thing But when lives reach 0 this can do more
            transform.SetPositionAndRotation(StartPosition.position, transform.rotation);
            SetHealth(_maxHealth);
        }
        else
        {
            transform.SetPositionAndRotation(StartPosition.position, transform.rotation);
            SetHealth(_maxHealth);
        }     
    }//Died

    protected void FakeDied()
    {
        Lives--;
        if (Lives == 0)
        {
            //transform.SetPositionAndRotation(StartPosition.position, transform.rotation);
            SetHealth(_maxHealth);
        }
        else
        {
            //transform.SetPositionAndRotation(StartPosition.position, transform.rotation);
            SetHealth(_maxHealth);
        }
    }//Died

    //Take Damage Function
    public void Damage(int value)
    {
        SetHealth(_health -= value);
    }

    //Heal Function
    public void Heal(int value)
    {
        SetHealth(_health += value);
    }

    public void ChangeSpeed(string Speed)
    {
        switch (Speed)
        {
            case "Slow":
                _MoveV = 100;
                break;

            case "Normal":
                _MoveV = 400;
                break;

            case "Fast":
                _MoveV = 450;
                break;
        }
    }

    public bool IsCrouching
    {
        get { return isCrouching; }        
    }

    void OnCollisionEnter(Collision H)
    {
        //Healing Gameobject Name!!!! SUBJECT TO CHANGE | OPTIONAL
        if (H.gameObject.name == "HealthPack" || H.gameObject.name == "Health")
        {
            Heal(20);
        }
    }//ColENTER

    void OnTriggerEnter(Collider H)
    {
        //Healing Gameobject Name!!!! SUBJECT TO CHANGE | OPTIONAL
        if (H.gameObject.name == "HealthPack" || H.gameObject.name == "Health")
        {
            Heal(20);
        }
    }//ColTRIGGER

    private void OnParticleCollision(GameObject D)
    {
        if (D.name == "Enemy_Projectile")
        {
            Damage(5);
        }
    }
}
