using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        // Check for player death or other actions
        if (currentHealth <= 0)
        {
            // Implement game over logic or respawn logic here
        }
    }
}
