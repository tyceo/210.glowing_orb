using UnityEngine;

public class PlayerBeamProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float moveSpeed = 50f;
    public float damage = 10f;
    [SerializeField] private string spawnPointName = "GameObject"; // Default name for spawn point

    private Transform spawnPoint;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.excludeLayers = 6; // Keep your layer exclusion

        // Find spawn point automatically
        GameObject spawnObj = GameObject.Find(spawnPointName);
        if (spawnObj != null)
        {
            spawnPoint = spawnObj.transform;
        }
        else
        {
            Debug.LogError($"Spawn point '{spawnPointName}' not found!");
        }
    }

    void Start()
    {
        if (spawnPoint != null)
        {
            // Set initial position and rotation to match spawn point
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }

    void FixedUpdate()
    {
        if (spawnPoint != null)
        {
            // Update direction continuously to follow spawn point rotation
            transform.forward = spawnPoint.forward;
        }

        // Move forward using physics
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If colliding with enemy projectile, destroy both
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        // Destroy when hitting environment
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Building") || collision.gameObject.CompareTag("Broken") || collision.gameObject.CompareTag("Broken1"))
        {
            Destroy(gameObject);
        }
    }
}
