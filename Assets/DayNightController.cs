using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
    [Header("Day-Night Settings")]
    public float daySpeed = 10.0f;
    public float nightSpeed = 2.0f;
    public float transitionSpeed = 1.0f;

    private Light sun;

    private void Start()
    {
        // Find the directional light (sun) in the scene
        sun = FindObjectOfType<Light>();

        if (sun == null)
        {
            Debug.LogError("No directional light found in the scene. Make sure you have a directional light to represent the sun.");
        }
    }

    private void Update()
    {
        // Press the 'L' key to toggle between day and night
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ToggleDayNight());
        }
    }

    private IEnumerator ToggleDayNight()
    {
        float targetIntensity = (sun.intensity > 0) ? 0 : 1; // Toggle between day (1) and night (0)

        while (Mathf.Abs(sun.intensity - targetIntensity) > 0.01f)
        {
            sun.intensity = Mathf.Lerp(sun.intensity, targetIntensity, Time.deltaTime * transitionSpeed);

            // Adjust rotation based on time of day
            float speed = (sun.intensity > 0) ? daySpeed : nightSpeed;
            transform.Rotate(Vector3.right, speed * Time.deltaTime);

            yield return null;
        }
    }
}
