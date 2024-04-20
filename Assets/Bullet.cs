using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;

    private void Start()
    {
        // Set initial velocity in the forward direction
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits an obstacle
        if (other.CompareTag("Obstacle"))
        {
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
