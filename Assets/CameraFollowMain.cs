using UnityEngine;

public class CameraFollowMain : MonoBehaviour
{
    public Transform player;    // Reference to the player object.
    public float sensitivityX = 2.0f;   // Mouse sensitivity for horizontal rotation.
    public float sensitivityY = 2.0f;   // Mouse sensitivity for vertical rotation.
    public float minXAngle = -90.0f;    // Minimum vertical rotation angle.
    public float maxXAngle = 90.0f;     // Maximum vertical rotation angle.

    private float rotationX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Lock the cursor to center for FPS controls.
        Cursor.visible = false;                      // Hide the cursor.
    }

    void Update()
    {
        // Capture mouse input for camera rotation.
        float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        rotationX -= Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minXAngle, maxXAngle);

        // Apply the rotations to the camera holder.
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        // Follow the player's position.
        transform.position = player.position;
    }
}