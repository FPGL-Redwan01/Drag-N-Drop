using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager sharedInstance = null;

    public AudioSource sfxAuidoSource;
    [SerializeField] private AudioSource backgroundAudioSource;
    public bool soundOn;
    //=========================================

    public AudioClip fanSetSFX , popSFX , levelComSFX;

    public static SoundManager SharedManager()
    {
        return sharedInstance;
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
  
    void Start()
    {
        soundOn = true;
        sfxAuidoSource = GetComponent<AudioSource>();

        //backgroundAudioSource =  GetComponent<AudioSource>();

        //if (backgroundAudioSource != null)
     
    }
    public void PlaySFX(AudioClip audioClip)
    {
        sfxAuidoSource.PlayOneShot(audioClip);
    }



}
