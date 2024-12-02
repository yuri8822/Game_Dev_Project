using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioClip backgroundMusicClip;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource soundEffectsSource;
    [SerializeField] private AudioClip keyCollectSound;  
    [SerializeField] private AudioClip portalSound;      

    private void Start()
    {
  
        PlayBackgroundMusic();
    }


    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }


    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void PlayKeyCollectSound()
    {
        if (soundEffectsSource != null && keyCollectSound != null)
        {
            soundEffectsSource.PlayOneShot(keyCollectSound);
        }
    }


    public void PlayPortalSound()
    {
        if (soundEffectsSource != null && portalSound != null)
        {
            soundEffectsSource.PlayOneShot(portalSound);
        }
    }
}
