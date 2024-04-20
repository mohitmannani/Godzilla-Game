

using UnityEngine;

public class RemoveAnkleGrabber : MonoBehaviour
{
    [SerializeField] private GameObject ankleGrabberPrefab;

    private void Start()
    {
        // Check if the ankleGrabberPrefab is assigned
        if (ankleGrabberPrefab == null)
        {
            Debug.LogError("AnkleGrabber prefab is not assigned!");
            return;
        }

        // Find all instances of the AnkleGrabber prefab in the scene
        GameObject[] ankleGrabbers = GameObject.FindGameObjectsWithTag("AnkleGrabber");

        // Iterate through each instance and destroy it if it matches the prefab
        foreach (var ankleGrabber in ankleGrabbers)
        {
            if (ankleGrabberPrefab == ankleGrabber)
            {
                Destroy(ankleGrabber);
            }
        }
    }
}
