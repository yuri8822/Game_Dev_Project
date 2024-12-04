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
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level 1":
                SceneManager.LoadScene("Level 2");
                break;
            case "Level 2":
                SceneManager.LoadScene("Level 3");
                break;
            case "Level 3":
                SceneManager.LoadScene("Level 4");
                break;
            case "Level 4":
                SceneManager.LoadScene("Level 5");
                break;
        }

    }
}
