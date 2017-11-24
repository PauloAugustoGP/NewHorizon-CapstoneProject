using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorVertical : MonoBehaviour {
    [Tooltip("Door animation speed.")]
    public float moveSpeed = 3.0f;
    private Vector3 originalPosition;
    public Vector3 moveDistance;
    public Transform player;
    public bool status;
    public float move = 3f;
    public Transform door;

    void Start() {
        status = false;
        originalPosition = transform.localPosition;
        //player = GameObject.Find("Player").transform;
        if (moveDistance == Vector3.zero) {
            moveDistance = new Vector3(move, transform.position.y, 0);
        }
    }
    void Update() {
        //if (Input.GetKeyDown(KeyCode.F)) {
            //if (Vector3.Distance(player.position, this.transform.position) < 5f) {
                //if (status) {
                // trigger exit
                    //StartCoroutine(moveDoor(originalPosition));
                //} else {
                // trigger enter
                //    Vector3 moveTo = originalPosition + moveDistance;
                 //   StartCoroutine(moveDoor(moveTo));
                //}
            //}
        //}
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Vector3 moveTo = originalPosition + moveDistance;
            StartCoroutine(moveDoor(moveTo));
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(moveDoor(originalPosition));
        }
    }
    public IEnumerator moveDoor(Vector3 moveTo) {
        float t = 0f;
        Vector3 startPos = door.localPosition;
        while (t < 1f) {
            t += Time.deltaTime * moveSpeed;
            door.localPosition = Vector3.Slerp(startPos, moveTo, t);
            yield return null;
        }
        status = !status;
        yield return null;
    }

    public void toggleDoorState() {
        if (!status) {
            Vector3 moveTo = originalPosition + moveDistance;
            StartCoroutine(moveDoor(moveTo));
        }
    }
}
