using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    [SerializeField] private AudioClip defaultReloadSound;
    [SerializeField] private AudioClip alternateReloadSound;

    private AudioSource audioSource;
    private Dictionary<int, AudioClip> weaponReloadSounds = new Dictionary<int, AudioClip>();
    private int currentWeapon = 1; // Default weapon

    private AmmoSystem ammoSystem; // Reference to the AmmoSystem script

    private void Start()
    {
        // Get the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Initialize the dictionary with reload sounds for each weapon
        weaponReloadSounds.Add(1, defaultReloadSound);
        weaponReloadSounds.Add(2, alternateReloadSound);

        // Get a reference to the AmmoSystem script
        ammoSystem = GetComponent<AmmoSystem>();

        if (ammoSystem == null)
        {
            Debug.LogError("AmmoSystem script not found on the same GameObject as PlayerShoot.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootInput?.Invoke();

        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
            if (ammoSystem != null && ammoSystem.CanReload())
            {
                PlayReloadSound();
                ammoSystem.Reload();
            }
        }

        // Switch between weapons with keys 1 and 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
        }
    }

    private void SwitchWeapon(int weapon)
    {
        if (weaponReloadSounds.ContainsKey(weapon))
        {
            currentWeapon = weapon;
            Debug.Log("Switched to Weapon " + currentWeapon);
        }
        else
        {
            Debug.LogWarning("Weapon " + weapon + " does not have a reload sound assigned.");
        }
    }

    private void PlayReloadSound()
    {
        // Check if the current weapon has a reload sound assigned and if the ammo is not full
        if (weaponReloadSounds.ContainsKey(currentWeapon) && ammoSystem != null && ammoSystem.CanReload())
        {
            AudioClip reloadSound = weaponReloadSounds[currentWeapon];

            // Create a temporary AudioSource component
            AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
            tempAudioSource.playOnAwake = false;
            tempAudioSource.clip = reloadSound;

            // Play the reload sound
            tempAudioSource.Play();

            // Destroy the temporary AudioSource component to clean up
            Destroy(tempAudioSource, reloadSound.length);
        }
        else
        {
            Debug.LogWarning("Current weapon does not have a reload sound assigned or ammo is full.");
        }
    }
}

