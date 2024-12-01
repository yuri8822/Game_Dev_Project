using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_lvl_Script : MonoBehaviour
{

    public GameObject Pause;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Pause.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseClicked()
    {
        Pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeClicked()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartClicked()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitClicked()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void MuteAudioClicked()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }
    }

    public void OnVolumeChanged(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
