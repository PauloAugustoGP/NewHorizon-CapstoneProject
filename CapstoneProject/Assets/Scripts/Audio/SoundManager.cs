using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    static SoundManager _instance = null;

    private const string SFX_VOL = "sfxVol";
    private const string VOICE_VOL = "voiceVol";
    private const string MUSIC_VOL = "musicVol";
    private const string MASTER_VOL = "masterVol";

    public bool disableLogging;

    [Header("Audio Mixers")]
    [SerializeField]
    private AudioMixer _masterMixer;

    [Header("Volumne Sliders")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider voiceVolumeSlider;
    public Slider masterVolumeSlider;

    public List<Slider> sliders;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource VoiceSource;
    [SerializeField] private AudioSource SoundSource;

    [Header("Music")]
    [SerializeField] private AudioClip MenuMusic;
    [SerializeField] private AudioClip LevelMusic;
    [SerializeField] private AudioClip GameOverMusic;
    [SerializeField] private AudioClip WinningMusic;
    
    /*[SerializeField] private AudioClip[] Powers;
    [SerializeField] private AudioClip[] Guards;
    [SerializeField] private AudioClip[] Vanish;
    [SerializeField] private AudioClip[] Aura;
    [SerializeField] private AudioClip[] Quantius;*/

    [Header("Powers")]
    [SerializeField] private AudioClip ProjectileCharging;
    [SerializeField] private AudioClip ProjectileFire;
    [SerializeField] private AudioClip ProjectileHit;
    [SerializeField] private AudioClip Teleport;
    [SerializeField] private AudioClip Melee;

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

    public Canvas AudioPanel;
    public Slider[] sli;

    void Awake () {
        //if (instance) {
        //    DestroyImmediate(gameObject);
        //} else {
        //    instance = this;

        //    DontDestroyOnLoad(this);
        //}
        sli = AudioPanel.GetComponents<Slider>();
        for (int i = 0; i < sli.Length; i++) {
            sliders.Add(sli[i]);
        }
        if(sliders.Count <= 0) {
            Log("Sliders: " + sliders.Count);
        }
    }

    public void MusicCaller(string clipName) {
        switch (clipName) {
            case "MenuMusic":
                playMusic(MenuMusic);

                break;
            case "LevelMusic":
                playMusic(LevelMusic);

                break;
            case "WinningMusic":
                playMusic(WinningMusic);

                break;
            case "gameOverMusic":
                playMusic(GameOverMusic);
                break;
        }
    }

    public void PowerSound(string soundName) {
        switch (soundName) {
            case "ProjectileCharging":
                playSound(ProjectileCharging);
                break;
            case "ProjectileFire":
                playSound(ProjectileFire);
                break;
            case " ProjectileHit":
                playSound(ProjectileHit);
                break;
            case "Teleport":
                playSound(Teleport);
                break;
            case "Melee":
                playSound(Melee);
                break;
        }
    }
   
    private void playSound(AudioClip clip) {
        SoundSource.clip = clip;
        SoundSource.Play();
    }

    private void playMusic(AudioClip clip) {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void masterSliderChange() {
        //Log("" + masterVolumeSlider.value);
        SetMasterVolume(masterVolumeSlider.value);
    }

    public void sfxSliderChange() {
        //Log("" + sfxVolumeSlider.value);
        SetSFxVolume(sfxVolumeSlider.value);
    }

    public void musicSliderChange() {
        //Log("" + musicVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
    }

    public void voiceSliderChange() {
        //Log("" + voiceVolumeSlider.value);
        SetVoiceVolume(voiceVolumeSlider.value);
    }


    public void SetSFxVolume(float pSFxVol) {
        _masterMixer.SetFloat(SFX_VOL, pSFxVol);
    }

    public void SetMusicVolume(float pMusicVol) {
        _masterMixer.SetFloat(MUSIC_VOL, pMusicVol);
    }

    public void SetVoiceVolume(float pVoiceVol) {
        _masterMixer.SetFloat(VOICE_VOL, pVoiceVol);
    }

    public void SetMasterVolume(float pMasterVol) {
        _masterMixer.SetFloat(MASTER_VOL, pMasterVol);
    }

    public static SoundManager instance {
        get { return _instance; }
        set { _instance = value; }
    }

    public void ToggleMasterVol() {
        masterVolumeSlider.value = masterVolumeSlider.minValue;
    }

    public void ToggleMusicVol() {
        musicVolumeSlider.value = musicVolumeSlider.minValue;
    }

    public void ToggleSFxVol() {
        sfxVolumeSlider.value = sfxVolumeSlider.minValue;
    }

    public void ToggleVoiceVol() {
        voiceVolumeSlider.value = voiceVolumeSlider.minValue;
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