using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brianCharacter : MonoBehaviour
{

    // Use this for initialization



    Rigidbody rb;
    Animator anim;

    public float speed;
    private bool c_jump;
    private float c_jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        speed = 5.0f;



    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKey(KeyCode.Space))
        {
            c_jump = true;
            rb.AddForce(Vector3.up * c_jumpForce);
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
