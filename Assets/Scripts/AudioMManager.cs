using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioSource effectsAudio;

    [SerializeField] private Button muteButton; // Button to toggle mute
    [SerializeField] private Slider volumeSlider; // Slider to adjust the volume

    private List<AudioSource> allAudioSources = new List<AudioSource>();
    private bool isMuted = false; // Tracks the mute state

    private void Start()
    {
        // Add all audio sources to the list
        allAudioSources.Add(backgroundAudio);
        allAudioSources.Add(playerAudio);
        allAudioSources.Add(effectsAudio);

        // Load saved settings
        isMuted = PlayerPrefs.GetInt("MuteAudio", 0) == 1;
        float savedVolume = PlayerPrefs.GetFloat("AudioVolume", 1f);

        // Apply initial settings
        volumeSlider.value = savedVolume;
        ApplyVolumeSettings(savedVolume, isMuted);

        // Assign button click event
        muteButton.onClick.AddListener(ToggleMute);

        // Assign slider change event
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }

   public void ToggleMute()
    {
        isMuted = !isMuted; // Toggle the mute state

        foreach (var audioSource in allAudioSources)
        {
            audioSource.mute = isMuted;
        }

        // Save mute state
        PlayerPrefs.SetInt("MuteAudio", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void AdjustVolume(float volume)
    {
        if (!isMuted) // Only adjust volume if not muted
        {
            foreach (var audioSource in allAudioSources)
            {
                audioSource.volume = volume;
            }
        }

        // Save volume setting
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
