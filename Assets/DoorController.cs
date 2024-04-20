using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float doorOpenAngle = 90.0f;
    public float doorCloseAngle = 0.0f;
    public float openSpeed = 2.0f;


    private bool isDoorOpen = false;
    private Quaternion originalRotation;

    // New public variable for rotation axis
    public Transform rotationPivot; // Choose which part of the door to rotate
    public Vector3 rotationAxis = Vector3.up; // Default to rotating around the Y-axis

    private bool playerInRange = false; // Flag to track player proximity

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.E)&& playerInRange)
        {
            // Toggle the door's state
            isDoorOpen = !isDoorOpen;

            // Rotate the door manually based on the chosen axis
            float targetAngle = isDoorOpen ? doorOpenAngle : doorCloseAngle;
            StartCoroutine(RotateDoor(targetAngle));
        }
    }


    IEnumerator RotateDoor(float targetAngle)
    {
        float currentAngle = rotationPivot.rotation.eulerAngles.y;

        while (Mathf.Abs(currentAngle - targetAngle) > 0.01f)
        {
            rotationPivot.Rotate(rotationAxis, openSpeed * Mathf.Sign(targetAngle - currentAngle) * Time.deltaTime);
            currentAngle = rotationPivot.rotation.eulerAngles.y;
            yield return null;
        }

        // Ensure the door is exactly at the target angle
        rotationPivot.rotation = Quaternion.Euler(rotationPivot.rotation.eulerAngles.x, targetAngle, rotationPivot.rotation.eulerAngles.z);

        // If closing the door, reset to the original rotation

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Display a message or perform any other action when the player is near the door
            Debug.Log("Press 'E' to open/close the door.");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove the message or perform any other action when the player leaves the proximity
            Debug.Log("Player left the door's proximity.");
            playerInRange = false;

            if (!isDoorOpen)
            {
                rotationPivot.rotation = originalRotation;
            }
        }
    }
}
