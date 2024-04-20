using UnityEngine;

public class BrightnessController : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private float brightnessChangeAmount = 0.1f;

    private float originalIntensity;

    private void Start()
    {
        if (directionalLight != null)
        {
            // Store the original intensity
            originalIntensity = directionalLight.intensity;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.O)) // Press 'I' and 'O' together
        {
            ResetBrightness();
        }
        else if (Input.GetKeyDown(KeyCode.I)) // Press 'I' to increase brightness
        {
            AdjustBrightness(brightnessChangeAmount);
        }
        else if (Input.GetKeyDown(KeyCode.O)) // Press 'O' to decrease brightness
        {
            AdjustBrightness(-brightnessChangeAmount);
        }
    }

    private void AdjustBrightness(float amount)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity += amount;

            // Clamp intensity to a reasonable range (optional)
            directionalLight.intensity = Mathf.Clamp(directionalLight.intensity, 0f, 8f);
        }
    }

    private void ResetBrightness()
    {
        if (directionalLight != null)
        {
            // Reset the intensity to its original value
            directionalLight.intensity = originalIntensity;
        }
    }
}
