using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour {

	// Use this for initialization
	public Transform player;
	public Transform leftDoor;
	public Transform rightDoor;
	public Vector3 moveDistance;
	public float move;
	public float moveSpeed = 3.0f;
	private Vector3 leftOriginal;
	private Vector3 rightOriginal;
	public bool status;
	public bool dEnabled;
	void Start() {
		status = false;
		//player = GameObject.Find("Player").transform;
		if (move <= 0) {
			move = 3f;
		}
		leftOriginal = leftDoor.position;
		rightOriginal = rightDoor.position;
		if (moveDistance == Vector3.zero) {
			moveDistance = new Vector3(move, transform.position.y, 0);
		}
		if (!leftDoor) {
			leftDoor = GameObject.Find("LeftDoor").transform;
		}
		if (!rightDoor) {
			rightDoor = GameObject.Find("RightDoor").transform;
		}
		Debug.Log (leftDoor);
		Debug.Log (rightDoor);
	}
	public IEnumerator moveDoor(Vector3 doorPos, Vector3 moveTo) {
		float t = 0f;
		Vector3 startPos = doorPos;
		while (t < 1f) {
			t += Time.deltaTime * moveSpeed;
			doorPos = Vector3.Lerp(startPos, moveTo, t);
			Debug.Log (doorPos);
			yield return null;
		}
		status = !status;
		yield return null;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			//if (dEnabled) {
				Vector3 moveToLeft = leftOriginal + moveDistance;
				Vector3 moveToRight = rightOriginal+ moveDistance;
				StartCoroutine(moveDoor(leftOriginal, moveToLeft));
				StartCoroutine(moveDoor(rightOriginal, moveToRight));
			//}
		}
	}

	public void toogleDoorState() {
		if (!status) {
			Vector3 moveToLeft = leftOriginal + moveDistance;
			Vector3 moveToRight = rightOriginal+ moveDistance;
			StartCoroutine(moveDoor(leftOriginal, moveToLeft));
			StartCoroutine(moveDoor(rightOriginal, moveToRight));
		}
	}
}
