using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godzillaDoor : MonoBehaviour
{
    public GameObject door;
    public float doorOpenPosition = -0.68f; // The position to open the door along the x-axis
    public float doorClosePosition = 0f; // The original position of the door

    public float moveSpeed = 2.0f; // Speed at which the door moves
    public bool GuiOn;
    public bool godzillaout = false;

    Animator doorAnimator;
    [Space(10)]
    [Header("Toggle for the GUI on/off")]


    [Space(10)]
    [Header("The text to display on trigger")]
    public string Text = "Turn Back";

    [Space(10)]
    [Header("Display Options")]
    public bool displayOnce = false; // Variable to determine if the text should be displayed only once

    [Tooltip("This is the window Box's size. It will be mid screen. Add or reduce the X and Y to move the box in Pixels. ")]
    public Rect BoxSize = new Rect(0, 0, 200, 100);

    private bool playerInRange;
    private bool isDoorOpen;
   
    public GUISkin customSkin;

    private void Start()
    {
         doorAnimator = GetComponent<Animator>();
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

            GUI.Label(BoxSize, Text);

            GUI.EndGroup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            GuiOn = true; // Show GUI message asking to press 'E' to unlock the door
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            GuiOn = false; // Hide GUI message when the player exits the trigger area
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            isDoorOpen = !isDoorOpen;

            // Assuming the player is tagged as "Player"
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Enable the animator
                
                if (doorAnimator != null)
                {
                    // Trigger a specific animation (replace "AnimationName" with the actual animation trigger parameter)
                    doorAnimator.SetTrigger("open");
                    godzillaout = true;
                }
                if (doorAnimator = null)
                {
                    Debug.Log("animation not found");

                }
            }
        }
    }
}