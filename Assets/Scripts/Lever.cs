using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {            
            Lever_Manager lever_Manager = FindObjectOfType<Lever_Manager>();
            lever_Manager.SetLever(gameObject);
        }
    }
}