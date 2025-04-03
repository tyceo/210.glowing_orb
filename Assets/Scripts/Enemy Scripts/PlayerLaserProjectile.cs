using UnityEngine;

public class PlayerLaserProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float moveSpeed = 50f;
    public float damage = 10f;
    [SerializeField] private LayerMask ignoreLayers;
    [SerializeField] private float lifetime = 5f;

    private Camera playerCamera;
    private Rigidbody rb;

    void Start()
    {
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        // Initialize projectile direction
        Vector3 launchDirection = GetCameraCenterDirection();
        transform.forward = launchDirection;

        // Set up physics
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Destroy after lifetime expires
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        // Move forward using physics
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    private Vector3 GetCameraCenterDirection()
    {
        // Create a ray from the camera through its center
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return ray.direction;
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

    // Optional: Draw debug line to show projectile path
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
    }
}
