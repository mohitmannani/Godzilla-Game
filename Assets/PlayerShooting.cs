using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10.0f;

    private void Update()
    {
        // Check for the "F" key press
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed"); // Add this line for debugging
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        Debug.Log("ShootFireball called"); // Add this line for debugging

        // Instantiate a new fireball at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Check if the fireball has a Rigidbody component
        Rigidbody rb = fireball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Shoot the fireball forward
            rb.velocity = transform.forward * fireballSpeed;
        }
        else
        {
            // Handle the case where the fireball prefab does not have a Rigidbody
            Debug.LogWarning("The fireball prefab does not have a Rigidbody component.");
            Destroy(fireball); // Remove the fireball if it can't be shot
        }
    }
}