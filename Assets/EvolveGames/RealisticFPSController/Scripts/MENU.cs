using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvolveGames
{
    public class MENU : MonoBehaviour
    {
        [Header("MENU")]
        [SerializeField] GameObject MenuPanel;
        [SerializeField] Animator ani;
        [SerializeField] PlayerController Player;
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
                    PauseGame();
                }
            }
        }

        void PauseGame()
        {
            MenuPanel.SetActive(true);
            Player.canMove = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            ani.SetBool("START", true);
            menu.SetActive(true);
            isPaused = true;
        }

        void ResumeGame()
        {
            MenuPanel.SetActive(false);
            Player.canMove = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            ani.SetBool("START", false);
            menu.SetActive(false);
            isPaused = false;
        }
    }
}


