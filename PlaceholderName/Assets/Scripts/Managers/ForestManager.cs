using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestManager : GenericSingletonClass_Forest<MonoBehaviour>
{
    public GameManager gameManager;
    //data
    public GameObject treeFather;
    public GameObject groundFather;
    public ForestSpawn[] forestSpawns;
    [SerializeField] private SpawnTime timeOfDay;
    [SerializeField] private List<GameObject> PlantsList = new List<GameObject>(); //stores all the plants spawned
    [SerializeField] private bool spawnedMorning;
    [SerializeField] private bool spawnedNoon;
    [SerializeField] private bool spawnedEvening;
    private bool spawnNow;
    public List<GameObject> plantsOnForest = new List<GameObject>();

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        spawnedMorning = false;
        spawnedNoon = false;
        spawnedEvening = false;
        spawnNow = true;
    }
    public void SpawnPlants()
    {
        //reset
        DestroyPlants();
        //Deside time of day
        if (gameManager.timeStateManager.currentTimeState == gameManager.timeStateManager.morningTimeState)
        {
            if (!spawnedMorning)
            {
                timeOfDay = SpawnTime.morning;
                spawnedMorning = true;
                spawnNow = true;
            }
        }
        else if (gameManager.timeStateManager.currentTimeState == gameManager.timeStateManager.noonTimeState)
        {
            if (!spawnedNoon)
            {
                timeOfDay = SpawnTime.noon;
                spawnedNoon = true;
                spawnNow = true;
            }
        }
        else if (gameManager.timeStateManager.currentTimeState == gameManager.timeStateManager.evneningTimeState)
        {
            if (!spawnedEvening)
            {
                timeOfDay = SpawnTime.evning;
                spawnedEvening = true;
                spawnNow = true;
            }
        }
        else
        {
            Debug.LogError("not a valid Spawn State");
        }

        if (spawnNow)
        {
            spawnNow = false;
            //Start the spawn
            int index = Random.Range(8, 12);//random amount of plants each time state
            Debug.Log("how much flowers? :" + index);
            for (int i = 0; i < index; i++)
            {
                GameObject plant = RollForPlant();

                if (plant)
                {
                    GameObject actualPlant = Instantiate(plant, this.transform); //choose a plant

                    //Give the plant a random place to spawn 
                    ForestSpawn thisSpawnParent = GetRandomParentSpawn(actualPlant.GetComponent<Plant>().growsOn);
                    //Give the plant a random spot to spawn on parent 
                    Vector3 spawnLocation = thisSpawnParent.GetRandomV3(); //try random spot on place
                    if (spawnLocation != Vector3.zero)
                    {
                        actualPlant.transform.position = spawnLocation;
                        plantsOnForest.Add(actualPlant);
                    }
                }
            }
        }
    }

    public ForestSpawn GetRandomParentSpawn(SpawnType spawnType)
    {
        int spawnIndex; //random parent
        spawnIndex = Random.Range(0, forestSpawns.Length - 1); //try random place
        if (forestSpawns[spawnIndex].isFull() || spawnType != forestSpawns[spawnIndex].spawnType) //spot is full on that parent OR not the correct spawn type
        {
            if (spawnIndex != 0) //start from 0
            {
                spawnIndex = 0;
            }
            while (forestSpawns[spawnIndex].isFull() || spawnType != forestSpawns[spawnIndex].spawnType) //iterate the arr to find an empty spot to spawn
            {
                spawnIndex++;
                if (spawnIndex == forestSpawns.Length) //no more places to spawn
                {
                    Debug.Log("No valid spawn parent was found");
                    return null;
                }
            }
        }
        return forestSpawns[spawnIndex];
    }

    public GameObject RollForPlant()
    {
        int cumulativeProbs = 0;
        int currentProbs = Random.Range(0, 100);
        //Debug.Log("CurrentPorb: " + currentProbs);
        for (int i = 0; i < PlantsList.Count; i++)
        {
            Plant plant = PlantsList[i].GetComponent<Plant>();
            if (timeOfDay == plant.spawnTime) {
                cumulativeProbs += plant.spawnChance;
               // Debug.Log("cumulative: " + cumulativeProbs);
                if (currentProbs <= cumulativeProbs)
                {
                    return PlantsList[i];
                }
            }
        }
        return null;
    }

    public void ResetDaySpawns()
    {
        spawnedMorning = false;
        spawnedNoon = false;
        spawnedEvening = false;
    }

    public void ActivePlants()
    {
        for (int i = 0; i < plantsOnForest.Count; i++)
        {
            if (plantsOnForest[i])
            {
                plantsOnForest[i].SetActive(true);
            }
        }
    }

    public void DeactivePlants()
    {
        for (int i = 0; i < plantsOnForest.Count; i++)
        {
            if (plantsOnForest[i])
            {
                plantsOnForest[i].SetActive(false);
            }
        }
    }

    public void DestroyPlants()
    {
        for (int i = 0; i < plantsOnForest.Count; i++)
        {
            if (plantsOnForest[i])
            {
                Destroy(plantsOnForest[i]);
            }
        }
        plantsOnForest.Clear();
    }
}
