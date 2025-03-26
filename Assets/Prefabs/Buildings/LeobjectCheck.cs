using UnityEngine;

public class LeobjectCheck : MonoBehaviour
{

    public GameObject replacementPrefab; // Assign your replacement prefab in inspector
    public Vector3 positionOffset = new Vector3(48f, 0f, 1.5f);

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Building"))
        {
            // Create position with offset
            Vector3 spawnPosition = transform.position + positionOffset;

            // Spawn with zero rotation (Quaternion.identity means no rotation)
            Instantiate(replacementPrefab, spawnPosition, Quaternion.identity);

            // Destroy the original tower
            Destroy(gameObject);
        }
        
    }
}
