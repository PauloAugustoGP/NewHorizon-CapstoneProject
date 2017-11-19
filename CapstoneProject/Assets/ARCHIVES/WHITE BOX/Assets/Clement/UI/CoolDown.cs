using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour {

    public Image coolDown;
    public bool useScript;
    public float waitTime;
    public bool isCoolingDown;
    public Projectile_PlayerScript projectilePlayerScript;
    // Use this for initialization
    void Start () {
        if (!projectilePlayerScript) {
            // first gameobject with this script will be used.
            projectilePlayerScript = GameObject.FindObjectOfType<Projectile_PlayerScript>();
            // uses the gameobject that this script is on.
            //projectilePlayerScript = GetComponent<Projectile_PlayerScript>();
            waitTime = projectilePlayerScript.coolDownTime;
            if (!projectilePlayerScript) {
                Debug.Log("Could not find a script to use. Falling back to default method.");
                // fallback to use the values in this file as the cooldown;
                useScript = false;
            }
        }
        if (waitTime <= 0) {
            waitTime = 2f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.X)) {
        
        if (useScript) {
            waitTime = projectilePlayerScript.coolDownTime;
        }
        if (isCoolingDown) {
            coolDown.fillAmount -= 1f / waitTime * Time.deltaTime;
        }
    }
}
