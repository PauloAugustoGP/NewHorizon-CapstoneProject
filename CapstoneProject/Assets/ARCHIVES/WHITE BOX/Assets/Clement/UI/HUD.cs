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

	public Text broadcast;
	public string bcMessage;
	public Color bcColor = Color.white;
	public int bcDelay = 5;

	public RectTransform healthBar;
	public RectTransform healthBarBg;
	public Text healthText;

	public static int maxHealth = 100;
    public float currentHealth;
	public float testHealth = maxHealth;

	public bool startFlash = false;

	void Awake() {
		if(!player) {
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
		}
		CharacterBehaviour.Health = 100;
        currentHealth = CharacterBehaviour.Health;
		timer = GameObject.Find("Timer").GetComponent<Text>();
		score = GameObject.Find("Score").GetComponent<Text>();
		flashScreen = GameObject.Find("DamageIndicator").GetComponent<Image>();
		healthText = GameObject.Find("healthText").GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		if(flashSpeed <= 0) {
			flashSpeed = 3;
		}
		if(startTime == 0) {
			startTime = Time.time;
			countDown = false;
		} else {
			countDown = true;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		SetTimer();
		if(Time.timeScale == 0) {
			pauseTime = true;
		} else {
			pauseTime = false;
		}
		currentTime = (minutes * 60) + seconds;
		// Debug.Log(currentTime);
		// fixHealthBar();
		fixTestHealthBar();

		if(startFlash) {
			flashScreen.color = Color.Lerp(flashColor, clearColor, Time.deltaTime * flashSpeed);
			StartCoroutine(damageFlash(1f));
		}

        currentHealth = CharacterBehaviour.Health;
        detectHealthChange();
	}
	
	public float getHealth() {
		Debug.Log(CharacterBehaviour.Health);
		return CharacterBehaviour.Health;
	}

	public float getTestHealth() {
		Debug.Log(testHealth);
		return testHealth;
	}

	IEnumerator damageFlash(float delay) {
		// fScreen.color = Color.Lerp(fColor, clearColor, Mathf.PingPong(Time.time, fSpeed));
		yield return new WaitForSeconds(delay);
		startFlash = false;
		flashScreen.color = clearColor;
    }

    public void detectHealthChange() {
        if(currentHealth < CharacterBehaviour.Health) {
            TakeDamage();
        }
    }

	public void TakeDamage(float amount) {
		// CharacterBehaviour.Health -= amount;
		startFlash = true;
		if(CharacterBehaviour.Health >= 100) {
			CharacterBehaviour.Health = 100;
		} else if(CharacterBehaviour.Health <= 0) {
			CharacterBehaviour.Health = 0;
		}
		fixHealthBar();
		Debug.Log("You've been hurt");
		Debug.Log("Player took " + amount + " damage. Heath: " + CharacterBehaviour.Health);
	}

    public void TakeDamage() {
        startFlash = true;
		if(CharacterBehaviour.Health >= 100) {
			CharacterBehaviour.Health = 100;
		} else if(CharacterBehaviour.Health <= 0) {
			CharacterBehaviour.Health = 0;
		}
        fixHealthBar();
        float newHealth = maxHealth - currentHealth;
        Debug.Log("You've been hurt. You took " + newHealth + " damage.");
    }

	public void TakeTestDamage(float amount) {
		testHealth -= amount;
		startFlash = true;
		if(testHealth >= 100) {
			testHealth = 100;
		} else if(testHealth <= 0) {
			testHealth = 0;
			Destroy(player);
		}
		fixTestHealthBar();
		Debug.Log("You've been hurt");
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
		float percentHealth = (CharacterBehaviour.Health /  maxHealth) * 100;
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