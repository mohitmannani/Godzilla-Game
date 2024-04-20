using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private bool atGate1 = true; // A flag to track the player's current location.

    // Detect if the player enters a gate trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            if (atGate1)
            {
                TeleportToGate2();
            }
            else
            {
                TeleportToGate1();
            }
        }
    }

    void TeleportToGate1()
    {
        // Teleport the player to Gate 1 position.
        transform.position = GameObject.FindWithTag("Gate1").transform.position;
        atGate1 = true;
    }

    void TeleportToGate2()
    {
        // Teleport the player to Gate 2 position.
        transform.position = GameObject.FindWithTag("Gate2").transform.position;
        atGate1 = false;
    }
}