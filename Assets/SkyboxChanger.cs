using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private Material[] skyboxes;
    private int currentSkyboxIndex = 0;

    private void Update()
    {
        // Press '9' key to cycle through skyboxes
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeSkybox();
        }
    }

    private void ChangeSkybox()
    {
        // Make sure there are skyboxes assigned
        if (skyboxes.Length == 0)
        {
            Debug.LogWarning("No skyboxes assigned.");
            return;
        }

        // Increment the index and wrap around if necessary
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;

        // Apply the new skybox
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];

        // Optional: Notify or log the skybox change
        Debug.Log("Changed to Skybox " + currentSkyboxIndex);
    }
}
