using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioSource effectsAudio;

    [SerializeField] private Button muteButton; 
    [SerializeField] private Slider volumeSlider; 

    private List<AudioSource> allAudioSources = new List<AudioSource>();
    private bool isMuted = false;

    private void Start()
    {
     
        allAudioSources.Add(backgroundAudio);
        allAudioSources.Add(playerAudio);
        allAudioSources.Add(effectsAudio);

    
        isMuted = PlayerPrefs.GetInt("MuteAudio", 0) == 1;
        float savedVolume = PlayerPrefs.GetFloat("AudioVolume", 1f);

      
        volumeSlider.value = savedVolume;
        ApplyVolumeSettings(savedVolume, isMuted);

       
        muteButton.onClick.AddListener(ToggleMute);

       
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }

   public void ToggleMute()
    {
        isMuted = !isMuted; 

        foreach (var audioSource in allAudioSources)
        {
            audioSource.mute = isMuted;
        }

       
        PlayerPrefs.SetInt("MuteAudio", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void AdjustVolume(float volume)
    {
        if (!isMuted) 
        {
            foreach (var audioSource in allAudioSources)
            {
                audioSource.volume = volume;
            }
        }

       
        PlayerPrefs.SetFloat("AudioVolume", volume);
        PlayerPrefs.Save();
    }

    public void ApplyVolumeSettings(float volume, bool mute)
    {
        foreach (var audioSource in allAudioSources)
        {
            audioSource.mute = mute;
            audioSource.volume = volume;
        }
    }
}
