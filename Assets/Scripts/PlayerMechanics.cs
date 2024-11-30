using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float playerSize = 10;

    [Header("References")]
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform.localScale = new Vector3(playerSize, playerSize, playerSize);
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * playerSpeed, rigidBody.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        } 
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-playerSize, playerSize, playerSize);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping() == false)
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
    
}
