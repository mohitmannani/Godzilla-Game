using UnityEngine;

public class DustStormController : MonoBehaviour
{
    [SerializeField]
    private AudioClip dustStormSound;

    private AudioSource audioSource;

    [SerializeField]
    private bool isLooping = true;

    [SerializeField]
    private float minDistance = 1f;

    [SerializeField]
    private float maxDistance = 10f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = dustStormSound;
        audioSource.loop = isLooping;

        // Set spatial blend to 3D
        audioSource.spatialBlend = 1f;

        // Set min and max distances
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;

        audioSource.Play();
    }

    void Update()
    {
        // Toggle audio loop on/off when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLooping = !isLooping;
            audioSource.loop = isLooping;
            Debug.Log("Audio Loop: " + (isLooping ? "Enabled" : "Disabled"));
        }
    }
}
