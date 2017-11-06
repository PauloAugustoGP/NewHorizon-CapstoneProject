using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public static MusicManager _instance = null;

    public AudioSource musicSource;
    public AudioSource SoundFX;
    public AudioClip song1;
    public AudioClip startScreenSong;
    public AudioClip LoseSong;
    public AudioClip WinSong;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            //add delegate to sceneLoaded for switching scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void PlaySong1()
    {
        if (musicSource)
        {
            musicSource.PlayOneShot(song1);
        }
    }
    public void PlaySoundEffects(AudioClip SoundFeX)
    {
        SoundFX.PlayOneShot(SoundFeX);
    }
    public void PlaySong(AudioClip ac)
    {
        musicSource.Stop();
        musicSource.PlayOneShot(ac);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Start")
        {
            musicSource.Stop();
            musicSource.PlayOneShot(startScreenSong);

        }
        else if (scene.name == "Finish")
        {
            musicSource.Stop();
            musicSource.PlayOneShot(LoseSong);
        }
        else if (scene.name == "PlayerWinsScene")
        {
            musicSource.Stop();
            musicSource.PlayOneShot(WinSong);
        }
    }
    /// <summary>
    /// Destorys objects and removes any delegates it has passed
    /// </summary>
    //tells anything calling it that its dead.
    private void Kill()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
