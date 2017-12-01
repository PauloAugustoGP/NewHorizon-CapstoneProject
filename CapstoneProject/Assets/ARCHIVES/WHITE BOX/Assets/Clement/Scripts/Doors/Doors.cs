using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Doors : MonoBehaviour {
    public enum type { SlidingVertical, SlidingNormnal }
    [SerializeField] private Transform player;
    [Tooltip("Door status. Open = true, Closed = false")]
    [SerializeField] protected bool status = false;
    [Tooltip("Used with the coroutine.")]
    [SerializeField] protected bool doorGo = false;
    [Tooltip("If switch is enabled. Will be changed from the switchButton script")]
    public bool doorEnabled;
    [SerializeField] protected bool inTrigger;
    [Tooltip("Force to move.")]
    [SerializeField] protected float move = 3f;
    [Tooltip("Handles direction.")]
    [SerializeField] protected float moveDistance;
    [Tooltip("Move speed.")]
    [SerializeField] protected float moveSpeed = 3f;

    void Start() {
        status = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
}
