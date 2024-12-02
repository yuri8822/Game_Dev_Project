using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject youWinPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlayPortalSound();  
            }

            other.gameObject.SetActive(false);  
            youWinPanel.SetActive(true);        
        }
    }

    public void OnContinueButtonPressed()
    {
        SceneManager.LoadScene("Level 2");
    }
}
