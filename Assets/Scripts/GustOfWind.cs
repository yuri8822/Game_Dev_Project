using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustOfWind : MonoBehaviour
{
    [SerializeField] private float upwardForce = 10f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rigidBody = other.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            rigidBody.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rigidBody = other.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
