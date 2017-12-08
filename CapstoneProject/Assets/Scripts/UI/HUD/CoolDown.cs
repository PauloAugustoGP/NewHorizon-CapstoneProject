using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour {
    [Tooltip("CoolDown progress bar reference.")]
    public Image coolDown;
    [Tooltip("If coolDOwn is stopped.")]
    public bool stop;
    [Tooltip("Is the game paused?")]
    public bool paused;
    [Tooltip("The cooldown time.")]
    public float coolDownTime;
    [Tooltip("If the cooldown is triggered.")]
    public bool isCoolingDown;
    [Tooltip("Use a script value.")]
    public bool useScriptValue;
    [Tooltip("The color to display when the cooldown has not been triggered.")]
    public Color cooledDownColor = Color.yellow;
    [Tooltip("The color to display when the cooldown has been triggered.")]
    public Color coolingDownColor = Color.red;
    [Tooltip("The projectile script with a '.coolDownTime' variable")]
    public ProjectilePlayer projectilePlayerScript;
    [Tooltip("Disable logging for this script")]
    public bool disableLogging;


    void Start() {
        stop = true;
        if (!projectilePlayerScript) {
            projectilePlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ProjectilePlayer>();
        }
        if (!coolDown) {
            Log("Error:  Missing coolDown image gameobject.", "error");
        }
        if (useScriptValue) {
            coolDownTime = projectilePlayerScript.coolDownTime;
        }
        if (coolDownTime <= 0) {
            coolDownTime = 2f;

            Log("Warning coolDownTime not set, defaulting to " + coolDownTime, "warning");
        }
        coolDown.color = cooledDownColor;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            StartCoolDown(coolDownTime);
        }
        if (Time.timeScale == 0) {
            paused = true;
        } else {
            paused = false;
            if (stop) {
                return;
            }
            coolDown.fillAmount -= 1f / coolDownTime * Time.deltaTime;

        }
        if (isCoolingDown == false) {
            if (coolDown.color != cooledDownColor) {
                coolDown.color = cooledDownColor;
            }
        }
        CheckCoolDown();
    }

    public void StartCoolDown(float start) {
        stop = false;
        coolDownTime = start;
        isCoolingDown = true;
        if (coolDown.color != coolingDownColor) {
            coolDown.color = coolingDownColor;
        }
        //Update();
        Log("Initiated cooldown.", "default");
    }

    public void ForceEnd() {
        stop = true;
        coolDownTime = 0;
        coolDown.fillAmount = 1;
        coolDown.color = cooledDownColor;
        Log("Cooldown has been force ended.", "default");
    }

    public void TogglePause() {
        if (Time.timeScale == 0) {
            paused = true;
        } else {
            paused = false;
        }
        Log("Paused: " + paused, "default");
    }

    public void CheckCoolDown() {
        if (coolDown.fillAmount == 0 && isCoolingDown) {
            ResetCoolDown();
        }
    }

    public void ResetCoolDown() {
        coolDown.fillAmount = 1;
        coolDown.color = cooledDownColor;
        isCoolingDown = false;
        stop = true;
        Log("Cooldown has been reset.", "default");
    }

    public void Log(string value, string type = "default") {
        if (!disableLogging) {
            //if(type != null) {
            switch (type) {
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