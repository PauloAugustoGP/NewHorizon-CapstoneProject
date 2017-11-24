using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : Doors {
    
	public Transform leftDoor;
	public Transform rightDoor;
    [Tooltip("Change the x,y,z variables for direction.")]
	public Vector3 moveDistanceL;
	public Vector3 moveDistanceR;
    [Tooltip("Start position of the left door.")]
	public Vector3 leftOrigin;
    [Tooltip("Start position of the right door.")]
	public Vector3 rightOrigin;
    [Tooltip("False for closed, true for open")]
    private type _slidingNormal;

    public type SlidingNormal {
        get { return _slidingNormal; }
        set { _slidingNormal = value; }
    }

    void Start() {
		status = false;
        player = GameObject.Find("Player").transform;
        if (!leftDoor) {
            leftDoor = GameObject.Find("LeftDoor").GetComponent<Transform>();
            leftOrigin = leftDoor.position;
            Debug.Log(leftOrigin);
        }
        if(!rightDoor) {
            rightDoor = GameObject.Find("RightDoor").GetComponent<Transform>();
            rightOrigin = rightDoor.position;
            Debug.Log(rightOrigin);
        }

        if(move <= 0) {
            move = 1f;
        }

        if(moveDistanceL == Vector3.zero) {
            moveDistanceL = new Vector3(-move, leftDoor.localPosition.y, 0);
        }
        if(moveDistanceR == Vector3.zero) {
            moveDistanceR = new Vector3(move, rightDoor.localPosition.y, 0);
        }
    }

    public IEnumerator LMoveDoor(Vector3 moveTo) {
        Debug.Log("LmoveDoor Starting");
        float t = 0f;
        Vector3 startPos = leftDoor.localPosition;
        while (t < 1f) {
            t += Time.deltaTime * moveSpeed;
            //startPos = Vector3.Lerp(startPos, moveTo, t);
            leftDoor.localPosition += moveTo * Time.deltaTime * moveSpeed;
            yield return null;
        }
        yield return null;
        Debug.Log("LmoveDoor Finishing");
    }
    public IEnumerator RMoveDoor(Vector3 moveTo) {
        Debug.Log("RmoveDoor Starting");
        float t = 0f;
        Vector3 startPos = rightDoor.localPosition;
        while (t < 1f) {
            t += Time.deltaTime * moveSpeed;
            //startPos = Vector3.Lerp(startPos, moveTo, t);
            //startPos += moveTo;
            rightDoor.localPosition += moveTo * Time.deltaTime * moveSpeed;
            yield return null;
        }
        yield return null;
        Debug.Log("RmoveDoor Finishing");
    }

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")) {
            //if(doorEnabled) {
                if(!status) {
                    StartCoroutine(Open());
                }
            //}
		}
	}

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            inTrigger = true;
        }
    }

    void OnTriggerExit (Collider other) {
		if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(Close());
        }
	}

	public override void toggleDoorState() {
		if (!status) {
            StartCoroutine(Open());
		}
	}

    public void LeftDoorMove(bool nega = false) {
        if(nega) {
            moveDistanceL = -moveDistanceL;
        } else {
            moveDistanceL = new Vector3(-move, moveDistanceL.y);
        }
        Vector3 moveTo = leftOrigin + moveDistanceL;
        StartCoroutine(LMoveDoor(moveTo));
    }

    public override IEnumerator Open() {
        LeftDoorMove();
        RightDoorMove();
        status = !status;
        yield return null;
    }

    public override IEnumerator Close() {
        LeftDoorMove(true);
        RightDoorMove(true);
        status = !status;
        yield return null;
    }

    public void RightDoorMove(bool nega = false) {
        if(nega) {
            moveDistanceR = -moveDistanceR;
        } else {
            moveDistanceR = new Vector3(Mathf.Abs(moveDistanceR.x), moveDistanceR.y);
        }
        Vector3 moveTo = rightOrigin + moveDistanceR;
        StartCoroutine(RMoveDoor(moveTo));
    }
}
