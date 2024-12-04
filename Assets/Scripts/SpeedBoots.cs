using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoots : MonoBehaviour
{
    public float speedBoost = 5.0f; // Speed boost value

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply speed boost to the player
            PlayerMechanics playerMechanics = other.GetComponent<PlayerMechanics>();
            if (playerMechanics != null)
            {
                playerMechanics.ApplySpeedBoost(speedBoost);
            }

            // Destroy the speed boots object
            Destroy(gameObject);
        }
    }
}