using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private Transform orientation;

    [Header("Look Settings")]
    [SerializeField] private float sensX = 10f;
    [SerializeField] private float sensY = 10f;

    private float yRotation;
    private float xRotation;

    public Transform CameraHolder => _cameraHolder;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * 0.1f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 0.1f;

        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        _cameraHolder.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;

    // Start is called before the first frame update
    void StartRaycast()
    {
        // Assuming that PlayerLook script is attached to the same GameObject
        cam = GetComponent<PlayerLook>().CameraHolder.GetComponentInChildren<Camera>();

        // If PlayerLook script is on another GameObject, you might need to find it
        // Example: cam = GameObject.Find("PlayerLookGameObject").GetComponent<PlayerLook>().CameraHolder.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void UpdateRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
    }
}
