using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private Transform[] projectileSpawnPoints;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float shootInterval = 2f;

    // Make dronePrefabs public so it can be accessed from other scripts
    [Header("Drone Settings")]
    [SerializeField] public GameObject[] dronePrefabs;

    private Transform _playerTransform;
    private int _currentSpawnPointIndex = 0;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>()?.transform;
        InvokeRepeating(nameof(ShootProjectile), 0f, shootInterval);
    }

    private void ShootProjectile()
    {
        if (_playerTransform != null && projectileSpawnPoints != null && projectileSpawnPoints.Length > 0)
        {
            GameObject dronePrefab = GetDronePrefab();
            Transform spawnPoint = projectileSpawnPoints[_currentSpawnPointIndex];

            if (dronePrefab != null && spawnPoint != null)
            {
                // Instantiate the dronePrefab at the spawn point
                GameObject drone = Instantiate(dronePrefab, spawnPoint.position, spawnPoint.rotation);
                // Get the DroneController component from the instantiated drone
                DroneController droneController = drone.GetComponent<DroneController>();

                if (droneController != null)
                {
                    // Initialize components and enable the DroneController
                    droneController.InitializeComponents();
                    droneController.enabled = true;
                }
            }

            _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % projectileSpawnPoints.Length;
        }
    }

    private GameObject GetDronePrefab()
    {
        if (dronePrefabs == null || dronePrefabs.Length == 0)
        {
            Debug.LogError("Drone Prefabs not assigned. Assign drone prefabs in the Inspector.");
            return null;
        }

        return dronePrefabs[Random.Range(0, dronePrefabs.Length)];
    }
}
