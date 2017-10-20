using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour {
    public float damagePower;
    public float speed;
    public GameObject target;
    public float health = 100f;
    public float phealth;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        phealth = target.GetComponent<charControlScript>().PlayerHealth;
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
            h.TakeDamage(damagePower);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.CompareTag("Player") && phealth > 0f) {
            HUD h = GameObject.Find("HUD").GetComponent<HUD>();
            h.TakeDamage(damagePower);
        }
    }
}