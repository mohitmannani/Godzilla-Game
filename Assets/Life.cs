using UnityEngine;

public class Life : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // You can add any additional logic related to health here
    }

    public void TakeDamage(float damage)
    {
        // Reduce the player's health when taking damage
        currentHealth -= damage;

        // Optional: Add any visual or audio feedback for taking damage

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implement death logic, such as playing death animations or effects
        // You can also handle other game over logic here
        Destroy(gameObject);
    }
}
