using UnityEngine;
using UnityEngine.AI;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private GameObject obstaclePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private int numberOfObstaclesToSpawn = 2;  // Set the desired number in the inspector
    [SerializeField, Range(1f, 10f)] private float minDistanceBetweenDrones = 5f;

    [Header("Obstacle Movement Settings")]
    [SerializeField] private float obstacleSpeed = 5f;

    [Header("Player Settings")]
    [SerializeField] private Transform playerTransform;

    [Header("Height Settings")]
    [SerializeField] private float minHeight = 10f;
    [SerializeField] private float maxHeight = 15f;

    [Header("Toggle Loop")]
    [SerializeField]
    private bool enableLoop = true;

#if UNITY_EDITOR
    [HideInInspector]
    public bool EnableLoop
    {
        get { return enableLoop; }
        set { enableLoop = value; }
    }
#endif

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (enableLoop)
        {
            SpawnObstacles();
        }
    }

    private void SpawnObstacles()
    {
        if (!enableLoop)
        {
            return;
        }

        Debug.Log("SpawnObstacles Start");

        for (int i = 0; i < numberOfObstaclesToSpawn; i++)
        {
            Debug.Log("Spawning obstacle " + i);

            Vector3 randomSpawnPos = GenerateRandomSpawnPosition();
            float randomHeight = Random.Range(minHeight, maxHeight);
            randomSpawnPos.y = randomHeight;

            GameObject obstacle = Instantiate(obstaclePrefab, randomSpawnPos, Quaternion.identity);
            SetObstacleSpeed(obstacle);

            // Adjust the obstacle's position to avoid others
            NavMeshAgent agent = obstacle.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(FindValidPosition(obstacle.transform.position));
            }
        }

        Debug.Log("SpawnObstacles End");
    }

    private Vector3 GenerateRandomSpawnPosition()
    {
        return transform.position + Random.onUnitSphere * minDistanceBetweenDrones;
    }

    private void SetObstacleSpeed(GameObject obstacle)
    {
        Rigidbody obstacleRb = obstacle.GetComponent<Rigidbody>();
        if (obstacleRb != null && playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - obstacle.transform.position).normalized;
            obstacleRb.velocity = directionToPlayer * obstacleSpeed;
        }
    }

    private Vector3 FindValidPosition(Vector3 originalPosition)
    {
        NavMeshHit hit;
        // Try to find a valid position nearby
        if (NavMesh.SamplePosition(originalPosition, out hit, minDistanceBetweenDrones, NavMesh.AllAreas))
        {
            return hit.position;
        }
        // If not found, return the original position
        return originalPosition;
    }
}
