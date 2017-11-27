using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour {

    public Image coolDown;
    public bool stop;
    public bool paused;
    public float coolDownTime;
    public bool isCoolingDown;
    public bool useScriptValue;
    public Color cooledDownColor = Color.yellow;
    public Color coolingDownColor = Color.red;
    public Projectile_PlayerScript projectilePlayerScript;


    void Start() {
        stop = true;
        if(!projectilePlayerScript) {
            projectilePlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Projectile_PlayerScript>();
        }
        if(!coolDown) {
            Debug.LogError("[CoolDown] Error:  Missing coolDown image gameobject.");
        }
        if(useScriptValue) {
            coolDownTime = projectilePlayerScript.coolDownTime;
        }
        if(coolDownTime <= 0) {
            coolDownTime = 2f;
            Debug.LogWarning("[CoolDoown] coolDownTime not set, defaulting to " + coolDownTime);
        }
    }

    void Update() {
        if (Time.timeScale == 0) {
            paused = true;
        } else {
            paused = false;
        }
        if(!paused) {
            if(stop) {
                return;
            }
            isCoolingDown = true;
            if(coolDown.color != coolingDownColor) {
                coolDown.color = coolingDownColor;
            }

            coolDown.fillAmount -= 1f / coolDownTime * Time.deltaTime;
        }
        if(coolDownTime == 0) {
            isCoolingDown = false;
            if(coolDown.color != cooledDownColor) {
                coolDown.color = cooledDownColor;
            }
        }
    }

    public void StartCoolDown(float start) {
        stop = false;
        coolDownTime = start;
    }

    public void ForceEnd() {
        stop = true;
        coolDownTime = 0;
        coolDown.fillAmount = 1;
        coolDown.color = cooledDownColor;
    }

    public void TogglePause() {
        if (Time.timeScale == 0) {
            paused = true;
        } else {
            paused = false;
        }
    }
}
