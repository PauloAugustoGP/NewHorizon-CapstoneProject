using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchButton : MonoBehaviour
{
    //Audio
    public AudioClip OpenedSFX;

    public AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 1.0f;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision c)
    {
        if (c.other.CompareTag("Projectile"))
        {
            playSound(OpenedSFX);
            Debug.Log("Door Opened!");
        }

    }

    // Function used to set clip to play through AudioSource attached to Character
    void playSound(AudioClip clip, float volume = 1.0f)
    {
        //Assign volume to AudioSource volume
        audioSource.volume = volume;

        // Tell AudioSource what clip to play
        audioSource.clip = clip;

        // Tell AudioSource to play sound
        audioSource.Play();
    }

}
