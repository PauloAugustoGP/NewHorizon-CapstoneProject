using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Doors : MonoBehaviour {
    public enum type { SlidingVertical, SlidingNormnal }
    public Transform player;
    [Tooltip("Door status. Open = true, Closed = false")]
    public bool status = false;
    [Tooltip("Used with the coroutine.")]
    public bool doorGo = false;
    [Tooltip("If switch is enabled. Will be changed from the switchButton script")]
    public bool doorEnabled;
    public bool inTrigger;
    public float move = 3f;
    public Vector3 moveDistance;
    public float moveSpeed = 3f;
    public bool enableLogging;

    void Start() {
        status = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(move <= 0) {
            move = 1f;
        }
    }

    public virtual IEnumerator moveDoor(Quaternion dest) {
        yield return null;
    }
    //Brian's code
    public virtual void toggleDoorState() { }
    public virtual IEnumerator Open() {
        yield return null;
    }
    public virtual IEnumerator Close() {
        yield return null;
    }

    public virtual void Log (string value) {
        if(enableLogging) {
            Debug.Log(value);
        }
    }
}
