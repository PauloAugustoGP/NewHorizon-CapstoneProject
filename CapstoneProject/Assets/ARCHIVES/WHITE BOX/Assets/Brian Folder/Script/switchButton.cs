using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{

    [SerializeField]
    private AudioClip OpenedSFX;
    [SerializeField]
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    

    
    
    
        void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<Projectile_ObjectScript>())
        {
            //PlaySound(OpenedSFX);
            Debug.Log("Door Opened!");
        }

            
    }
    


    void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        //Assign volume to AudioSource volume
        audioSource.volume = volume;

        // Tell AudioSource what clip to play
        audioSource.clip = clip;

        // Tell AudioSource to play sound
        audioSource.Play();
    }

}
