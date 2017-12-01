using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound_Manager : MonoBehaviour {

    static Sound_Manager _instance = null;

    /*[SerializeField] private AudioMixer _masterMixer;

    private const string MUSIC_VOL = "MusicVol";
    private const string VOICE_VOL = "VoiceVol";
    private const string SOUND_VOL = "SoundVol";
    */

    [SerializeField] private AudioSource MusicSource;
    //[SerializeField] private AudioSource VoiceSource;
    [SerializeField] private AudioSource SoundSource;

    [Header("Music")]
    [SerializeField] private AudioClip MenuMusic;
    [SerializeField] private AudioClip LevelMusic;
    [SerializeField] private AudioClip GameOverMusic;
    [SerializeField] private AudioClip WinningMusic;
    
    [SerializeField] private AudioClip[] Powers;
    [SerializeField] private AudioClip[] Guards;
    [SerializeField] private AudioClip[] Vanish;
    [SerializeField] private AudioClip[] Aura;
    [SerializeField] private AudioClip[] Quantius;

    /*[Header("Powers")]
    [SerializeField] private AudioClip ProjectileCharging;
    [SerializeField] private AudioClip ProjectileFire;
    [SerializeField] private AudioClip ProjectileHit;
    [SerializeField] private AudioClip Teleport;
    [SerializeField] private AudioClip Melee;*/

    /*[Header("Guards")]
    [SerializeField] private AudioClip GuardsBanter1;
    [SerializeField] private AudioClip GuardsBanter2;
    [SerializeField] private AudioClip GuardsBanter3;
    [SerializeField] private AudioClip GuardsBanter4;
    [SerializeField] private AudioClip GuardsBanter5;

    [Header("Vanish")]
    [SerializeField] private AudioClip VanishTalk1;
    [SerializeField] private AudioClip VanishTalk2;
    [SerializeField] private AudioClip VanishTalk3;
    [SerializeField] private AudioClip VanishTalk4;
    [SerializeField] private AudioClip VanishTalk5;

    [Header("Aura")]
    [SerializeField] private AudioClip AuraTalk1;
    [SerializeField] private AudioClip AuraTalk2;
    [SerializeField] private AudioClip AuraTalk3;
    [SerializeField] private AudioClip AuraTalk4;
    [SerializeField] private AudioClip AuraTalk5;

    [Header("Quantius")]
    [SerializeField] private AudioClip QuantiusTalk1;
    [SerializeField] private AudioClip QuantiusTalk2;
    [SerializeField] private AudioClip QuantiusTalk3;
    [SerializeField] private AudioClip QuantiusTalk4;
    [SerializeField] private AudioClip QuantiusTalk5;*/


    // Use this for initialization
    void Start ()
    {
        if (instance)
            DestroyImmediate(gameObject); // destroys the new Game_Manager upon scenes being loaded and keeps the old one
        else
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
    }

// Update is called once per frame
void Update ()
    {
		
	}

    /*public void SetMusicVolume(float pMusicVol)
    {

        _masterMixer.SetFloat(MUSIC_VOL, pMusicVol);
    }

    public void SetVoiceVolume(float pVoiceVol)
    {

        _masterMixer.SetFloat(VOICE_VOL, pVoiceVol);
    }

    public void SetSoundVolume(float pSoundVol)
    {

        _masterMixer.SetFloat(SOUND_VOL, pSoundVol);
    }*/

    public void MusicCaller(string clipName, float volume = 1.0f)
    {
        switch (clipName)
        {
            case "MenuMusic":
                playMusic(MenuMusic, volume);

                break;
            case "LevelMusic":
                playMusic(LevelMusic, volume);

                break;
            case "WinningMusic":
                playMusic(WinningMusic, volume);

                break;
            case "gameOverMusic":
                playMusic(GameOverMusic, volume);

                break;
        }
    }

    public void PowerSound()
    {
        
    }

    private void playSound(AudioClip clip, float volume = 1.0f)
    {
        // Assign volume to AudioSource volume
        SoundSource.volume = volume;

        // Assign AudioClip to AudioSource clip
        SoundSource.clip = clip;

        // Play assigned AudioClip through AudioSource on SoundManager
        SoundSource.Play();
    }

    private void playMusic(AudioClip clip, float volume = 1.0f)
    {
        // Assign volume to AudioSource volume
        MusicSource.volume = volume;

        // Assign AudioClip to AudioSource clip
        MusicSource.clip = clip;

        // Play assigned AudioClip through AudioSource on SoundManager
        MusicSource.Play();
    }

    public static Sound_Manager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}
