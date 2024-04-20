using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anklegrabber : MonoBehaviour
{
    public GameObject bloodSplashUI; // Reference to the blood splash UI object
    public AudioClip hitSound; // Sound to play on collision with obstacles

    private void OnTriggerEnter(Collider other)
    {
        RaycastHit hit;

        // Raycast from the ankle grabber position downward
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            // Check if the collider's tag is "Obstacles"
            if (hit.collider.CompareTag("Player"))
            {
                // Play the hit sound
                AudioSource.PlayClipAtPoint(hitSound, transform.position);

                // Activate the blood splash UI object
                if (bloodSplashUI != null)
                {
                    bloodSplashUI.SetActive(true);
                    // You might want to deactivate it after a certain delay
                    // For example, bloodSplashUI.SetActive(false) after a few seconds
                }
            }
        }
    }
}