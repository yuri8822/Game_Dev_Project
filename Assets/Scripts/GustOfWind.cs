using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustOfWind : MonoBehaviour
{
    [SerializeField] private float upwardForce = 10f; 

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
        }
    }
}
