using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pause : MonoBehaviour
{
    public Laser laserScript;
    [Header("MENU")]
    [SerializeField] GameObject MenuPanel;
   
    [SerializeField] PlayerInput input;
    [Header("Input")]
    [SerializeField] KeyCode BackKey = KeyCode.Escape;

    private bool isPaused = false;

    public GameObject menu;

    

    private void Update()
    {
        if (Input.GetKeyDown(BackKey))
        {
            if (isPaused)
            {
                
                ResumeGame();
            }
            else
            {
                Vector3 mousePos = Input.mousePosition;
                Debug.Log("Mouse Position: " + mousePos);
                PauseGame();
            }
        }


        // Example: Get mouse position
       
      

        // Example: Check for mouse button click (left button)
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked");
            // Add your logic for left mouse button click here
        }

    }


    void PauseGame()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        laserScript.enabled = false;

        //input.enabled = false;
        menu.SetActive(true);
        isPaused = true;
    }

     void ResumeGame()
        {
            MenuPanel.SetActive(false);
        laserScript.enabled = true;
        Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            input.enabled = true;
             menu.SetActive(false);
            isPaused = false;
        }
    
}