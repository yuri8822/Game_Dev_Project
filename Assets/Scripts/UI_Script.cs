using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject Map;
    public GameObject Settings;
    public AudioSource AudioSource;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        Map.SetActive(false);
        Settings.SetActive(false);

        if (AudioSource != null)
        {
            AudioSource.volume = PlayerPrefs.GetFloat("Volume", 1.0f); // audio source volume set to value retrieved from player prefs
            AudioSource.mute = PlayerPrefs.GetInt("Mute", 0) == 1; // audio source mute set to value retrieved from player prefs
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f); // volume slider value set to value retrieved from player prefs
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueButtonClicked()
    {
        int index = PlayerPrefs.GetInt("currentLevel", 1);
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level " + levelNumber.ToString());
    }
    
    public void SelectLevelClicked()
    {
        MainMenu.SetActive(false);
        Map.SetActive(true);
    }

    public void MapBackClicked()
    {
        MainMenu.SetActive(true);
        Map.SetActive(false);
    }

    public void SettingsClicked()
    {
        MainMenu.SetActive(false);
        Settings.SetActive(true);
    }

    public void SettingsBackClicked()
    {
        MainMenu.SetActive(true);
        Settings.SetActive(false);
    }

    public void MuteAudioClicked()
    {
        if (AudioSource != null)
        {
            AudioSource.mute = !AudioSource.mute;
            PlayerPrefs.SetInt("Mute", AudioSource.mute ? 1 : 0);
        }
    }

    public void AudioSliderChanged(float volume)
    {
        if (AudioSource != null)
        {
            AudioSource.volume = volume;
            PlayerPrefs.SetFloat("Volume", volume);
        }
    }

    public void QuitToDesktopClicked()
    {
        Application.Quit();
    }
}
