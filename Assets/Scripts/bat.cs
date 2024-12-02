using UnityEngine;

public class IceBatEnemy : MonoBehaviour
{
    [Header("Bat Attributes")]
    public float speed = 7f; // Constant movement speed
    public float attackRange = 10f; // Range at which the bat detects the player
    public int attackDamage = 10; // Damage dealt to the player
    public float returnSpeed = 3f; // Speed at which the bat returns to its original position

    [Header("References")]
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalPosition;
    private bool isAttacking;
    private bool isPlayerInRange;
    private float distanceTraveled;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalPosition = transform.position; // Store the bat's starting position
        isPlayerInRange = false;
        distanceTraveled = 0f;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            isPlayerInRange = true;
            MoveTowardsPlayer();
        }
        else
        {
            isPlayerInRange = false;
            ReturnToOriginalPosition();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!isAttacking)
        {
            // Calculate how far the bat has traveled from its original position
            distanceTraveled = Vector2.Distance(originalPosition, transform.position);

            // Stop moving if the bat has traveled the full attack range
            if (distanceTraveled >= attackRange)
            {
                RemainStationary();
                return;
            }

            // Move towards the player's position at a constant speed
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Play "Flying" animation
            animator.SetBool("IsFlying", true);
        }
    }

    private void ReturnToOriginalPosition()
    {
        // Stop any movement and return to the starting position
        if (Vector2.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
            animator.SetBool("IsFlying", true); // Play flying animation
        }
        else
        {
            RemainStationary();
        }
    }

    private void RemainStationary()
    {
        // Stop movement and play idle animation
        rb.velocity = Vector2.zero;
        animator.SetBool("IsFlying", false);
    }

    private void Attack()
    {
        isAttacking = true;

        // Play attack animation
        animator.SetTrigger("Attack");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPlayerInRange)
        {
            // Deal damage to the player upon collision
            PlayerMechanics playerMechanics = collision.gameObject.GetComponent<PlayerMechanics>();
            if (playerMechanics != null)
            {
                playerMechanics.PlayerHurt(attackDamage);
            }

            // Optionally, trigger an attack animation upon collision
            Attack();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false; // Reset attack state when the player moves out of collision
        }
    }
}
