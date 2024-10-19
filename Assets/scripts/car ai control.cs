using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : MonoBehaviour
{
    public Transform[] waypoints;
    public float waypointDistanceThreshold = 10f;
    public float accelerationFactor = 1f;
    public float brakeDistance = 5f;

    private int currentWaypointIndex = 0;
    private CarController carController; // Reference to your CarController script
    private Transform targetWaypoint;

    private void Start()
    {
        carController = GetComponent<CarController>(); // Get reference to CarController
    }

    private void Update()
    {
        if (targetWaypoint != null)
        {
            MoveTowardsWaypoint(); // Handle movement towards the waypoint
        }
    }

    private void MoveTowardsWaypoint()
    {
        // Calculate distance to the current waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

        // Handle acceleration and steering
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float steering = Vector3.Dot(transform.right, direction); // Use this for steering
        float acceleration = (distanceToWaypoint > brakeDistance) ? 1f : 0f; // Brake when near the waypoint

        carController.horizontalInput = steering; // Steer towards the waypoint
        carController.verticalInput = acceleration * accelerationFactor; // Control speed

        // If close enough to the waypoint, switch to the next one
        if (distanceToWaypoint < waypointDistanceThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }
            targetWaypoint = waypoints[currentWaypointIndex]; // Set the next waypoint
        }
    }
}
