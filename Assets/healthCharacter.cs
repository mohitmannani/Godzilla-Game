using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthCharacter : MonoBehaviour
{
    public Text healthText;
    public int maxHealth = 100;
    public int currentHealth;
    public GUISkin customSkin;

  
   

    [Tooltip("This is the window Box's size. It will be mid screen. Add or reduce the X and Y to move the box in Pixels. ")]
    public Rect BoxSize = new Rect(0, 0, 200, 100);


    public bool GuiOn = false; // Toggle for GUI display

    [Space(10)]
    [Header("The text to display on trigger")]
    public string gameOverText = "Game Over"; // Text to display when game over

    void Start()
    {
        currentHealth = maxHealth;

       // UpdateHealthUI();
        Debug.Log("Start function called. Health initialized to: " + currentHealth);
    }


    // Function to update the health value and the UI display
   

    public void UpdateHealth(int healthChange)
    {
        currentHealth += healthChange;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp the health value
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GuiOn = true;

            Debug.Log("gui on");
            Time.timeScale = 0; // Freeze the game
        }
    }
    void UpdateHealthUI()
    {

        healthText.text = "Health: " + currentHealth + "%";

        Debug.Log("Current Health: " + currentHealth);



    }


    void OnGUI()
    {
        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn)
        {
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));

            GUI.Label(BoxSize, gameOverText);

            GUI.EndGroup();
        }
    }

}
