using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private AudioClip shootingSound; // Add this line for the audio clip

    private int ammoCount;
    private AudioSource audioSource;

    private void Start()
    {
        ammoCount = maxAmmo;

        // Initialize the AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If AudioSource component is not attached, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set loop to false to prevent sound from looping
        audioSource.loop = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammoCount > 0) // Left mouse click and there is ammo
        {
            ThrowProjectile();

            // Play shooting sound only when shooting
            if (audioSource && shootingSound)
            {
                // Enable AudioSource temporarily to play the sound
                audioSource.enabled = true;

                audioSource.clip = shootingSound;
                audioSource.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private void ThrowProjectile()
    {
        if (projectilePrefab && throwPoint)
        {
            // Instantiate a new projectile at the throw point
            GameObject newProjectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);

            // Apply force to the projectile to simulate the throw
            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
            if (projectileRigidbody)
            {
                projectileRigidbody.AddForce(throwPoint.forward * 20f, ForceMode.Impulse);
            }

            // Decrement the ammo count
            ammoCount--;

            // Destroy the projectile after a certain time (adjust as needed)
            Destroy(newProjectile, 3f);
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammoCount = maxAmmo;
    }

    // Expose a method to play the shooting sound from the inspector
    public void PlayShootingSound()
    {
        if (audioSource && shootingSound)
        {
            // Enable AudioSource temporarily to play the sound
            audioSource.enabled = true;

            audioSource.clip = shootingSound;
            audioSource.Play();
        }
    }
}