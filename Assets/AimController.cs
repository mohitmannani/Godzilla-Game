using UnityEngine;
using UnityEditor;

public class AimController : MonoBehaviour
{
    public float normalFOV = 60f;
    public float aimFOV = 40f;
    public float aimSpeed = 10f;

    private bool isAiming = false;
    private GameObject grabbedObject;

    [System.Serializable]
    public class InteractableObject
    {
        public GameObject gameObject;
        public bool isEnabled = true; // This variable indicates whether the object is currently interactable
        public Vector3 rotationOnAim = new Vector3(0f, 45f, 0f); // Desired rotation when aiming
    }

    public InteractableObject[] interactableObjects;

    void Update()
    {
        // Check for right mouse button input
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            TryGrabObject();
        }
        else
        {
            isAiming = false;
            ReleaseObject();
        }

        // Adjust FOV
        float targetFOV = isAiming ? aimFOV : normalFOV;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);

        // Rotate the grabbed object when aiming
        if (grabbedObject != null)
        {
            RotateGrabbedObject(grabbedObject);
        }
    }

    void TryGrabObject()
    {
        // Check if any interactable object is within reach and can be grabbed
        foreach (var interactable in interactableObjects)
        {
            if (interactable.isEnabled && IsObjectWithinReach(interactable.gameObject))
            {
                grabbedObject = interactable.gameObject;
                break;
            }
        }
    }

    bool IsObjectWithinReach(GameObject obj)
    {
        // Example: Check if the object is within a certain distance (adjust as needed)
        float distanceThreshold = 2f;
        return Vector3.Distance(transform.position, obj.transform.position) < distanceThreshold;
    }

    void ReleaseObject()
    {
        grabbedObject = null;
    }

    void RotateGrabbedObject(GameObject obj)
    {
        // Rotate the grabbed object based on the specified rotation values
        obj.transform.rotation = Quaternion.Euler(grabbedObject.GetComponent<InteractableObject>().rotationOnAim);
    }
}
