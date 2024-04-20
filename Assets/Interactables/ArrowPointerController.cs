using UnityEngine;

public class ArrowPointerController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float rotationSpeed = 5f; // Speed of rotation
    public float rotationDamping = 1f; // Damping effect for smoother rotation
    public float maxDistance = 10f; // Maximum distance at which the arrow responds to the target

    [Header("Arrow Settings")]
    public Vector3 arrowOffset = new Vector3(0f, 2f, 0f); // Offset from the player's position
    public Vector3 arrowRotation = new Vector3(0f, 0f, 0f); // Rotation relative to the player's rotation
    public Vector3 rotationAxis = Vector3.up; // Axis of rotation

    void Update()
    {
        // Check if playerTransform is assigned
        if (playerTransform != null)
        {
            // Perform a raycast to determine the target position
            RaycastHit hit;
            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, maxDistance))
            {
                // Rotate the arrow towards the raycast hit point
                RotateArrow(hit.point - playerTransform.position);

                // Set the arrow position with an offset from the raycast hit point
                SetArrowPosition(hit.point);
            }
        }
        else
        {
            Debug.LogError("PlayerTransform not assigned in ArrowPointerController!");
        }
    }

    void RotateArrow(Vector3 directionToTarget)
    {
        // Create a rotation to look at the target, keeping a specific axis fixed
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        // Smoothly rotate towards the target rotation with damping
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * rotationDamping * Time.deltaTime);

        // Apply rotation relative to the arrow's rotation and specified axis
        transform.Rotate(rotationAxis, arrowRotation.y, Space.Self);
    }

    void SetArrowPosition(Vector3 hitPoint)
    {
        // Set the arrow position with an offset from the raycast hit point
        transform.position = hitPoint + arrowOffset;
    }
}
