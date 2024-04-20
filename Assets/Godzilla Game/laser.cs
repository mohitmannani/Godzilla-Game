using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _maxLength;
    [SerializeField] private Vector3 _rayStartingPoint;
    public LayerMask layerMask; // Define the layer mask for raycasting
    public Image bloodSplashImage; // Reference to the blood splash UI image
    public float bloodSplashDuration = 1.0f; // Duration to display the blood splash in seconds
    public GameObject godzilla; // Reference to the "Godzilla" GameObject
    public float maxDistance = 10.0f; // Adjust this value as needed
    public GameObject explosionPrefab;
    public ParticleSystem flameParticles;
    public int maxHealth = 100;
    public int currentHealth;
    public float delayBeforeDestroy = 3.0f;
    public Healthbar healthBar;


    private void Start()
    {
        godzilla = GameObject.FindGameObjectWithTag("Godzilla"); // Make sure to tag your "Godzilla" object as "Godzilla"

        _beam.enabled = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }



    private void Awake()
    {
        _beam.enabled = false;
    }

    private void Activate()
    {
        _beam.enabled = true;
    }

    private void Deactivate()
    {
       _beam.enabled = false;
    //    _beam.SetPosition(0, _rayStartingPoint);
   //     _beam.SetPosition(1, _rayStartingPoint);
    }
    private bool isPaused = false;

    private void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            Activate();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Deactivate();
        }



    }
  



    private void FixedUpdate()
    {
        if (!_beam.enabled)
        {
            return;
        }
      //  IsPlayerCloseToGodzilla();
        CastRayFromFixedPoint();
    }

    private void CastRayFromFixedPoint()
    {
        Vector3 direction = transform.position - _rayStartingPoint; // Direction from the starting point to the transform

        Ray ray = new Ray(_rayStartingPoint, direction.normalized); // Normalized direction

        bool cast = Physics.Raycast(ray, out RaycastHit hit, _maxLength);

        if (cast && (hit.collider.CompareTag("Godzilla") || IsPlayerCloseToGodzilla()))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject);
            TakeDamage(1);
        }
    }

    private bool IsPlayerCloseToGodzilla()
    {
        if (godzilla != null)
        {
            Vector3 playerPosition = transform.position;
            Vector3 godzillaPosition = godzilla.transform.position;
            Vector3 direction = godzillaPosition - playerPosition;

            RaycastHit hit;

            if (Physics.Raycast(playerPosition, direction, out hit, maxDistance, layerMask))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                return true;
            }
        }

        return false;
    }



    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            if (explosionPrefab != null)
            {
                // Create an explosion at the "Godzilla" object's position
                explosionPrefab.SetActive(true);
                flameParticles.Play();
                StartCoroutine(DestroyAfterDelay(godzilla, 1.0f));
                
            }

           // Destroy(godzilla); // Destroy the "Godzilla" object when health reaches zero or less
        }
        
    }
    private IEnumerator DestroyAfterDelay(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if the object is still available before attempting to destroy it
        if (targetObject != null)
        {
            Destroy(targetObject);
           // Destroy(godzilla);
        }
    }
}