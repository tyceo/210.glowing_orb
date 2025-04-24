using UnityEngine;

public class Eater : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attractionForce = 10f;
    [SerializeField] private float attractionRadius = 5f;
    [SerializeField] private float maxAttractionDistance = 0.1f;

    [Header("Visualization")]
    [SerializeField] private bool showGizmo = true;
    [SerializeField] private Color gizmoColor = Color.cyan;

    public Transform targetObject;

    private void FixedUpdate()
    {
        // Find all objects with the Broken1 tag within radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Broken1"))
            {
                AttractObject(col.attachedRigidbody);
            }
        }
        transform.position = targetObject.position;
    }

    private void AttractObject(Rigidbody rb)
    {
        if (rb == null) return;

        Vector3 direction = transform.position - rb.position;
        float distance = direction.magnitude;

        // Don't attract if already very close
        if (distance <= maxAttractionDistance) return;

        // Calculate force (stronger when farther away)
        float forceMagnitude = attractionForce * (distance / attractionRadius);
        Vector3 force = direction.normalized * forceMagnitude;

        // Apply force
        rb.AddForce(force);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, attractionRadius);
        }
    }
}