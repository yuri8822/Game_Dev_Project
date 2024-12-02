using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private float iFramesDuration = 1f;
    [SerializeField] private int noFlashes = 3;
    [SerializeField] private float jumpRayLength = 1f;
    [SerializeField] private float jumpRayOffset = 1f;

    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Player Sound Clips")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip healSound;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float coolDownTimer = Mathf.Infinity;
    private float horizontalInput;
    private int maxHealth;
    private bool isDead;
    private bool isInvulnerable;
    private SpriteRenderer spriteRenderer;
    private StatusBar healthBar;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        maxHealth = playerHealth;
        isDead = false;
        isInvulnerable = false;
        Time.timeScale = 1;
        healthBar = GetComponent<StatusBar>();
        if (healthBar != null)
        {
            healthBar.ChangeSliderValue(playerHealth, maxHealth);
        }
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

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        } 
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(playerSize), playerSize, playerSize);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping() == false)
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }

        animator.SetBool("isWalking", horizontalInput != 0);
        animator.SetBool("isJumping", isJumping());
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
        animator.SetTrigger("jump");
        if (audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private bool isJumping()
    {
        Vector2 raySourceLeft = new Vector2(boxCollider.bounds.center.x - jumpRayOffset, boxCollider.bounds.min.y);
        Vector2 raySourceRight = new Vector2(boxCollider.bounds.center.x + jumpRayOffset, boxCollider.bounds.min.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(raySourceLeft, Vector2.down, jumpRayLength, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(raySourceRight, Vector2.down, jumpRayLength, groundLayer);

        VisualizeJumpRay(raySourceLeft, Vector2.down, jumpRayLength, hitLeft.collider != null ? Color.green : Color.red);
        VisualizeJumpRay(raySourceRight, Vector2.down, jumpRayLength, hitRight.collider != null ? Color.green : Color.red);
        
        return hitLeft.collider == null && hitRight.collider == null;
    }

    private bool validAttack()
    {
        return (isJumping() == false) && (coolDownTimer > attackCooldown);
    }

    private void PlayerAttacking()
    {
        if (Input.GetMouseButtonDown(0) && validAttack())
        {
            animator.SetTrigger("attack");
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
                hit.collider.gameObject.GetComponent<EnemyHealth>().FlashRed(2); // Flash red twice
                Debug.Log("Hit enemy");
            }
            else
            {
                Debug.Log("Hit nothing");
            }
        }
        
        coolDownTimer = 0;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    public void PlayerHurt(int damage)
    {
        if (isInvulnerable == false)
        {
            playerHealth = Mathf.Clamp(playerHealth - damage, 0, maxHealth);
            if (playerHealth > 0)
            {
                animator.SetTrigger("hurt");
                StartCoroutine(Iframes());
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(hurtSound);
                }
            }
            else
            {
                PlayerDead();
            }
            if (healthBar != null)
            {
                healthBar.ChangeSliderValue(playerHealth, maxHealth);
            }
        }
    }
       
    private void PlayerDead()
    {
        if (isDead == false)
        {
            animator.SetTrigger("dead");
            isDead = true;
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
            }
        }
        Invoke("RestartLevel", 1.2f);
    }

    private void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
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

    private void VisualizeJumpRay(Vector2 source, Vector2 direction, float length, Color colour)
    {
        Debug.DrawLine(source, source + direction * length, colour);
    }

    public void Heal(int healAmount)
    {
        if (isDead == false)
        {
            audioSource.PlayOneShot(healSound);
            
            playerHealth = Mathf.Clamp(playerHealth + healAmount, 0, maxHealth);
            Debug.Log("Player healed. Current health: " + playerHealth);
            if (healthBar != null)
            {
                healthBar.ChangeSliderValue(playerHealth, maxHealth);
            }
        }
    }



}
