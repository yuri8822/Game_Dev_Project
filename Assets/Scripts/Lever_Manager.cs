using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Manager : MonoBehaviour
{
    public GameObject door;           // Reference to the door GameObject

    private int leversSet = 0;      // Counter for set levers
    private int totalLevers;              // Total number of levers

    private void Start()
    {
        // Count all key objects at the start
        totalLevers = GameObject.FindGameObjectsWithTag("Lever").Length;
        
        // Ensure the door is hidden at the start
        if (door != null)
            door.SetActive(false);
    }

    public void SetLever(GameObject lever)
    {
        leversSet++;
        Destroy(lever); // Remove the lever from the scene

        if (leversSet == totalLevers)
        {
            Debug.Log("All levers Set! door activated.");
            ActivateDoor();
        }
    }
    private void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true); // Show the door
            Debug.Log("door is now visible.");
        }
        else
        {
            Debug.LogError("door GameObject is not assigned in Lever_Manager!");
        }
    }
}
