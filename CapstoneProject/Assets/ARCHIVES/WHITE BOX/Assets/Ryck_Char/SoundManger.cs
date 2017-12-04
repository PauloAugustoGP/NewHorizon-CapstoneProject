using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManger : MonoBehaviour {
    static SoundManger _instance = null;
    /// <summary>
    /// 
    /// CHANGE THE SCENE NAMES TO ACT ACCORDINGLY
    /// 
    /// </summary>
    //Music goes here
    public AudioClip mainTheme;
    public AudioClip lvlTheme;

    //Sound goes here
    //Character Audio
    public AudioClip tele;
    public AudioClip fire;
    public AudioClip death;
    public AudioClip jump;

    //World Audio
    //Here is where you can call sounds for the world
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;


    //Source
    public AudioSource music;
    public AudioSource SFX;
    // Use this for initialization
    void Start () {

        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(this);
        }

		MusicCaller("MenuMusic");
	}
	
	// Update is called once per frame
	void Update () {
		/*
        if (SceneManager.GetActiveScene().name != "CharMov_TEST")
        {
            music.volume = 0.3f;
        }

        if (SceneManager.GetActiveScene().name == "")
        {
            music.Stop();
        }

        if (SceneManager.GetActiveScene().name == "" && !music.isPlaying)
        {
            music.Play();
        }

        if (SceneManager.GetActiveScene().name == "")
        {
            if (music.isPlaying)
            {

            }
        }
        */
	}

    public static SoundManger instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

	///<summary>
	///<para>Give the name of the clip to be played (Teleport, Fire, Death, Jump).</para>
	///</summary>
	public void SoundCaller(string clipName, float volume = 1.0f)
	{
		switch (clipName)
		{
		case "Teleport":
			playSingleSound (tele, volume);

			break;
		case "Fire":
			playSingleSound (fire, volume);

			break;
		case "Death":
			playSingleSound (death, volume);

			break;
		case "Jump":
			playSingleSound (jump, volume);

			break;
		}
	}

	///<summary>
	///<para>Give the name of the clip to be played (MainTheme, LevelMusic).</para>
	///</summary>
	public void MusicCaller(string clipName, float volume = 1.0f)
	{
		switch (clipName)
		{
		case "MainTheme":
			playMusic(mainTheme, volume);

			break;
		case "LevelMusic":
			playMusic(lvlTheme, volume);

			break;
		}
	}

	private void playSingleSound(AudioClip clip, float volume = 1.0f)
    {
        // Assign volume to AudioSource volume
        SFX.volume = volume;

        // Assign AudioClip to AudioSource clip
        SFX.clip = clip;

        // Play assigned AudioClip through AudioSource on SoundManager
        SFX.Play();
    }

	private void playMusic(AudioClip clip, float volume = 1.0f)
    {
        // Assign volume to AudioSource volume
        music.volume = volume;

        // Assign AudioClip to AudioSource clip
        music.clip = clip;

        // Play assigned AudioClip through AudioSource on SoundManager
        music.Play();
    }
}
