using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    //Declare Variables
    [SerializeField] private int cityRadius;
    [SerializeField] private int cityAmount;

    [SerializeField] private List<GameObject> buildings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < cityAmount; i++)
        {
            //Pick properties of the building
            GameObject buildingType = buildings[Random.Range(0, buildings.Count)];
            float size = Random.Range(1, 1.25f);
            Vector3 buildingScale = new Vector3(size, size, size);
            Color buildingColor = new Color(Random.Range(0.9f,1f), Random.Range(0.9f, 1f), Random.Range(0.9f, 1f), 1);

            //Get a random location, if it is not filled then try and spawn a building.
            float spawnX = Random.Range(-cityRadius, cityRadius + 1);
            float spawnZ = Random.Range(-cityRadius, cityRadius + 1);

            //Create a sphere to see if any other buildings are in it.
            Collider[] checkList = Physics.OverlapSphere(new Vector3(spawnX, 0, spawnZ), 2.51f);
            for (int j = 0; j < checkList.Length; j++)
            {
                if (checkList[j].gameObject.tag != "Building" && checkList[j].gameObject.tag != "Player")
                {
                    //Spawn a new building
                    GameObject newBuilding = Instantiate(buildingType, new Vector3(spawnX, 0 + (buildingScale.y / 2), spawnZ), new Quaternion());
                    newBuilding.transform.localScale = buildingScale;
                    newBuilding.gameObject.GetComponent<Renderer>().material.color = buildingColor;
                }
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
