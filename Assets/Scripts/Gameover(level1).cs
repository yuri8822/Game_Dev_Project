using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; 
    [SerializeField] private PlayerMechanics player; 

    private void Update()
    {
    
        if (player != null && player.GetType().GetField("playerHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(player).Equals(0))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Time.timeScale = 0; 
        gameOverPanel.SetActive(true);
    }


    public void RetryLevel()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }


    public void LoadMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Main Menu"); 
    }
}
