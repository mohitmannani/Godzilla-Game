using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float shootingRange = 10f;
    public float fireRate = 2f;
    public Transform turret;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private float nextFireTime;

    private void Update()
    {
        if (player != null)
        {
            // Calculate distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Follow the player if within shooting range
            if (distanceToPlayer <= shootingRange)
            {
                FollowPlayer();

                // Shoot at the player if cooldown has elapsed
                if (Time.time > nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate; // Update next fire time
                }
            }
        }
    }

    void FollowPlayer()
    {
        // Calculate direction to the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep the tank on the same plane as the player

        // Log the direction for troubleshooting
        Debug.Log("Direction: " + direction);

        // Rotate towards the player
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // Move towards the player
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        // Instantiate a projectile
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, turret.rotation);
        }
    }
}
