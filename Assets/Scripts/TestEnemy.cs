using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
   // This script is to test if the player taking damage and dying is working correctly

   [SerializeField] private int damage = 1;

   private void OnTriggerEnter2D(Collider2D other)
   {
        if (other.gameObject.tag == "Player")
        {
            PlayerMechanics player = other.gameObject.GetComponent<PlayerMechanics>();
            player.PlayerHurt(damage);
        }
   }
}
