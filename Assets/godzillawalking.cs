using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godzillawalking : MonoBehaviour

{
    public float moveSpeed = 2f; // Speed of Godzilla's movement
    public float rotationPauseMin = 3f; // Minimum time before rotation
    public float rotationSpeed = 50f;
    public float rotationPauseMax = 7f; // Maximum time before rotation
    private bool isPaused = false;
    private godzillaDoor doorScript; // Reference to the godzillaDoor script
    public GameObject targetToFollow;
   public GameObject doorObject;
    private bool isFirstTime = true;
    private Rigidbody rb;
  

    public healthCharacter characterHealth;

    private void Start()
    {
        Time.timeScale = 1.0f;
        characterHealth = GetComponent<healthCharacter>();

        if (characterHealth == null)
        {
            Debug.LogError("healthCharacter script not found!");
        }


        if (doorObject != null)
        {
            // Get the godzillaDoor script attached to the door object
            doorScript = doorObject.GetComponent<godzillaDoor>();
        }


        
             StartCoroutine(PerformRotation());
          
        rb = GetComponent<Rigidbody>();

        // Find the GameObject with the godzillaDoor script attached
        //  GameObject doorObject = GameObject.Find("Door"); // Replace with your actual door object name


        


    }

    private void Update()
    {
       
        if (doorScript != null && doorScript.godzillaout && targetToFollow != null)
        {
            if (isFirstTime)
            {
                isFirstTime = false;

                // Position the GameObject
                transform.position = new Vector3(123.139999f, 0.150000006f, 236.130005f);

                // Set the rotation
                transform.rotation = Quaternion.Euler(0, 90f, 0);

                // Move forward in the GameObject's local space by 10 units
                transform.Translate(Vector3.forward * 15f);
            }

            MoveTowardsTarget();

        }
        else
        {
            MoveForward();
            

        }
    }



    private void MoveTowardsTarget()
    {
        Vector3 targetDirection = targetToFollow.transform.position - transform.position;

        // Ensure the targetDirection isn't zero (to avoid division by zero)
        if (targetDirection != Vector3.zero && targetDirection.magnitude >= 2f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Normalize the direction to have a magnitude of 1
            Vector3 movementDirection = new Vector3(targetDirection.x, 0f, targetDirection.z).normalized;

            // Multiply by the moveSpeed to control the movement
            Vector3 movement = movementDirection * moveSpeed * Time.deltaTime;

            // Modify the object's position
            transform.Translate(movement, Space.World);



        }
        else if (targetDirection != Vector3.zero && targetDirection.magnitude <= 2f)
        {

            DamageCharacter(-1);


        }
    }


    public void DamageCharacter(int damage)
    {
        Debug.Log("value of damage = "+ damage);

        if (characterHealth != null)
        {
            Debug.Log("-10 health");
            characterHealth.UpdateHealth(damage); // Pass the damage value to the UpdateHealth method
        }

        
         


    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }


    private IEnumerator PerformRotation()
    {
        while (true)
        {
            if (!isPaused && doorScript.godzillaout == false)
        
            {
                float rotationPauseTime = Random.Range(rotationPauseMin, rotationPauseMax);
                yield return new WaitForSeconds(rotationPauseTime);

                transform.Rotate(Vector3.up, 180f);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("RotateTag"))
        {
            Debug.Log("collider hit");
            transform.Rotate(Vector3.up, 180f);
        }
    }

   
}