using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour {
    public float damagePower;
    public float speed;
    public charControlScript target;
    public float health = 100f;
    public float phealth;
	// Use this for initialization
	void Start () {
        if(!target) {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<charControlScript>();
        }
        phealth = target.PlayerHealth;
        if(damagePower <= 0f) {
            damagePower = 3f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player") && phealth > 0f) {
            HUD h = GameObject.Find("HUD").GetComponent<HUD>();
            // h.TakeDamage(damagePower);
            h.TakeTestDamage(damagePower);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.CompareTag("Player") && phealth > 0f) {
            HUD h = GameObject.Find("HUD").GetComponent<HUD>();
            // h.TakeDamage(damagePower);
            h.TakeTestDamage(damagePower);
        }
    }
}