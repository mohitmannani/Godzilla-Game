using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    public Transform player;  // Assign the player's GameObject in the Inspector

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the NPC to the player
            Vector3 lookDirection = player.position - transform.position;

            // Ensure the NPC doesn't tilt up or down while looking at the player
            lookDirection.y = 0;

            if (lookDirection != Vector3.zero)
            {
                // Rotate the NPC to face the player
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}