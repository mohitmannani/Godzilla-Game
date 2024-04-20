using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change "Player" to the tag of your player GameObject
        {
            // Load the scene you want to transition to
            SceneManager.LoadScene("FreeRide");
        }
    }
}

