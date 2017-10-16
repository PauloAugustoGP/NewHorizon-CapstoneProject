﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud1 : MonoBehaviour {
    // currently selected player;
    charControlScript character;
    public GameObject player;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float flashSpeed = 5f;
    public Image flashScreen;
    // timer text;
    [SerializeField]
    Text timer;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text broadcast;
    [SerializeField]
    bool continuousBroadcast = false;
    [SerializeField]
    String broadcastMessage = "Hi, this is a test";
    [SerializeField]
    Color broadcastColor = Color.white;
    [SerializeField]
    int broadcastDelay = 5;
    [SerializeField]
    int minutes;
    [SerializeField]
    int seconds;
    [SerializeField]
    float startTime;
    [SerializeField]
    bool stopTime = false;
    [SerializeField]
    int currentTime;
    [SerializeField]
    bool countDown = false;

    int mval;
    public RectTransform healthBar;
    public RectTransform healthBarBg;
    public float phealth;
    // Update is called once per frame
    void Start() {
        // if start time is not set, set count down to false and set start time to Time.time else set countdown to false;
        if (startTime == 0) {
            startTime = Time.time;
            countDown = false;
        } else {
            countDown = true;
        }

        // auto detect GameObjects;
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        broadcast = GameObject.FindGameObjectWithTag("Broadcast").GetComponent<Text>();
        flashScreen = GameObject.Find("flash").GetComponent<Image>();
        character = player.GetComponent<charControlScript>();
        phealth = character.PlayerHealth;
    }

    void Update() {
        // call the set timer function;
        setTimer();
        if (Time.time == 0)
            stopTime = true;
        else
            stopTime = false;
        /* IMPORTANT;
		* To call the broadcast/shout function;
		* Always call it inside of the StartCoroutine function as shown below;
		*/
        // This is glitchy;
        //if (continuousBroadcast) {
        //    StartCoroutine(shout(broadcastColor, broadcastMessage, broadcastDelay));
        //}
        currentTime = (minutes * 60) + seconds;
        //Debug.Log(currentTime);
        fixHealthBar();
    }

    public void fixHealthBar() {
        float newHealth = calculateHealth();
        healthBar.sizeDelta = new Vector3(newHealth, healthBar.sizeDelta.y);
    }

    public float calculateHealth() {
        float maxHealth = 1;
        //if(character.maxHealth == null) {
        //    maxHealth = 1;
        //} else {
        //    maxHealth = character.maxHealth;
        //}
        float percentHealth = (maxHealth - phealth) * 100;
        float width = percentHealth / 100;
        width = healthBarBg.sizeDelta.x - width;
        //Debug.Log("Calculated health: " + width);
        return width;
    }

    void setTimer() {
        // if stop time is set to false configure the timer else pause the timer and set the color to yellow;
        if (!stopTime) {
            timer.color = Color.white;
            float t;
            // calculate the currentTime when counting down or up and set t to that value;
            if (countDown == false) {
                t = Time.time - startTime;
            } else {
                t = startTime - Time.time;
            }

            // calculate minutes and seconds;
            minutes = ((int)t / 60);
            string min = minutes.ToString();
            seconds = ((int)t % 60);
            string sec = seconds.ToString("f0");
            // change seconds when it's equal to 60;
            if (sec == "60") {
                sec = "59";
                int.TryParse(min, out mval);
                mval += 1;
            }
            // if seconds is a single digit, then add a 0 to the front of it;
            if (sec.Length == 1) {
                sec = "0" + sec;
            }
            // set the minutes and seconds string to value;
            string value = min + ":" + sec;
            // call the setText function;
            setText(timer, value);
        } else {
            timer.color = Color.yellow;
        }
    }

    void setText(Text obj, String value) {
        //Debug.Log("Set Text: " + value);
        // set the text value of the current object with a text component to the specified value;
        obj.text = value;
    }

    void setColor(Text obj, Color value) {
        //Debug.Log("Set Color: " + value);
        // set the text value of the current object with a text component to the specified value;
        obj.color = value;
    }

    public void flashDamage(Color color, float fspeed) {
        flashColour = color;
        if (fspeed != 0)
            flashSpeed = fspeed;
        flashScreen.color = Color.Lerp(flashColour, Color.clear, flashSpeed * Time.deltaTime);
    }

    IEnumerator shout(Color color, String text = "Broadcast Text", int delay = 3) {
        setText(broadcast, text);
        setColor(broadcast, color);
        yield return new WaitForSeconds(delay);
        setText(broadcast, null);

    }
}