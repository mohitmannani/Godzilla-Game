using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    private GameObject pauseMenu; // Reference to your pause menu UI GameObject

    [SerializeField]
    private GameObject player; // Reference to your player GameObject

    private PlayerShoot playerShootScript; // Reference to the PlayerShoot script

    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("Pause Menu GameObject not assigned in the Inspector!");
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject not assigned in the Inspector!");
        }
        else
        {
            // Assuming the PlayerShoot script is directly attached to the player GameObject
            playerShootScript = player.GetComponent<PlayerShoot>();
            if (playerShootScript == null)
            {
                Debug.LogError("PlayerShoot script not found on the player GameObject!");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Check if the game is paused before handling input
        if (!isPaused)
        {
            // Your normal input handling goes here
            // For example, shooting logic
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;

        // Freeze the entire game by setting Time.timeScale to 0
        Time.timeScale = 0f;

        // Show your pause menu UI
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        // Disable the PlayerShoot script
        if (playerShootScript != null)
        {
            playerShootScript.enabled = false;
        }

        // Lock the cursor to prevent it from moving
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ResumeGame()
    {
        isPaused = false;

        // Unfreeze the game by setting Time.timeScale to 1
        Time.timeScale = 1f;

        // Hide your pause menu UI
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Enable the PlayerShoot script
        if (playerShootScript != null)
        {
            playerShootScript.enabled = true;
        }

        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Shoot()
    {
        // Your shooting logic goes here
        Debug.Log("Player shoots!");
    }

    public void OpenAnotherScene()
    {
        // Unpause the game before switching scenes
        Time.timeScale = 1f;

        // Load the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
