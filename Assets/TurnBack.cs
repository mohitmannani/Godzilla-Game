using UnityEngine;

public class TurnBack : MonoBehaviour
{
    [Space(10)]
    [Header("Toggle for the GUI on/off")]
    public bool GuiOn;

    [Space(10)]
    [Header("The text to display on trigger")]
    public string Text = "Turn Back";

    [Space(10)]
    [Header("Display Options")]
    public bool displayOnce = false; // Variable to determine if the text should be displayed only once

    public AudioClip audioClip;
    private AudioSource audioSource;

    [Tooltip("This is the window Box's size. It will be mid screen. Add or reduce the X and Y to move the box in Pixels. ")]
    public Rect BoxSize = new Rect(0, 0, 200, 100);

    public GUISkin customSkin;

    // if this script is on an object with a collider display the GUI
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAudio(); // Stop any existing audio
            PlayAudio(); // Play the audio

            if (!displayOnce)
            {
                GuiOn = true;
            }
            else
            {
                Invoke("TurnOffGui", audioClip.length); // Turn off GUI after the audio clip length
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GuiOn = false;
            StopAudio(); // Stop the audio when the player exits the collider
        }
    }

    void OnGUI()
    {
        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn)
        {
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));

            GUI.Label(BoxSize, Text);

            GUI.EndGroup();
        }
    }

    // Method to play the audio clip
    void PlayAudio()
    {
        if (audioClip != null)
        {
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.PlayOneShot(audioClip);
        }
    }

    // Method to stop the audio clip
    void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Method to turn off the GUI after displaying the text once
    void TurnOffGui()
    {
        GuiOn = false;
    }
}
