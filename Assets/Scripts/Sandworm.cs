using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandworm : MonoBehaviour
{

    public int attackDamage = 10; // Damage dealt to the player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player upon collision
            PlayerMechanics playerMechanics = collision.gameObject.GetComponent<PlayerMechanics>();
            if (playerMechanics != null)
            {
                playerMechanics.PlayerHurt(attackDamage);
            }
        }
    }
}
