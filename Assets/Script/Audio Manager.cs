using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip clickSound;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
