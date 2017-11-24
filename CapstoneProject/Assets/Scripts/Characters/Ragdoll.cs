using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {

    [SerializeField]
    Rigidbody[] rigidbodies;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public Rigidbody[] Bodies
    {
        get { return rigidbodies; }
    }
}
