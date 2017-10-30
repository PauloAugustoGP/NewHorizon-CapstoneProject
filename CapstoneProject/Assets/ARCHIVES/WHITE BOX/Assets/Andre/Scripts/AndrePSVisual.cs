using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrePSVisual : MonoBehaviour
{

    private ParticleSystem _ps;

    private float _temp;
    private int _count;

    // Use this for initialization
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        _ps.Emit(_count);
    }

    // Update is called once per frame
    public void UpdateSize(float pDeltaC)
    {
        _ps.startSize += (0.1f * pDeltaC);
        _temp += (0.5f * pDeltaC);
        _count = (int)_temp;
    }
}