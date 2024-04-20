using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Toggle muteToggle; // Reference to the Toggle in the Inspector
    public AudioSource backgroundMusic; // Reference to your background music AudioSource

    private void Start()
    {
        // Ensure the toggle reflects the current state of the audio
        muteToggle.isOn = !backgroundMusic.isPlaying;
    }

    public void ToggleMute()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
        else
        {
            backgroundMusic.Play();
        }
    }
}
