using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;        // Reference to the target (marble) Transform
    public float smoothSpeed = 0.125f;    // Smoothing factor for camera movement
    public Vector3 offset;          // Offset position of the camera from the target

    void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = Player.position + offset;

        // Use SmoothDamp to gradually move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Make the camera look at the target
        transform.LookAt(Player);
    }
}
