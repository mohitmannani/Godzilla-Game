using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem flameParticles;
    public AudioClip flameSound;
    public AudioSource audioSource;

    public float emissionDuration = 1.5f; // Duration of flame emission
    private bool isEmitting = false;

    private void Start()
    {
        if (flameParticles != null)
        {
            StartCoroutine(EmitFlames());
        }
        else
        {
            Debug.LogWarning("Particle System reference not set!");
        }

        if (audioSource != null && flameSound != null)
        {
            audioSource.clip = flameSound;
            audioSource.loop = false; // Ensure the sound doesn't loop
        }
        else
        {
            Debug.LogWarning("Audio Source or Sound Effect reference not set!");
        }
    }

    private IEnumerator EmitFlames()
    {
        isEmitting = true;

        while (isEmitting)
        {
            flameParticles.Play(); // Start emitting flames

            if (audioSource != null && flameSound != null)
            {
                audioSource.PlayOneShot(flameSound); // Play the sound effect once
            }

            yield return new WaitForSeconds(emissionDuration);

            flameParticles.Stop(); // Stop emitting flames

            yield return new WaitForSeconds(2f); // Wait for 2 seconds before next emission
        }
    }

    public void StopFlames()
    {
        isEmitting = false;
    }
}