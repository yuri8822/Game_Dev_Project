using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour
{
    public TextMeshProUGUI keyCountText; // Text for displaying key count
    public GameObject portal;           // Reference to the portal GameObject

    private int keysCollected = 0;      // Counter for collected keys
    private int totalKeys;              // Total number of keys

    private void Start()
    {
        // Count all key objects at the start
        totalKeys = GameObject.FindGameObjectsWithTag("key").Length;
        UpdateKeyText();

        // Ensure the portal is hidden at the start
        if (portal != null)
            portal.SetActive(false);
    }

    public void CollectKey(GameObject key)
    {
        keysCollected++;
        UpdateKeyText();
        Destroy(key); // Remove the key from the scene

        if (keysCollected == totalKeys)
        {
            Debug.Log("All keys collected! Portal activated.");
            ActivatePortal();
        }
    }

    private void UpdateKeyText()
    {
        keyCountText.text = "Keys: " + keysCollected + "/" + totalKeys;
    }

    private void ActivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(true); // Show the portal
            Debug.Log("Portal is now visible.");
        }
        else
        {
            Debug.LogError("Portal GameObject is not assigned in KeyManager!");
        }
    }
}
