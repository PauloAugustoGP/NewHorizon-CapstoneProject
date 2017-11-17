using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour {

    public Image coolDown;
    public float timeRemaining;
    public Projectile_PlayerScript projectilePlayerScript;
    // Use this for initialization
    void Start () {
        //projectilePlayerScript = GameObject.FindObjectOfType<Projectile_PlayerScript>();
        coolDown.fillAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //timeRemaining = projectilePlayerScript.coolDownTime;
    }

    public void updateProgressBar() {
        coolDown.fillAmount = timeRemaining;
    }
}
