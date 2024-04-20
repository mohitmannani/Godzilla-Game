using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hit : MonoBehaviour
{
    public Image bloodSplashImage; // Reference to the blood splash UI image
    public float bloodSplashDuration = 1.0f; // Duration to display the blood splash in seconds
    public AudioClip punchSound; // Sound effect for the punch
    public float soundVolume = 0.5f; // Sound effect volume
    public float maxDistance = 3.0f; // Adjust this value as needed
    public LayerMask layerMask; // Define the layer mask for raycasting

    private bool isHit = false;
    public GameObject godzilla; // Reference to the "Godzilla" GameObject




    public GameObject fireballObject; // Reference to the fireball GameObject
    public float activationDuration = 1.0f; // Duration to activate the fireball
    private bool isCooldown = false; // Flag to prevent multiple activations

    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar healthBar;
    

    private void Start()
    {
        godzilla = GameObject.FindGameObjectWithTag("Godzilla"); // Make sure to tag your "Godzilla" object as "Godzilla"
        

    currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (!isCooldown && Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(ActivateAndCooldown());
        }


        if (Input.GetKeyDown(KeyCode.F) && IsPlayerCloseToGodzilla())
        {
            // Play the punch sound effect
            Debug.Log("Punch!");

            PlayPunchSound();

            // Display blood splash
            ShowBloodSplash();

            // Set a flag to prevent multiple hits
            isHit = true;


            TakeDamage(1);
        }
    }


    System.Collections.IEnumerator ActivateAndCooldown()
    {
        isCooldown = true;

        fireballObject.SetActive(true);

        yield return new WaitForSeconds(activationDuration);

        fireballObject.SetActive(false);

        isCooldown = false;
    }


    private bool IsPlayerCloseToGodzilla()
    {
        if (godzilla != null)
        {
            Vector3 playerPosition = transform.position;
            Vector3 godzillaPosition = godzilla.transform.position;
            Vector3 direction = godzillaPosition - playerPosition;

            RaycastHit hit;

            if (Physics.Raycast(playerPosition, direction, out hit, maxDistance, layerMask))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                return true;
                //  if (hit.collider.gameObject == godzilla)
                //   {
                //    Debug.Log("Close to Godzilla");
                //     return true;
                // }
            }
        }

        return false;
    }

    private void PlayPunchSound()
    {
        if (punchSound != null)
        {
            AudioSource.PlayClipAtPoint(punchSound, transform.position, soundVolume);
            Debug.Log("Punch sound played");
        }
    }

    private void ShowBloodSplash()
    {
        bloodSplashImage.gameObject.SetActive(true);
        Debug.Log("Blood splash shown");

        // Deactivate the blood splash after a delay
        Invoke("HideBloodSplash", bloodSplashDuration);
    }

    private void HideBloodSplash()
    {
        Debug.Log("Hiding blood splash");
        bloodSplashImage.gameObject.SetActive(false);
    }



    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}