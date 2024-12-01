using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // This script contains functions that will common across all enemies.
    // They will have health and they will take damage from the player and die.
    // I have implemented functionality for that here. Please attach this script to any enemy u make that will be damageable.
   [Header("Enemy Attributes")]
   [SerializeField] private int enemyHealth = 50;
   [SerializeField] private float delayDestruction = 1f;        // How long should the enemy game object exist before the Destroy function is called.
   [SerializeField] private float iFramesDuration = 1f;          // How long the enemy should be invulnerable after getting hit.
   [SerializeField] private int noFlashes = 3;                   // Number of times the enemy flashes red when invulnerable


   [Header("Animation Triggers")]
   [SerializeField] private String hurtTrigger;
   [SerializeField] private String deadTrigger;
   private int maxHealth;
   private Animator animator;
   private bool isDead;
   private SpriteRenderer spriteRenderer;
   private bool isInvulnerable;

   private void Awake()
   {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = enemyHealth;
        isDead = false;
        isInvulnerable = false;
   }

   public void EnemyHurt(int damage)
   { 
        if (isInvulnerable == false)
        {
               enemyHealth = Mathf.Clamp(enemyHealth - damage, 0, maxHealth);
               if (enemyHealth > 0)
               {

                    if (animator != null)
                    {
                         animator.SetTrigger(hurtTrigger);
                         StartCoroutine(Iframes());
                    }
               }
               else
               {
                    EnemyDead();
               }
        }
   }

   private void EnemyDead()
   {
          if(isDead == false)
          {
               if (animator != null)
               {
                    animator.SetTrigger(deadTrigger);
               }
               isDead = true;
               Invoke("DestroyEnemy", delayDestruction);           // Delay the destruction of the enemy game object to allow death animations and sound effects to play. Asjust according to your enemy animations and sounds.
          }
   }

   private void DestroyEnemy()
   {
        Destroy(gameObject);
   }

   public bool isEnemyDead()        // You can use this lock other enemy actions such as movement and attacks when the enemy dies.
   {
        return isDead;
   }

   private IEnumerator Iframes()
    {
        isInvulnerable = true;
        for(int i = 0; i < noFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/ (noFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/ (noFlashes * 2));
        }
        isInvulnerable = false;
        
    }
}
