using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {
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
        originalPosition = transform.position;
        //player = GameObject.Find("Player").transform;
        if (moveDistance == Vector3.zero) {
            moveDistance = new Vector3(move, transform.position.y, 0);
        }
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            //if (Vector3.Distance(player.position, this.transform.position) < 5f) {
                if (status) {
                // trigger exit
                    StartCoroutine(moveDoor(originalPosition));
                } else {
                // trigger enter
                    Vector3 moveTo = originalPosition + moveDistance;
                    StartCoroutine(moveDoor(moveTo));
                }
            //}
        }
    }
    public IEnumerator moveDoor(Vector3 moveTo) {
        float t = 0f;
        Vector3 startPos = door.position;
        while (t < 1f) {
            t += Time.deltaTime * moveSpeed;
            door.position = Vector3.Lerp(startPos, moveTo, t);
            yield return null;
        }
        status = !status;
        yield return null;
    }
}
