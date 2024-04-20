using UnityEngine;

public class RandomTornadoMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;

    [Header("Area Settings")]
    public Vector3 areaSize = new Vector3(10f, 0f, 10f);
    public Vector3 centerPosition = Vector3.zero;

    // Use Rigidbody for physics interactions
    private Rigidbody tornadoRigidbody;

    void Start()
    {
        // Set the initial position within the specified area
        transform.position = GetRandomPositionInArea();

        // Get or add Rigidbody component
        tornadoRigidbody = GetComponent<Rigidbody>();
        if (tornadoRigidbody == null)
        {
            tornadoRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        tornadoRigidbody.useGravity = false; // Disable gravity for the tornado
    }

    void Update()
    {
        MoveRandomly();
    }

    void MoveRandomly()
    {
        // Move the tornado randomly within the specified area
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        Vector3 newPosition = transform.position + randomDirection * moveSpeed * Time.deltaTime;

        // Clamp the position to stay within the specified area
        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, centerPosition.x - areaSize.x / 2, centerPosition.x + areaSize.x / 2),
            centerPosition.y,
            Mathf.Clamp(newPosition.z, centerPosition.z - areaSize.z / 2, centerPosition.z + areaSize.z / 2)
        );

        // Update the position and rotation of the tornado
        tornadoRigidbody.MovePosition(newPosition);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    Vector3 GetRandomPositionInArea()
    {
        // Get a random position within the specified area
        float randomX = Random.Range(centerPosition.x - areaSize.x / 2, centerPosition.x + areaSize.x / 2);
        float randomZ = Random.Range(centerPosition.z - areaSize.z / 2, centerPosition.z + areaSize.z / 2);

        return new Vector3(randomX, centerPosition.y, randomZ);
    }
}
