using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 50f;

    private float currentHealth;

    private enum EnemyState
    {
        Idle,
        Chasing,
        Attacking,
        Dead
    }

    private EnemyState currentState = EnemyState.Idle;

    private Life playerLife;

    private void Start()
    {
        currentHealth = maxHealth;
        playerLife = FindObjectOfType<Life>();

        if (playerLife == null)
        {
            Debug.LogError("PlayerDestroyer: Life script not found on any GameObject in the scene.");
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                CheckForPlayer();
                break;
            case EnemyState.Chasing:
                MoveTowardsPlayer();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Dead:
                // Handle any death-related logic
                break;
        }
    }

    private void CheckForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                SetState(EnemyState.Chasing);
                break;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Implement movement logic here to move towards the player
        // You can use Vector3.MoveTowards, NavMeshAgent, or other movement methods

        // For demonstration purposes, let's transition to attack when close to the player
        if (Vector3.Distance(transform.position, playerLife.transform.position) < detectionRange)
        {
            SetState(EnemyState.Attacking);
        }
    }

    private void Attack()
    {
        if (playerLife != null)
        {
            playerLife.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            SetState(EnemyState.Dead);
        }
    }

    private void Die()
    {
        // Implement death logic, such as playing death animations or effects
        Destroy(gameObject);
    }

    private void SetState(EnemyState newState)
    {
        currentState = newState;

        // Additional logic can be added here based on state transitions
    }
}
