using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the portal is the player
        if (other.CompareTag("Player"))
        {
            // Hide the player when they enter the portal
            other.gameObject.SetActive(false);

            // Call the method to show the win message
            WinGame();
        }
    }

    private void WinGame()
    {
        // Display the "You Win!" message in the console
        Debug.Log("You Win!");

        // Optionally, you can add additional functionality here, 
        // such as loading a new scene or showing a UI victory screen
    }
}
