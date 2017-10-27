﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [Tooltip("Door open angle.")]
    public float openAngle = 90.0f;
    [Tooltip("Door close angle.")]
    public float closeAngle = 0.0f;
    [Tooltip("Door animation speed.")]
    public float animSpeed = 2.0f;
    [Tooltip("Door open.")]
    private Quaternion open = Quaternion.identity;
    [Tooltip("Door close.")]
    private Quaternion close = Quaternion.identity;
    [Tooltip("Player.")]
    private Transform player = null;
    [Tooltip("Door status. Open = true, Closed = false")]
    public bool status = false; //false = close, true = open
    [Tooltip("Used with the coroutine.")]
    private bool doorGo = false;

    void Start() {
        status = false; // open
        open = Quaternion.Euler(0, openAngle, 0);
        close = Quaternion.Euler(0, closeAngle, 0);
        player = GameObject.Find("Player").transform;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && !doorGo) {
            //if (Vector3.Distance(player.position, this.transform.position) < 5f) {
                if (status) {
                    StartCoroutine(moveDoor(close));
                } else {
                    StartCoroutine(moveDoor(open));
                }
            //}
        }
    }
    public IEnumerator moveDoor(Quaternion dest) {
        doorGo = true;
        while (Quaternion.Angle(transform.localRotation, dest) > 4.0f) {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, dest, Time.deltaTime * animSpeed);
            // yield return new WaitForSeconds(3);
            yield return null;
        }
        status = !status;
        doorGo = false;
        // yield return new WaitForSeconds(3);
        yield return null;
    }
}
