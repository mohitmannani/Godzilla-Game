using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool destroyOnCollision = true;

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string obstacleAnimationTrigger = "Zombie@Death01_A";

    [Header("Player Following Settings")]
    [SerializeField] private bool followPlayer = true;
    [SerializeField] private Transform playerTransform;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int damagePerHit = 1;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float stoppingDistance = 2f;

    [Header("On Hit Event")]
    public UnityEvent onHitEvent;

    private int currentHealth;

    private void Start()
    {
        // Automatically find the player if not set in the inspector
        if (followPlayer && playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        // Initialize health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Follow the player's position
        if (followPlayer && playerTransform != null)
        {
            MoveTowardsPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ground layer and not the player
        if (IsCollisionWithGround(collision) && !collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with the ground
            if (destroyOnCollision)
            {
                Debug.Log("Obstacle collided with ground and will be destroyed.");
                TakeDamage(damagePerHit);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player shot the obstacle using the FireExplosion bullet
        if (other.gameObject.name == "FireExplosion")
        {
            Debug.Log("Obstacle hit by FireExplosion bullet.");
            TakeDamage(damagePerHit);
        }
    }

    private void TakeDamage(int damage)
    {
        // Reduce health
        currentHealth -= damage;

        // Check if health is depleted
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy dead!");
            // Trigger the OnHit event
            onHitEvent.Invoke();

            // Optionally, play the disappearing animation or any other actions
            PlayAnimation(obstacleAnimationTrigger, 2f); // Adjust the speed value as needed
        }
    }

    private bool IsCollisionWithGround(Collision collision)
    {
        return (groundLayer.value & 1 << collision.gameObject.layer) != 0;
    }

    private void PlayAnimation(string trigger, float speed)
    {
        // Play the specified animation trigger with the specified speed
        if (animator != null)
        {
            Debug.Log("Playing obstacle animation.");
            animator.speed = speed;
            animator.SetTrigger(trigger);
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Move towards the player if not too close
        if (distanceToPlayer > stoppingDistance)
        {
            transform.Translate(directionToPlayer * movementSpeed * Time.deltaTime, Space.World);
        }
    }
}

