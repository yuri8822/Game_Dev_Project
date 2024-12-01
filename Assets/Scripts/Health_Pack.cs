using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int healAmount = 25;  

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerMechanics player = other.GetComponent<PlayerMechanics>();
            if (player != null)
            {
                player.Heal(healAmount);  
                Destroy(gameObject);  
            }
        }
    }
}
