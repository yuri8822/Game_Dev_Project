using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlayKeyCollectSound();  
            }

            
            KeyManager keyManager = FindObjectOfType<KeyManager>();
            keyManager.CollectKey(gameObject);
        }
    }
}
