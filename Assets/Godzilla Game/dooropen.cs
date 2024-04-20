using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropen : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("doorcollider")) // Replace "YourGameObjectTag" with the tag of the GameObject you want to trigger this
        {
            animator.SetInteger("state change", 1);
        }
    }
}
