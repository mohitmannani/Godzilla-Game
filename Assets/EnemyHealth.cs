using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add code here to handle the enemy drone's death, such as playing a death animation or particle effect.

        // Once the enemy drone has been destroyed, you can remove it from the game using the following line of code:
        Destroy(gameObject);
    }
}