using UnityEngine;

public class CompanionDrone : MonoBehaviour
{
    public Transform player;
    public Vector3 followDistance = new Vector3(5f, 2f, 5f);
    public float normalMovementSpeed = 3f;
    public float boostedMovementSpeed = 10f;
    public float rotationSpeed = 5f;
    public float obstacleAvoidanceDistance = 2f;
    public LayerMask obstacleLayer;

    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float fireRate = 2f;
    private float nextFireTime;

    public float maxYConstraint = 10f;

    private bool isReturning = false;
    private Vector3 returnStartPosition;

    private float maxDistanceToPlayer = 50f; // Adjust as needed

    void Start()
    {
        returnStartPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > maxDistanceToPlayer)
            {
                // Reposition the drone if it's too far from the player
                RepositionDrone();
            }
            else
            {
                AvoidObstacles();
                PerformAIActions();
                HandleInput();
            }
        }
    }

    void AvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleAvoidanceDistance, obstacleLayer))
        {
            Vector3 avoidDir = Vector3.Reflect(transform.forward, hit.normal);
            transform.rotation = Quaternion.LookRotation(avoidDir, Vector3.up);
        }
    }

    void PerformAIActions()
    {
        if (Vector3.Distance(transform.position, player.position) < followDistance.magnitude)
        {
            FollowPlayer();
        }
        else if (!isReturning)
        {
            Patrol();
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // Move only on the X and Z axes
        MoveOnAxes(direction, isReturning ? boostedMovementSpeed : normalMovementSpeed);
    }

    void Patrol()
    {
        if (patrolWaypoints.Length == 0)
        {
            Debug.LogError("No patrol waypoints assigned to the drone.");
            return;
        }

        Vector3 targetWaypoint = patrolWaypoints[currentWaypointIndex].position;
        Vector3 direction = targetWaypoint - transform.position;

        // Check if the drone is close enough to the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint) < 1f)
        {
            // Move to the next waypoint in the array
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }

        // Rotate towards the current waypoint
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // Move only on the X and Z axes
        MoveOnAxes(direction, normalMovementSpeed);
    }

    void MoveOnAxes(Vector3 direction, float speed)
    {
        // Move only on the X and Z axes
        Vector3 newPosition = transform.position + new Vector3(direction.x, 0f, direction.z).normalized * speed * Time.deltaTime;

        // Check against maxYConstraint
        newPosition.y = Mathf.Clamp(newPosition.y, -Mathf.Infinity, maxYConstraint);

        transform.position = newPosition;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFollowState();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            CallDroneToPlayer();
        }
    }

    void ToggleFollowState()
    {
        if (Vector3.Distance(transform.position, player.position) > followDistance.magnitude)
        {
            Debug.Log("Drone is now following the player.");
        }
        else
        {
            Debug.Log("Drone is now patrolling.");
        }
    }

    void ShootProjectile()
    {
        nextFireTime = Time.time + 1f / fireRate;

        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }
    }

    void CallDroneToPlayer()
    {
        isReturning = true;
        Debug.Log("Drone called to player's position.");
        StartCoroutine(ReturnToPlayer());
    }

    System.Collections.IEnumerator ReturnToPlayer()
    {
        while (Vector3.Distance(transform.position, player.position) > followDistance.magnitude)
        {
            // Move only on the X and Z axes
            Vector3 direction = (player.position - transform.position).normalized;
            MoveOnAxes(direction, boostedMovementSpeed);
            yield return null;
        }

        isReturning = false;
        returnStartPosition = transform.position;
        Debug.Log("Drone has returned to the player.");
    }

    void RepositionDrone()
    {
        // Reposition the drone closer to the player
        transform.position = player.position + Vector3.up; // You can customize the repositioning logic
        isReturning = false;
        returnStartPosition = transform.position;
        Debug.Log("Drone has been repositioned.");
    }
}
