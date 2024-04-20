/*=========================================================
	PARTICLE PRO FX volume one 
	PPFXPhysicForce.cs
	
	Add Rigidbody force to explosions.
	
	(c) 2014
=========================================================*/

using UnityEngine;
using System.Collections;

public class PPFXPhysicForce : MonoBehaviour
{
    public float radius = 10f;
    public float force = 10f;
    public float delay = 0.2f;

    Collider[] colliders;

    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(delay);

        colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            // Check if the collider is still valid
            if (colliders[i] == null)
                continue;

            // Attempt to get the Rigidbody component
            var _rb = colliders[i].GetComponent<Rigidbody>();

            // Check if the Rigidbody is still valid
            if (_rb != null)
            {
                _rb.AddExplosionForce(force, transform.position, radius, new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3)), ForceMode.Impulse);
            }
        }

        yield return null;
    }
}
