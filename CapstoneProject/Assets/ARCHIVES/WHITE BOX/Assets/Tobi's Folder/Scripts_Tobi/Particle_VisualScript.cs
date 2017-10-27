using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_VisualScript : MonoBehaviour {

	private ParticleSystem _ps;

	// Use this for initialization
	void Start () 
	{
		_ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	public void UpdateSize (float pDeltaC) 
	{
		_ps.startSize += (0.1f * pDeltaC);
		//_ps.emission.rateOverTime.constant += (10f * pDeltaC);
	}
}
