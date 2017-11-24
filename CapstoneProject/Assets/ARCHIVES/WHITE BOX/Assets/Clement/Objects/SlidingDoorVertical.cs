using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorVertical : Doors {
    [Tooltip("Door animation speed.")]
    private Vector3 originalPosition;
    public Transform door;

    void Start() {
        originalPosition = transform.localPosition;
        if (moveDistance == Vector3.zero) {
            moveDistance = new Vector3(transform.localPosition.x, move, 0);
        }
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(Open());
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(Close());
        }
    }
    public IEnumerator moveDoor(Vector3 moveTo) {
        float t = 0f;
        Vector3 startPos = transform.localPosition;
        while (t < 1f) {
            t += Time.deltaTime * moveSpeed;
            transform.localPosition = Vector3.Slerp(startPos, moveTo, t);
            yield return null;
        }
        status = !status;
        yield return null;
    }

    public override IEnumerator Open() {
        Vector3 moveTo = originalPosition + moveDistance;
        StartCoroutine(moveDoor(moveTo));
        yield return null;
    }

    public override IEnumerator Close() {
        StartCoroutine(moveDoor(originalPosition));
        yield return null;
    }

    public override void toggleDoorState() {
        if (!status) {
            StartCoroutine(Open());
        }
    }
}
