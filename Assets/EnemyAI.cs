using UnityEngine;

namespace BigRookGames.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        // --- Muzzle ---
        public GameObject muzzlePrefab;
        public GameObject muzzlePosition;

        // --- Config ---
        public bool autoFire;
        public float shotDelay = .5f;
        public bool rotate = true;
        public float rotationSpeed = .25f;

        // --- Options ---
        public GameObject target;
        public float chaseDistance = 10f;
        public float attackDistance = 5f;
        public float moveSpeed = 3f;
        public float turnSpeed = 2f;

        // --- Projectile ---
        [Tooltip("The projectile gameobject to instantiate each time the enemy attacks.")]
        public GameObject projectilePrefab;

        // --- Timing ---
        [SerializeField] private float shootTimer;
        public float shootInterval = 1f;

        private void Start()
        {
            shootTimer = 0f;
        }

        private void Update()
        {
            // --- If rotate is set to true, rotate the enemy in the scene ---
            if (rotate)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y
                                                                        + rotationSpeed, transform.localEulerAngles.z);
            }

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            // --- Check if the target is within attack range ---
            if (distanceToTarget <= chaseDistance)
            {
                if (distanceToTarget <= attackDistance)
                {
                    Shoot();
                }
                else
                {
                    Chase();
                }
            }
        }

        // --- Method to handle enemy chasing ---
        private void Chase()
        {
            Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

            Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
            transform.Translate(moveDirection, Space.World);
        }

        // --- Method to handle enemy shooting ---
        private void Shoot()
        {
            if (shootTimer <= 0f)
            {
                shootTimer = shootInterval;

                // --- Spawn muzzle flash ---
                Instantiate(muzzlePrefab, muzzlePosition.transform);

                // --- Shoot Projectile Object ---
                if (projectilePrefab != null)
                {
                    Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation);
                }
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }
        }
    }
}
