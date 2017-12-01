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
    public bool disableLogging;


    void Start() {
        stop = true;
        if(!projectilePlayerScript) {
            projectilePlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Projectile_PlayerScript>();
        }
        if(!coolDown) {
            Log("Error:  Missing coolDown image gameobject.", "error");
        }
        if(useScriptValue) {
            coolDownTime = projectilePlayerScript.coolDownTime;
        }
        if(coolDownTime <= 0) {
            coolDownTime = 2f;
            Log("Warning coolDownTime not set, defaulting to " + coolDownTime, "warning");
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.N)) {
            StartCoolDown(coolDownTime);
        }
        if (Time.timeScale == 0) {
            paused = true;
        } else {
            paused = false;
            if(stop) {
                return;
            }
            isCoolingDown = true;
            if(coolDown.color != coolingDownColor) {
                coolDown.color = coolingDownColor;
            }
            coolDown.fillAmount -= 1f / coolDownTime * Time.deltaTime;
            
        }
        if(isCoolingDown == false) {
            if(coolDown.color != cooledDownColor) {
                coolDown.color = cooledDownColor;
            }
        }
        CheckCoolDown();
    }

    public void StartCoolDown(float start) {
        stop = false;
        coolDownTime = start;
        Update();
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
        Log("Paused: " + paused);
    }

    public void CheckCoolDown() {
        if(coolDown.fillAmount == 0 && isCoolingDown) {
            ResetCoolDown();
        }
    }

    public void ResetCoolDown() {
        coolDown.fillAmount = 1;
        coolDown.color = cooledDownColor;
        isCoolingDown = false;
        stop = true;
    }

    public void Log(string value, string type = "default") {
        if(disableLogging) {
            //if(type != null) {
                switch(type) {
                    case "Error":
                    case "error":
                    case "err":
                    case "Err":
                        Debug.LogError("[CoolDown] " + value);
                        break;
                    case "Warning":
                    case "warning":
                    case "Warn":
                    case "warn":
                        Debug.LogWarning("[CoolDown] " + value);
                        break;
                    case "default":
                    default:
                        Debug.Log("[CoolDown] " + value);
                        break;
                }
            //}
        }
    }
}