using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyKnightAttack : MonoBehaviour
{
   [Header("Attributes")]
   [SerializeField] private float attackCooldown = 1f;
   [SerializeField] private int attackDamage = 20;
   [SerializeField] private float range = 1f;
   [SerializeField] private Vector2 attackBoxSize = new Vector2(1f, 1f);


   [Header("References")]
   [SerializeField] private LayerMask playerLayer;
   [SerializeField] private BoxCollider2D attackBox;
   private PlayerMechanics player;
   private float cooldownTimer = Mathf.Infinity;
   private Animator animator;


   private void Awake()
   {
        animator = GetComponent<Animator>();

   }

   private void Update()
   {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown && isPlayerNear())
        {
            cooldownTimer = 0;
            Attack();
        }
   }

   private void Attack()
   {
        int random = Random.Range(1, 4);
        animator.SetTrigger("attack0" + random.ToString());
   }

   private void DamagePlayer()
   {
        if (isPlayerNear())
        {
            player.PlayerHurt(attackDamage);
        }
   }

   private bool isPlayerNear()
   {
        RaycastHit2D hit = Physics2D.BoxCast(attackBox.bounds.center + transform.right * range * (transform.localScale.x > 0? 1f : -1f), attackBoxSize, 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            player = hit.collider.gameObject.GetComponent<PlayerMechanics>();
        }
        return hit.collider != null;
   }

   private void OnDrawGizmos()
   {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackBox.bounds.center + transform.right * range * (transform.localScale.x > 0? 1f : -1f), attackBoxSize);
   }
}
