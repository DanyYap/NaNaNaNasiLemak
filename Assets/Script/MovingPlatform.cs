using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;         // Set waypoints in Inspector
    public float moveSpeed = 2f;          // Speed of platform
    public float waitTime = 1f;           // Wait time at each point

    private int currentIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                currentIndex = (currentIndex + 1) % waypoints.Length;
            }
            return;
        }

        // Move towards the current waypoint
        Transform target = waypoints[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // If arrived at the waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            isWaiting = true;
        }
    }
}
