using UnityEngine;
using System.Collections;

public class TestDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private AudioClip damageSound; // Add this line

    private void Update()
    {
        // Example: Press the space key to apply damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyDamage();
        }
    }

    private void ApplyDamage()
    {
        // Find the Health component on the player GameObject
        Health playerHealth = FindObjectOfType<Health>();

        // Check if the Health component is found
        if (playerHealth != null)
        {
            // Apply damage to the player
            playerHealth.TakeDamage(damageAmount);

            // Play damage sound if the audio clip is assigned
            if (damageSound != null)
            {
                AudioSource.PlayClipAtPoint(damageSound, transform.position);
            }

            // Delay additional actions after applying damage
            StartCoroutine(DelayedActions());
        }
        else
        {
            Debug.LogWarning("No Health component found on the player!");
        }
    }

    private IEnumerator DelayedActions()
    {
        // Wait for 2 seconds before additional actions
        yield return new WaitForSeconds(2f);

        // Additional actions after waiting
        Debug.Log("Additional actions after applying damage.");
    }
}