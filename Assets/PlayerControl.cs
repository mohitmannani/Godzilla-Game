using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject magnetAttachment; // Reference to the magnet attachment GameObject
    private MagnetAttachment magnetScript; // Reference to the MagnetAttachment script

    private void Start()
    {
        magnetScript = magnetAttachment.GetComponent<MagnetAttachment>();
    }

    private void Update()
    {
        // Check for right mouse click (mouse button index 1)
        if (Input.GetMouseButtonDown(1))
        {
            // Toggle the magnet on/off
            magnetScript.ToggleMagnet();
        }
    }
}
