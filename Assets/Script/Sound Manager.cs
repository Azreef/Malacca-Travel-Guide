using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip clickSound;
    public AudioSource audioSource;

    private AudioClip infoSound;
    public void PlayClickSound()
    {
        audioSource.clip= clickSound;
        audioSource.Play();
        //clickSound.
    }


    //public void set

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
