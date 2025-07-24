using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip clickSound;
    public AudioClip arrivedSound;
    public AudioClip exitSound;

    public AudioSource audioSource;

    private AudioClip infoSound;
    public void PlayClickSound()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
     
    }

    public void SetInfoSound(AudioClip soundClip)
    {
        
        if(soundClip != null)
        {
            infoSound = soundClip;
        }
        else
        {
            Debug.LogError("ERROR: Audio Not Found");
        }
       
    }

    public void PlayInfoSound()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.clip = infoSound;
            audioSource.Play();
        }

    }

    public void PlayArrivingSound()
    {
        audioSource.clip = arrivedSound;
        audioSource.Play();
    }

    public void PlayExitSound()
    {
        audioSource.clip = exitSound;
        audioSource.Play();
    }

}
