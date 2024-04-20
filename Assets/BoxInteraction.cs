using UnityEngine;
using UnityEngine.UI;

public class BoxInteraction : MonoBehaviour
{
    public GameObject codeInputUI;
    public Text codeInputText;

    private bool isPlayerNear = false;
    private string correctCode = "1234"; // Change this to your desired 4-digit code

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Show the code input UI
            codeInputUI.SetActive(true);
        }
    }

    // Call this method when the player attempts to submit the code
    public void SubmitCode()
    {
        string enteredCode = codeInputText.text;

        if (enteredCode == correctCode)
        {
            Debug.Log("Code is correct! Box unlocked.");
            // Add logic to open the box or perform other actions
        }
        else
        {
            Debug.Log("Incorrect code. Try again.");
            // Add logic for incorrect code (e.g., display a message to the player)
        }

        // Hide the code input UI
        codeInputUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // Hide the code input UI when the player moves away from the box
            codeInputUI.SetActive(false);
        }
    }
}
