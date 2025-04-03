using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //Declare Variables
    public float timeElapsed;
    public float score;
    public float wave;

    [SerializeField] private float secondsBetweenWaves;
    [SerializeField] private float helicoptersToSpawn;
    [SerializeField] private float turretsToSpawn;

    [SerializeField] private GameObject helicopterPrefab;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private List<Vector3> helicopterSpawnLocations;
    [SerializeField] private List<Vector3> turretSpawnLocations;

    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI waveUI;

    float ticks;
    float seconds;


    //toms cool counting 
    private int initialBuildingCount;
    private int currentBuildingCount;
    private float percentageRemaining;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(helicopterPrefab, new Vector3(Random.Range(-60, 60), Random.Range(15, 25), Random.Range(-60, 60)), new Quaternion());
        Instantiate(turretPrefab, new Vector3(Random.Range(-80, 80), Random.Range(20, 30), Random.Range(-80, 80)), new Quaternion());
        Invoke(nameof(InitializeBuildingCount), 0.1f);
    }

    void InitializeBuildingCount()
    {
        // Count all initial buildings
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        initialBuildingCount = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "building1(Clone)")
            {
                initialBuildingCount++;
            }
        }

        currentBuildingCount = initialBuildingCount;
        percentageRemaining = 100f;
        Debug.Log($"Initial buildings: {initialBuildingCount} (100%)");
    }

    void Update()
    {
        // Only track if we've initialized
        if (initialBuildingCount > 0)
        {
            int previousCount = currentBuildingCount;
            currentBuildingCount = CountCurrentBuildings();

            // Only update percentage if count changed
            if (currentBuildingCount != previousCount)
            {
                percentageRemaining = (float)currentBuildingCount / initialBuildingCount * 100f;
                Debug.Log($"Buildings remaining: {currentBuildingCount}/{initialBuildingCount} ({percentageRemaining:F1}%)");
            }
        }
    }

    int CountCurrentBuildings()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "building1(Clone)")
            {
                count++;
            }
        }

        return count;
    }

    // Public access to the current percentage
    public float GetPercentageRemaining()
    {
        return percentageRemaining;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Count up seconds
        ticks += 1;
        if(ticks >= 50)
        {
            ticks = 0;
            seconds += 1;
            timeElapsed += 1;
            score += 1;
        }

        //Spawn wave
        if(seconds >= secondsBetweenWaves)
        {
            seconds = 0;
            //Spawn enemies
            for(int i = 0; i < helicoptersToSpawn; i++)
            {
                //Instantiate(helicopterPrefab, helicopterSpawnLocations[Random.Range(0, helicopterSpawnLocations.Count)], new Quaternion());
                Instantiate(helicopterPrefab, new Vector3(Random.Range(-60,60), Random.Range(15, 25), Random.Range(-60, 60)), new Quaternion());
            }
            for (int i = 0; i < turretsToSpawn; i++)
            {
                //Instantiate(turretPrefab, turretSpawnLocations[Random.Range(0, turretSpawnLocations.Count)], new Quaternion());
                Instantiate(turretPrefab, new Vector3(Random.Range(-80, 80), Random.Range(20, 30), Random.Range(-80, 80)), new Quaternion());
            }
            //Change wave values
            wave += 1;
            secondsBetweenWaves += Random.Range(5, 10);
            helicoptersToSpawn += Random.Range(0, 2);
            turretsToSpawn += Random.Range(0, 2);
        }

        //Update Ui
        scoreUI.text = "City remaining: " + percentageRemaining + "%"; //was score
        waveUI.text = "Wave: " + wave;

    }
}
