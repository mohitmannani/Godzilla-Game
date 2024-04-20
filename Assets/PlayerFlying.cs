using UnityEngine;

public class PlayerFlying : MonoBehaviour
{
    public float flyingSpeed = 5.0f;
    public float ascendSpeed = 5.0f;
    public float descendSpeed = 10.0f;
    public float rotationSpeed = 60.0f;

    private bool isFlying = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFlying = !isFlying;
            rb.useGravity = !isFlying;
            Debug.Log("Flying: " + isFlying);
        }

        if (isFlying)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

            Vector3 moveForce = moveDirection * flyingSpeed;
            rb.AddForce(moveForce);

            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 ascendForce = Vector3.up * ascendSpeed;
                rb.AddForce(ascendForce);
            }
            else if (Input.GetKey(KeyCode.V))
            {
                // Apply a stronger downward force for faster descent
                Vector3 descendForce = Vector3.down * descendSpeed;
                rb.AddForce(descendForce);
            }

            float pitch = Input.GetAxis("Vertical");
            float yaw = Input.GetAxis("Horizontal");
            float roll = 0.0f;

            if (Input.GetKey(KeyCode.Q))
            {
                roll = -1.0f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                roll = 1.0f;
            }

            Vector3 rotation = new Vector3(pitch, yaw, roll) * rotationSpeed;
            rb.AddTorque(rotation);
        }
    }
}