using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the KeyManager and call its CollectKey method
            KeyManager keyManager = FindObjectOfType<KeyManager>();
            keyManager.CollectKey(gameObject);
        }
    }
}
