using UnityEngine;

public class PlayerLaserProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float moveSpeed = 50f;
    public float damage = 10f;
    [SerializeField] private LayerMask ignoreLayers;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private string spawnPointName = "GameObject"; // Name of your spawn point GameObject

    private Transform spawnPoint;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Automatically find the spawn point by name
        if (GameObject.Find(spawnPointName) != null)
        {
            spawnPoint = GameObject.Find(spawnPointName).transform;
        }
        else
        {
            Debug.LogError($"Spawn Point named '{spawnPointName}' not found in scene!");
        }
    }

    void Start()
    {
        if (spawnPoint != null)
        {
            // Set position and rotation to match spawn point
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }

        // Set up physics
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Destroy after lifetime expires
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (spawnPoint != null)
        {
            // Update direction continuously in case player rotates
            transform.forward = spawnPoint.forward;
        }

        // Move forward using physics
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with player and other projectiles
        if (other.CompareTag("Player") || other.CompareTag("PlayerProjectile"))
        {
            return;
        }

        // Handle enemy projectile collisions
        if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        // Handle environment collisions
        if (other.CompareTag("Ground") || other.CompareTag("Building"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
    }
}
