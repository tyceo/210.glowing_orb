using UnityEngine;

public class LeobjectCheck : MonoBehaviour
{
    public GameObject replacementPrefab; // Assign your replacement prefab in inspector
    public Vector3 positionOffset = new Vector3(0f, 0f, 0f); // 48f, 0f, 1.5f
    public float sizeReductionFactor = 100f; // New variable to control size reduction

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Building"))
        {
            // Store the original object's scale
            Vector3 originalScale = transform.localScale;

            // Create position with offset
            Vector3 spawnPosition = transform.position;

            // Spawn with zero rotation and store the reference
            GameObject newObject = Instantiate(replacementPrefab, spawnPosition, Quaternion.identity);

            // Apply the original object's scale divided by 100 to the new object
            newObject.transform.localScale = originalScale / sizeReductionFactor;

            // Destroy the original tower
            Destroy(gameObject);
        }
    }
}



/*
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

 */