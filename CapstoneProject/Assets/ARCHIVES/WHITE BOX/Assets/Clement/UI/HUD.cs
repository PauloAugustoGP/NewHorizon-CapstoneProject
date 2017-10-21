using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public charControlScript player;
    public Color fColor = new Color(1f, 0f, 0f, 0.1f);
    public float fSpeed;
    public Image fScreen;

    public Text timer;
    public int minutes;
    public int seconds;
    public float startTime;
    public float currentTime;
    public bool pauseTime = false;
    public bool countDown = false;
    public int mval;

    public Text score;

    public Text broadcast;
    public string bcMessage;
    public Color bcColor = Color.white;
    public int bcDelay = 5;

    public RectTransform healthBar;
    public RectTransform healthBarBg;
    public Text healthText;

    public static int maxHealth = 100;
    public float testHealth = maxHealth;

    void Awake() {
        if(!player) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<charControlScript>();
        }
        player.PlayerHealth = 100;
        timer = GameObject.Find("Timer").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        fScreen = GameObject.Find("DamageIndicator").GetComponent<Image>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        if(fSpeed <= 0) {
            fSpeed = 3;
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
        SetTimer();
		if(Time.timeScale == 0) {
            pauseTime = true;
        } else {
            pauseTime = false;
        }
        currentTime = (minutes * 60) + seconds;
        Debug.Log(currentTime);
        // fixHealthBar();
        fixTestHealthBar();
    }
    
    public float getHealth() {
        Debug.Log(player.PlayerHealth);
        return player.PlayerHealth;
    }

    public float getTestHealth() {
        Debug.Log(testHealth);
        return testHealth;
    }

    public void damageFlash() {
        fColor = new Color(255, 0, 0, 0.3f);
        fScreen.color = Color.Lerp(fColor, Color.clear, fSpeed * Time.deltaTime);
        Debug.Log("You've been hurt");
    }

    public void TakeDamage(float amount) {
        player.PlayerHealth -= amount;
        damageFlash();
        if(player.PlayerHealth >= 100) {
            player.PlayerHealth = 100;
        } else if(player.PlayerHealth <= 0) {
            player.PlayerHealth = 0;
            Destroy(player);
        }
        fixHealthBar();
        Debug.Log("Player took " + amount + " damage. Heath: " + player.PlayerHealth);
    }

    public void TakeTestDamage(float amount) {
        testHealth -= amount;
        damageFlash();
        if(testHealth >= 100) {
            testHealth = 100;
        } else if(testHealth <= 0) {
            testHealth = 0;
            Destroy(player);
        }
        fixTestHealthBar();
        Debug.Log("Player took " + amount + " damage. Heath: " + testHealth);
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
        healthBar.sizeDelta =new Vector3(newHealth, healthBar.sizeDelta.y);
    }

    public void fixTestHealthBar() {
        float newHealth = CalculateTestHealth();
        healthBar.sizeDelta =new Vector3(newHealth, healthBar.sizeDelta.y);
    }

    public float CalculateHealth() {
        float percentHealth = (player.PlayerHealth /  maxHealth) * 100;
        Debug.Log(percentHealth);
        healthText.text = percentHealth.ToString();
        float width = (percentHealth / 100) * healthBarBg.sizeDelta.x;
        Debug.Log(width);
        return width;
    }

    public float CalculateTestHealth() {
        float percentHealth = (testHealth /  maxHealth) * 100;
        Debug.Log(percentHealth);
        healthText.text = percentHealth.ToString();
        float width = (percentHealth / 100) * healthBarBg.sizeDelta.x;
        Debug.Log(width);
        return width;
    }
}