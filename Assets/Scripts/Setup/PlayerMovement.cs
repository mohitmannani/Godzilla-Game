using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f; // Reduced walk speed
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float movementAcceleration = 10f; // Adjusted acceleration
    [SerializeField] private float movementDeceleration = 5f; // Added deceleration

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.4f;

    [Header("Drag")]
    [SerializeField] private float drag = 5f; // Adjusted drag

    [Header("Obstacle Destruction")]
    [SerializeField] private float destroyRange = 10f;
    [SerializeField] private int shotsToDestroy = 2;
    private int shotsCount = 0;

    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    private bool isCarrying = false;

    private void Start()
    {
        rb.freezeRotation = true;
    }

    public void SetCarrying(bool carrying)
    {
        isCarrying = carrying;
    }

    private void Update()
    {
        if (!isCarrying)
        {
            HandleMovementInput();

            // Check for jumping
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // Check for left mouse button clicks to destroy obstacles
            if (Input.GetMouseButtonDown(0))
            {
                DestroyObstacle();
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);
    }

    private void HandleMovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Smooth acceleration and deceleration
        Vector3 targetVelocity = moveDirection * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * movementAcceleration);

        // Apply drag to simulate deceleration
        rb.velocity *= 1f - drag * Time.deltaTime;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void DestroyObstacle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, destroyRange))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                // Obstacle hit, increment shot count
                shotsCount++;

                // Check if enough shots to destroy the obstacle
                if (shotsCount >= shotsToDestroy)
                {
                    Destroy(hit.collider.gameObject);
                    shotsCount = 0; // Reset shot count
                }
            }
        }
    }
}
