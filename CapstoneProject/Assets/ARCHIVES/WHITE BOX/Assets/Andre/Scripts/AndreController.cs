using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndreController : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    public Camera cam;
    public float rotateSpeed = 2f;
    public RectTransform messageSpawnPoint;
    public PlayerMessage spawn;
    public PlayerUI accessUI;
  
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 10 * Time.deltaTime));
            anim.SetFloat("Speed", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -10 * Time.deltaTime));
            anim.SetFloat("Speed", 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-10 * Time.deltaTime, 0, 0));
            anim.SetFloat("Speed", 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));
            anim.SetFloat("Speed", 1);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetFloat("Speed", 0);
        }
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        cam.transform.Rotate(-Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            GameObject.FindObjectOfType<Level_Manager>().PlayerDeath();
        }
    }
}
