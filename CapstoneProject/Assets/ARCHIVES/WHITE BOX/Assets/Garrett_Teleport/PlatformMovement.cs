using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private float _topFloorHeight;
    private float _bottomFloorHeight;


    [SerializeField] private bool _isUp;
    [SerializeField] private bool _isMoving;



	// Use this for initialization
	void Start () {
        _isUp = false;
        _isMoving = false;
        _topFloorHeight = 4.0f;
        _bottomFloorHeight = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(transform.position.y >= _topFloorHeight && !_isUp)
        {
            _isUp = true;
            _isMoving = false;
        }

        if(transform.position.y <= _bottomFloorHeight && _isUp)
        {
            _isUp = false;
            _isMoving = false;
        }
        

        if(_isMoving)
        {
            if(_isUp)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
            if(!_isUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
        
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            _isMoving = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            _isMoving = true;
            _isUp = true;
        }
    }

}
