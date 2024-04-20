using UnityEngine;

public class DroneShootingPoint : MonoBehaviour
{
    [Header("Shooting Point Settings")]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    // Get the shooting point's position
    public Vector3 GetPosition()
    {
        return transform.position + positionOffset;
    }

    // Get the shooting point's rotation
    public Quaternion GetRotation()
    {
        return Quaternion.Euler(rotationOffset);
    }
}
