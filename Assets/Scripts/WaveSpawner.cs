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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(helicopterPrefab, new Vector3(Random.Range(-60, 60), Random.Range(15, 25), Random.Range(-60, 60)), new Quaternion());
        Instantiate(turretPrefab, new Vector3(Random.Range(-80, 80), Random.Range(20, 30), Random.Range(-80, 80)), new Quaternion());
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
        scoreUI.text = "Score: " + score;
        waveUI.text = "Wave: " + wave;

    }
}
