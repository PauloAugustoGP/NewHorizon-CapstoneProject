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
    public bool switchTriggered;
    [SerializeField] SwitchButton sb;

    void Start() {
        status = false;
        originalPosition = transform.position;
        //player = GameObject.Find("Player").transform;
        if (moveDistance == Vector3.zero) {
            moveDistance = new Vector3(move, transform.position.y, 0);
        }
        if(!sb) {
            sb = GameObject.FindObjectOfType<SwitchButton>();
        }
    }

    void Update() {
        switchTriggered = sb.switchTriggered;
        //if (Input.GetKeyDown(KeyCode.F)) {
            //if (Vector3.Distance(player.position, this.transform.position) < 5f) {
                //if (status) {
                // trigger exit
                //    StartCoroutine(moveDoor(originalPosition));
                //} else {
                // trigger enter
                //    Vector3 moveTo = originalPosition + moveDistance;
                //    StartCoroutine(moveDoor(moveTo));
                //}
            //}
        //}
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (switchTriggered) {
                Vector3 moveTo = originalPosition + moveDistance;
                StartCoroutine(moveDoor(moveTo));
            }
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(moveDoor(originalPosition));
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

    public void toogleDoorState() {
        if (!status) {
            Vector3 moveTo = originalPosition + moveDistance;
            StartCoroutine(moveDoor(moveTo));
        }
    }
}
