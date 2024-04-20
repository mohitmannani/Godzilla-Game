using UnityEngine;
using System.Collections;

public class IPVelocityTrigger : MonoBehaviour
{
    [Tooltip("The orientation of the force.")]
    public ForceType forceType;
    [Tooltip("The number to multiply the velocity by.")]
    public float velocityFactor;

    void OnTriggerEnter(Collider other)
    {
        // Check if MKSceneManager.instance exists and is initialized
        if (MKSceneManager.instance == null)
        {
            Debug.LogWarning("MKSceneManager.instance is not initialized.");
            return;
        }

        if (MKSceneManager.instance.inputLocked || !enabled)
            return;

        Vector3 forceVector = Vector3.zero;

        if (other.attachedRigidbody && other.gameObject.name.Contains("IPPlayer"))
        {
            if (forceType == ForceType.RelativeToPad)
            {
                forceVector = transform.TransformDirection(Vector3.forward);
                forceVector = forceVector.normalized;
                forceVector *= velocityFactor;

                // Check if other.attachedRigidbody.velocity exists
                if (other.attachedRigidbody.velocity != null)
                {
                    Vector3 oldVelocity = other.attachedRigidbody.velocity;
                    Vector3 newVelocity = new Vector3(oldVelocity.x * forceVector.x, oldVelocity.y * forceVector.y, oldVelocity.z * forceVector.z);
                    other.attachedRigidbody.velocity = newVelocity;
                }
                else
                {
                    Debug.LogWarning("other.attachedRigidbody.velocity is null.");
                }
            }
            else
            {
                other.attachedRigidbody.velocity *= velocityFactor;
            }
        }
    }
}