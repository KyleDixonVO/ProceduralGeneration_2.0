using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityGenerator : MonoBehaviour
{

    public float cityX = 1000;
    public float cityZ = 1000;

    public float generatedDistX;
    public float generatedDistZ;

    public float streetMinSpacing = 2.0f;
    public float streetMaxSpacing = 8.0f;

    public float maxBuildingHeight = 100.0f;
    public float minBuildingHeight = 5.0f;

    public float maxBuildingWidth = 20;
    public float minBuildingWidth = 2;

    public GameObject cityOrigin;
    public GameObject building;

    public Slider MaxBuildingHeight;
    public Slider MinBuildingHeight;
    public Slider MaxBuildingWidth;
    public Slider MinBuildingWidth;
    public Slider StreetSpaceMax;
    public Slider StreetSpaceMin;

    public List<GameObject> buildings;
    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<GameObject>();
        GenerateBuildings();
        MaxBuildingHeight.value = maxBuildingHeight;
        MinBuildingHeight.value = minBuildingHeight;
        MaxBuildingWidth.value = maxBuildingWidth;
        MinBuildingWidth.value = minBuildingWidth;
        StreetSpaceMax.value = streetMaxSpacing;
        StreetSpaceMin.value = streetMinSpacing;
    }

    // Update is called once per frame
    void Update()
    {
        maxBuildingHeight = MaxBuildingHeight.value;
        minBuildingHeight = MinBuildingHeight.value;
        maxBuildingWidth = MaxBuildingWidth.value;
        minBuildingWidth = MinBuildingWidth.value;
        streetMaxSpacing = StreetSpaceMax.value;
        streetMinSpacing = StreetSpaceMin.value;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i] != null)
                {
                    Destroy(buildings[i]);
                }
            }

            buildings = new List<GameObject>();
            GenerateBuildings();
        }
    }

    public void GenerateBuildings()
    {
        generatedDistX = 0;
        generatedDistZ = 0;

        for (generatedDistX = 0; generatedDistX < cityX; generatedDistX++)
        {
            if (building != null)
            {
                generatedDistX += Random.Range(streetMinSpacing, streetMaxSpacing);
                generatedDistX += building.transform.localScale.x;
            }
            for (generatedDistZ = 0; generatedDistZ < cityZ; generatedDistZ++)
            {
                building = GameObject.CreatePrimitive(PrimitiveType.Cube);
                building.transform.position = new Vector3(cityOrigin.transform.position.x + generatedDistX, cityOrigin.transform.position.y, cityOrigin.transform.position.z + generatedDistZ);
                building.transform.localScale = new Vector3(Random.Range(minBuildingWidth, maxBuildingWidth), Random.Range(minBuildingHeight, maxBuildingHeight), Random.Range(minBuildingWidth, maxBuildingWidth));

                if (building.transform.localScale.y < 20)
                {
                    building.GetComponent<MeshRenderer>().material.color = Color.cyan;
                }
                else if (building.transform.localScale.y < 60)
                {
                    building.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else if (building.transform.localScale.y < 100)
                {
                    building.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else if (building.transform.localScale.y < 150)
                {
                    building.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    building.GetComponent<MeshRenderer>().material.color = Color.magenta;
                }

                generatedDistZ += building.transform.localScale.z;
                generatedDistZ += Random.Range(streetMinSpacing, streetMaxSpacing);
                buildings.Add(building);
            }
            buildings.Add(building);
        }
    }

}
