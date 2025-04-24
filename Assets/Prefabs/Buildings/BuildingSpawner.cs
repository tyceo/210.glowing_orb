using UnityEngine;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public int numberOfObjects = 20;
    public float minDistanceBetween = 10f;
    public int maxAttemptsPerObject = 30;
    public Vector3 spawnRotation = new Vector3(-90f, 0f, 0f); // Added rotation parameter


    void Start()
    {
        List<Vector3> validPositions = GenerateValidPositions();
        SpawnObjects(validPositions);
    }

    List<Vector3> GenerateValidPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        int attempts = 0;

        while (positions.Count < numberOfObjects && attempts < maxAttemptsPerObject * numberOfObjects)
        {
            Vector3 potentialPosition = new Vector3(
                Random.Range(-100f, 100f),
                0f,
                Random.Range(-100f, 100f)
            );

            if (IsPositionValid(potentialPosition, positions))
            {
                positions.Add(potentialPosition);
            }
            attempts++;
        }

        if (positions.Count < numberOfObjects)
        {
            Debug.LogWarning($"Only generated {positions.Count} positions out of {numberOfObjects} requested");
        }

        return positions;
    }

    bool IsPositionValid(Vector3 position, List<Vector3> existingPositions)
    {
        foreach (Vector3 existingPos in existingPositions)
        {
            if (Vector3.Distance(position, existingPos) < minDistanceBetween)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnObjects(List<Vector3> positions)
    {
        foreach (Vector3 pos in positions)
        {
            // Create rotation quaternion from Euler angles (-90, 0, 0)
            Quaternion rotation = Quaternion.Euler(spawnRotation);

            // Instantiate the object
            GameObject spawnedObject = Instantiate(objectPrefab, pos, rotation);

            // Generate random scale between 100 and 300
            float randomScale = Random.Range(100f, 250f);

            // Apply the random scale
            spawnedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
}