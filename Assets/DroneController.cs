using UnityEngine;

public class DroneController : MonoBehaviour
{
    [Header("Drone Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookAheadDistance = 5f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Damage Settings")]
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private float minDistanceToPlayer = 5f;

    [Header("Shooting Settings")]
    [SerializeField] private float shootingSpeed = 10f;
    [SerializeField] private int maxShots = 5; // Set the maximum number of shots
    [SerializeField] private float bulletLifetime = 5f; // Set the lifetime of the bullet

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPosition; // Add the spawn position variable

    private Rigidbody _rb;
    private Transform _playerTransform;
    private int _currentDronePrefabIndex = 0;

    private DroneLookAtPlayer _lookAtPlayerScript;
    private int remainingShots; // Variable to keep track of remaining shots

    private void Start()
    {
        InitializeComponents();
        if (enabled)
        {
            // Spawn drones
            SpawnDrones();

            // Start shooting coroutine
            InvokeRepeating(nameof(ShootProjectile), 0f, 2f);
        }
    }

    public void InitializeComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _playerTransform = FindObjectOfType<PlayerMovement>()?.transform;

        if (_rb == null)
        {
            Debug.LogError("Rigidbody not found. Make sure it has a Rigidbody component.");
            enabled = false;
        }

        if (_playerTransform == null)
        {
            Debug.LogError("PlayerMovement not found. Make sure the player has the correct component.");
            enabled = false;
        }

        _lookAtPlayerScript = GetComponent<DroneLookAtPlayer>();
        if (_lookAtPlayerScript == null)
        {
            _lookAtPlayerScript = gameObject.AddComponent<DroneLookAtPlayer>();
        }

        _lookAtPlayerScript.SetPlayerTransform(_playerTransform);

        remainingShots = maxShots; // Initialize remaining shots
    }

    private void Update()
    {
        if (CheckForNullComponents())
            return;

        MoveTowardsPlayer();
    }

    private bool CheckForNullComponents()
    {
        return _rb == null || _playerTransform == null || damagePrefab == null || _lookAtPlayerScript == null;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 movement = directionToPlayer * moveSpeed * Time.deltaTime;

        if (!HasObstacleInFront())
        {
            _rb.MovePosition(transform.position + movement);
        }

        Vector3 directionToPlayerAvoidance = (transform.position - _playerTransform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer < minDistanceToPlayer)
        {
            _rb.MovePosition(transform.position + directionToPlayerAvoidance * (minDistanceToPlayer - distanceToPlayer));
        }
    }

    private bool HasObstacleInFront()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.Raycast(ray, lookAheadDistance, obstacleLayer);
    }

    private void SpawnDrones()
    {
        if (CheckForNullComponents())
            return;

        for (int i = 0; i < maxShots; i++)
        {
            GameObject dronePrefab = damagePrefab; // Use damagePrefab as the dronePrefab

            Vector3 randomSpawnPos = (spawnPosition != null) ? spawnPosition.position : transform.position;
            randomSpawnPos += Random.insideUnitSphere * spawnRadius;
            randomSpawnPos.y = transform.position.y;

            GameObject drone = Instantiate(dronePrefab, randomSpawnPos, Quaternion.identity);
            DroneController droneController = drone.GetComponent<DroneController>();

            if (droneController != null)
            {
                droneController.InitializeComponents();
                droneController.enabled = true;
            }

            _currentDronePrefabIndex++;
        }
    }

    private void ShootProjectile()
    {
        if (CheckForNullComponents() || remainingShots <= 0)
            return;

        GameObject projectilePrefab = damagePrefab;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0f; // Ignore the vertical component
            projectileRb.velocity = directionToPlayer * shootingSpeed;

            // Set the lifetime of the projectile
            Destroy(projectile, bulletLifetime);

            remainingShots--; // Decrease remaining shots
        }
    }
}
