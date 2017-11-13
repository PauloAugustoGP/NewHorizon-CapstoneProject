using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public CharacterBehaviour player;
    [Tooltip("Colour to flash the screen with when the player takes damage.")]
    public Color flashColor = new Color(1f, 0f, 0f, 0.3f);
    [Tooltip("The speed at which the flash color flashes. Increase for faster flashes")]
    public float flashSpeed;
    public Image flashScreen;
    Color clearColor = new Color(0, 0, 0, 0);

    public Text timer;
    public int minutes;
    public int seconds;
    public float startTime;
    public float currentTime;
    public bool pauseTime = false;
    public bool countDown = false;
    public int mval;

    public Text score;
    public bool disableLogging = false;
    public Text broadcast;
    public string bcMessage;
    public Color bcColor = Color.white;
    public int bcDelay = 5;

    public RectTransform healthBar;
    public RectTransform healthBarBg;
    public Text healthText;

    public static float maxHealth = 100;
    public static float currentHealth;
    public float testHealth = maxHealth;

    public bool startFlash = false;

    void Awake() {
        if(!player) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
        }
        timer = GameObject.Find("Timer").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        flashScreen = GameObject.Find("DamageIndicator").GetComponent<Image>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        if(flashSpeed <= 0) {
            flashSpeed = 40;
        }
        if(startTime == 0) {
            startTime = Time.time;
            countDown = false;
        } else {
            countDown = true;
        }
    }
    // Update is called once per frame
    void Update () {
        // currentHealth = CharacterBehaviour.Health;
        fixHealthBar();
        SetTimer();
        if(Time.timeScale == 0) {
            pauseTime = true;
        } else {
            pauseTime = false;
        }
        currentTime = (minutes * 60) + seconds;

        if(startFlash) {
            flashScreen.color = Color.Lerp(flashColor, clearColor, Time.deltaTime * flashSpeed);
            StartCoroutine(damageFlash(1f));
        }

    }
    
    public float getHealth() {
        return player.GetHealth();
    }

    IEnumerator damageFlash(float delay) {
        // fScreen.color = Color.Lerp(fColor, clearColor, Mathf.PingPong(Time.time, fSpeed));
        yield return new WaitForSeconds(delay);
        startFlash = false;
        flashScreen.color = clearColor;
    }

    public void TakeDamage() {
        startFlash = true;
        fixHealthBar();
        Log("You've been hurt.");
    }

    public void SetTimer() {
        if(!pauseTime) {
            timer.color = Color.white;
            float time;
            if (!countDown)
                time = Time.time - startTime;
            else
                time = startTime - Time.time;

            minutes = ((int)time / 60);
            string min = minutes.ToString();
            seconds = ((int)time % 60);
            string sec = seconds.ToString("f0");
            if(sec == "60") {
                sec = "59";
                int.TryParse(min, out mval);
                mval += 1;
            }

            if(sec.Length == 1) {
                sec = "0" + sec;
            }

            string value = min + ":" + sec;
            timer.text = value;

        } else {
            timer.color = Color.yellow;
        }
    }

    public void fixHealthBar() {
        float newHealth = CalculateHealth();
        Log("Newhealthbar width");
        healthBar.sizeDelta = new Vector3(newHealth, healthBar.sizeDelta.y);
        Log("HealthBar fixed");
    }

    public float CalculateHealth() {
        Log("Calc: " + currentHealth);
        Log("maxHealth: " + maxHealth);
        float health = getHealth();
        if (health >= 100) {
            health = 100;
        } else if (health <= 0) {
            health = 0;
        }
        float percentHealth = (health * 100) / maxHealth;
        Log(percentHealth);
        healthText.text = percentHealth.ToString();
        float width = (percentHealth * healthBarBg.sizeDelta.x) / 100;
        Log(width);
        Log("Health calculated");
        return width;
    }

    public void Log(string value) {
        if(!disableLogging) {
            Debug.Log("Hud Log: " + value);
        }
    }
    public void Log(int value) {
        if(!disableLogging) {
            Debug.Log("Hud Log: " + value);
        }
    }
    public void Log(float value) {
        if(!disableLogging) {
            Debug.Log("Hud Log: " + value);
        }
    }
}