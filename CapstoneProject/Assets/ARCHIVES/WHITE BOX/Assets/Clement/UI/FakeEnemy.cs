using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour {
    public float damagePower;
    public float speed;
    public CharacterBehaviour target;
    public float health = 100f;
    public float phealth;
	// Use this for initialization
	void Start () {
        if(!target) {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
        }
        if(damagePower <= 0f) {
            damagePower = 3f;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        phealth = CharacterBehaviour.Health;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player") && phealth > 0f) {
            HUD h = GameObject.Find("HUD").GetComponent<HUD>();
            phealth -= 20;
            h.TakeDamage();
            //phealth -= 20;
            //h.startFlash = true;
            //h.fixHealthBar();
        }
    }
}