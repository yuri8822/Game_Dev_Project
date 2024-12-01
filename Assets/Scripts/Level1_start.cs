using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_start : MonoBehaviour
{
    public GameObject storyPanel;

    private void Start()
    {
        // Pause the game when the panel is active
        storyPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void StartLevel()
    {
        // Resume the game and hide the panel
        storyPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
}
