using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyKnightMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private float idleDuration = 1f;

    [Header("Movement Bounds")]
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    [Header("References")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator animator;

    private Vector3 enemyScale;
    private bool isMovingLeft;
    private float idleTimer;

    private void Awake()
    {
        enemyScale = enemy.localScale;
    }

    private void Update()
    {
        if (isMovingLeft == true)
        {
            if (enemy.position.x >= leftBound.position.x)
            {
                Move(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightBound.position.x)
            {
                Move(1);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        animator.SetBool("isWalking", false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            isMovingLeft = !isMovingLeft;
        }
    }

    public void ChangeDirectionOnHit()
    {
        animator.SetBool("isWalking", false);
        isMovingLeft = !isMovingLeft;
    }    

    private void Move(int direction)
    {
        idleTimer = 0;
        animator.SetBool("isWalking", true);
        enemy.localScale = new Vector3(Mathf.Abs(enemyScale.x) * direction, enemyScale.y, enemyScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * enemySpeed, enemy.position.y, enemy.position.z);
    }

    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
    }

    public void SetEnemySpeed(float speed)
    {
        enemySpeed = speed;
    }
}
