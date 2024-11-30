using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints (GameObjects)
    public float speed = 2f;       // Speed at which the enemy moves
    private int currentWaypointIndex = 0;  // Index of the current waypoint

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    }

    // Function to move the enemy along the path
    void MoveAlongPath()
    {
        if (waypoints.Length == 0)
            return;

        // Move the enemy towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // If the enemy has reached the current waypoint, move to the next waypoint
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;

            // If we've reached the last waypoint, reset to the first waypoint (for looping)
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;  // Remove this line if you don't want looping
            }
        }
    }
}
