using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godzillaMove : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of Godzilla's movement
    public float rotationPauseMin = 3f; // Minimum time before rotation
    public float rotationPauseMax = 7f; // Maximum time before rotation


    public float rotationSpeed = 50f;
   



    private bool isPaused = false;
   
    public GameObject targetToFollow;
    
    
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
            StartCoroutine(PerformRotation());

        rb = GetComponent<Rigidbody>();
    }

    

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);



    }

    private IEnumerator PerformRotation()
    {
        while (isFirstTime)
        {
            


                float rotationPauseTime = Random.Range(rotationPauseMin, rotationPauseMax);
                yield return new WaitForSeconds(rotationPauseTime);

                transform.Rotate(Vector3.up, 180f);
            
        }
    }

    private bool isFirstTime = true;

    private void Update()
    {

        if ( targetToFollow != null  )
        {
           
          
            MoveTowardsTarget();


        }
        if (isFirstTime)
        {
          
            MoveForward();


        }
    }
    


    private void MoveTowardsTarget()
    {

        Vector3 targetDirection = targetToFollow.transform.position - transform.position;

        // Ensure the targetDirection isn't zero (to avoid division by zero)
        if (targetDirection != Vector3.zero && !isFirstTime && targetDirection.magnitude >= 6f )
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
            isFirstTime = false;

            DamageCharacter(-1);


        }
    }


    public void DamageCharacter(int damage)
    {
        Debug.Log("value of damage = " + damage);

        if (characterHealth != null)
        {
            Debug.Log("-10 health");
            characterHealth.UpdateHealth(damage); // Pass the damage value to the UpdateHealth method
        }





    }







}