using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public GameObject player;
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
    public float phealth;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.Find("Timer").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        fScreen = GameObject.Find("DamageIndicator").GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
        phealth = getHealth();
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
        fixHealthBar();
    }
    
    public float getHealth() {
        return player.GetComponent<charControlScript>().PlayerHealth;
    }

    public void damageFlash() {
        fColor = Color.red;
        fScreen.color = Color.Lerp(fColor, Color.clear, fSpeed * Time.deltaTime);
    }

    public void TakeDamage(float amount) {
        phealth -= amount;
        damageFlash();
        if(phealth >= 100) {
            phealth = 100;
        } else if(phealth <= 0) {
            phealth = 0;
            Destroy(player);
        }
        fixHealthBar();
        Debug.Log("Player took " + amount + " damage. Heath: " + phealth);
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

    public float CalculateHealth() {
        float maxHealth = 100;
        float percentHealth = (maxHealth - getHealth()) * 100;
        float width = percentHealth / 100;
        width = healthBarBg.sizeDelta.x - width;
        return width;
    }
}
