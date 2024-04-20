using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialogTrigger : MonoBehaviour
{
    [SerializeField] private string dialogSceneName = "DefaultDialogScene";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the specified dialog scene
            SceneManager.LoadScene(dialogSceneName);
        }
    }

    // Optionally, you can add a method to change the dialog scene dynamically
    public void SetDialogScene(string sceneName)
    {
        dialogSceneName = sceneName;
    }
}
