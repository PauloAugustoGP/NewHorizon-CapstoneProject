using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour {

    private Rigidbody _rb;

    [SerializeField]
    private float _runSpeed;

    [SerializeField]
    private float _crouchSpeed;

    [SerializeField]
    private bool _crouched;


	// Use this for initialization
	void Start () {

        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        _runSpeed = 5f;
        _crouchSpeed = 2f;

        _crouched = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        float horInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        Vector3 horMove = transform.right * horInput;
        Vector3 vertMove = transform.forward * vertInput;

        Vector3 moveDir = new Vector3(horInput, 0f, vertInput) * Time.deltaTime;

        if (!_crouched)
            moveDir *= _runSpeed;
        else
            moveDir *= _crouchSpeed;

        _rb.velocity = moveDir;
		
	}

    private void Move(float pHorizontal, float pVertical)
    {
        //_rb.velocity = new Vector3()
    }
}
