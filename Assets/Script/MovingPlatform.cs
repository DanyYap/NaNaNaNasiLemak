using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;         // Set waypoints in Inspector
    public float moveSpeed = 2f;          // Speed of platform
    public float waitTime = 1f;           // Wait time at each point

    public GameObject Player;
    private bool PlayerOnPlatform = false;
    private Vector3 lastPosition;
    
    private int currentIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Start()
    {
        lastPosition = transform.position;
    }
    
    void Update()
    {
        if (waypoints.Length == 0) return;

        Vector3 movementDelta = transform.position - lastPosition;

        if (Player != null && PlayerOnPlatform)
        {
            Player.transform.position += movementDelta;
        }

        // Move platform
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                currentIndex = (currentIndex + 1) % waypoints.Length;
            }
        }
        else
        {
            Transform target = waypoints[currentIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                isWaiting = true;
            }
        }

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerOnPlatform = false;
        }
    }
}
