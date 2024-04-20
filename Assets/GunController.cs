using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;   // Prefab of the bullet to be fired
    public Transform firePoint;       // Point where the bullets are spawned
    public float bulletSpeed = 20f;   // Speed of bullets
    public int bulletDamage = 20;     // Damage bullets deal
    public float fireRate = 0.2f;     // Fire rate in seconds
    public int maxAmmo = 30;          // Maximum ammunition
    private int currentAmmo;          // Current ammunition
    private float fireTimer = 0f;     // Timer for firing rate

    void Start()
    {
        currentAmmo = maxAmmo;  // Initialize current ammo to the maximum ammo
    }

    void Update()
    {
        // Handle player input for shooting (e.g., mouse click)
        if (Input.GetButton("Fire1") && Time.time >= fireTimer)
        {
            if (currentAmmo > 0)
            {
                Shoot();  // Call the Shoot method
                fireTimer = Time.time + 1f / fireRate;  // Reset the fire timer
                currentAmmo--;  // Decrease ammo after shooting
            }
        }

        // Handle player input for reloading (e.g., "R" key)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();  // Call the Reload method
        }
    }

    void Shoot()
    {
        // Spawn a bullet from the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // You can add any additional logic here for shooting
    }

    void Reload()
    {
        // If the current ammo is less than the maximum ammo, reload
        if (currentAmmo < maxAmmo)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, currentAmmo); // Calculate how much ammo to reload

            // Implement reloading logic here (e.g., deduct ammo from the player's inventory)

            // Update the current ammo count
            currentAmmo += ammoToReload;
        }
    }
}
