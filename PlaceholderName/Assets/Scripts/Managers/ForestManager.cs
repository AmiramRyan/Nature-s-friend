using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestManager : GenericSingletonClass_Forest<MonoBehaviour>
{
    private GameManager gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    //data
    public GameObject treeFather;
    public GameObject groundFather;
    public ForestSpawn[] forestSpawns;
    [SerializeField] private SpawnTime timeOfDay;
    [SerializeField] private List<GameObject> PlantsList = new List<GameObject>(); //stores all the plants spawned
    
    public void SpawnPlants()
    {
        //Deside time of day
        if(gameManager.timeStateManager.currentTimeState == gameManager.timeStateManager.morningTimeState)
        {
            timeOfDay = SpawnTime.morning;
        }
        else if(gameManager.timeStateManager.currentTimeState == gameManager.timeStateManager.noonTimeState)
        {
            timeOfDay = SpawnTime.noon;
        }
        else if(gameManager.timeStateManager.currentTimeState == gameManager.timeStateManager.evneningTimeState)
        {
            timeOfDay = SpawnTime.evning;
        }
        else
        {
            Debug.LogError("not a valid Spawn State");
        }

        //Start the spawn
        int spawnIndex;
        int index = Random.Range(8, 15);
        for (int i = 0; i < index; i++) //random amount of plants each time state
        {
            GameObject plant = Instantiate(RollForPlant()); //choose a plant

            /*//Set Father Object
            if(plant.GetComponent<Plant>().growsOn == "tree")
            {
                plant.transform.SetParent(treeFather.transform);

            }
            else
            {
                plant.transform.SetParent(groundFather.transform);
            }*/

            //Give the plant a random place to spawn 
            spawnIndex = Random.Range(0, forestSpawns.Length); //try random place
            if (forestSpawns[spawnIndex].isFull()) //place is full 
            {
                if (spawnIndex != 0) //start from 0
                {
                    spawnIndex = 0;
                }
                while (forestSpawns[spawnIndex].isFull()) //iterate the arr to find an empty spot to spawn
                {
                    spawnIndex++;
                }
            }
            //Found a spawn
            Vector3 spawnLocation = forestSpawns[spawnIndex].GetRandomV3(); //generate random spawn based on space stats
            plant.transform.position = spawnLocation;
        }

    }

    public GameObject RollForPlant()
    {
        int cumulativeProbs = 0;
        int currentProbs = Random.Range(0, 100);
        for (int i = 0; i < PlantsList.Count; i++)
        {
            Plant plant = PlantsList[i].GetComponent<Plant>();
            if (timeOfDay == plant.spawnTime) {
                cumulativeProbs += plant.spawnChance;
                if (currentProbs <= cumulativeProbs)
                {
                    return PlantsList[i];
                }
            }
        }
        return null;
    }
}
