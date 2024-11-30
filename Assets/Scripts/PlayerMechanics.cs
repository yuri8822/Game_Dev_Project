using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float playerSize = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1f, 1f);
    [SerializeField] private int attackDamage = 10;

    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float coolDownTimer = Mathf.Infinity;
    private float horizontalInput;
    private int maxHealth;
    private bool isDead;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        maxHealth = playerHealth;
        isDead = false;
    }

    private void Update()
    {
        if (isDead == false)
        {
            PlayerMovement();
            PlayerAttacking();
        }
    }

    private void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * playerSpeed, rigidBody.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        } 
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-playerSize, playerSize, playerSize);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping() == false)
        {
            Jump();
        }

        animator.SetBool("isWalking", horizontalInput != 0);
        animator.SetBool("isJumping", isJumping());
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
        animator.SetTrigger("jump");
    }

    private bool isJumping()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,Vector2.down, 0.1f, groundLayer);
        return hit.collider == null;
    }

    private bool validAttack()
    {
        return (isJumping() == false) && (coolDownTimer > attackCooldown);
    }

    private void PlayerAttacking()
    {
        if (Input.GetMouseButtonDown(0) && validAttack())
        {
            Attack();
        }
        
        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        float playerDirection = Mathf.Sign(transform.localScale.x);
        Vector2 boxSource = (Vector2)transform.position + new Vector2(playerDirection * (attackBoxSize.x / 2), 0);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(boxSource, attackBoxSize, 0f, Vector2.zero, 0, enemyLayer);
        VisualizeAttackBox(boxSource, attackBoxSize, Color.red, 0.5f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().EnemyHurt(attackDamage);
                Debug.Log("Hit enemy");
            }
            else
            {
                Debug.Log("Hit nothing");
            }
        }
        animator.SetTrigger("attack");
        coolDownTimer = 0;
    }

    public void PlayerHurt(int damage)
    {
        playerHealth = Mathf.Clamp(playerHealth - damage, 0, maxHealth);
        if (playerHealth > 0)
        {
            animator.SetTrigger("hurt");
        }
        else
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        animator.SetTrigger("dead");
        isDead = true;
    }

    private void VisualizeAttackBox(Vector2 center, Vector2 size, Color colour, float duration)
    {
        Vector2 topLeft = center + new Vector2(-size.x / 2, size.y / 2);
        Vector2 topRight = center + new Vector2(size.x / 2, size.y / 2);
        Vector2 bottomLeft = center + new Vector2(-size.x / 2, -size.y / 2);
        Vector2 bottomRight = center + new Vector2(size.x / 2, -size.y / 2);

        Debug.DrawLine(topLeft, topRight, colour, duration);
        Debug.DrawLine(topRight, bottomRight, colour, duration);
        Debug.DrawLine(bottomRight, bottomLeft, colour, duration);
        Debug.DrawLine(bottomLeft, topLeft, colour, duration);
    }

}
