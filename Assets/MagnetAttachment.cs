using UnityEngine;

public class MagnetAttachment : MonoBehaviour
{
    private bool magnetActive = false;
    [SerializeField] private float magnetStrength = 50f;
    [SerializeField] private Color[] colorOptions; // Array of colors to cycle through
    private int currentColorIndex = 0; // Index of the current color

    private Renderer magnetRenderer;

    private void Start()
    {
        magnetRenderer = GetComponent<Renderer>();
        UpdateMagnetColor();
    }

    private void UpdateMagnetColor()
    {
        magnetRenderer.material.color = colorOptions[currentColorIndex];
    }

    public void ToggleMagnet()
    {
        magnetActive = !magnetActive;
        if (magnetActive)
        {
            Debug.Log("Super Magnet is ON!");
        }
        else
        {
            Debug.Log("Super Magnet is OFF!");
        }
    }

    private void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colorOptions.Length; // Cycle through colors
        UpdateMagnetColor();
    }

    private void Update()
    {
        if (magnetActive && Input.GetMouseButtonDown(1)) // Check for right-click
        {
            ChangeColor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (magnetActive)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                Vector3 direction = transform.position - other.transform.position;
                direction.Normalize();
                rb.AddForce(direction * magnetStrength, ForceMode.Impulse);
            }
        }
    }
}


