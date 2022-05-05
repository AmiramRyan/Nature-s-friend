using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerManager : MonoBehaviour
{
    //-----Referances----
    public ClockManager clockManager;
    public int openingTime = 10; //time the store can start accepting costumers
    public int closingTime = 22; //time the store stops getting costumers
    public int[] workDays = { 0, 1, 2, 3, 4, 5 };
    //-----State variables-----
    public int MaxOrdersPerDay = 3;
    public int MinOrdersPerDay = 1;
    public bool spawnActive;
    public Vector3 spawnPos;
    //-----Orders Bank-----
    public GenericOrder[] earthOrders;
    public GenericOrder[] waterOrders;
    public GenericOrder[] fireOrders;
    public GenericOrder[] windOrders;
    public GenericOrder[] storyOrders;
    private int storyOrdersIndex = 0;
    private int dailySpawnIndex = 0;
    //----Costumers Bank-----
    [SerializeField]private List<GameObject> costumerQueForDay;
    //----Debug costumers prefabs----
    public GameObject cos1;
    public GameObject cos2;
    public GameObject cos3;
    //list all story characters
    //TODO import the body parts bank
    private void OnEnable()
    {
        //ClockManager.onDayChange += NewDay;
        ClockManager.onTimeChange += SpawnCostumer;
        //debug REMOVETHIS
        costumerQueForDay.Add(cos1);
        costumerQueForDay.Add(cos2);
        costumerQueForDay.Add(cos3);
        spawnActive = true;

    }

    private void OnDisable()
    {
        //ClockManager.onDayChange -= NewDay;
        ClockManager.onTimeChange -= SpawnCostumer;
    }

    public void NewDay()
    {
        int costumersToSpawn = Random.RandomRange(MinOrdersPerDay, MaxOrdersPerDay);
        dailySpawnIndex = 0;
        /*for (int i = 0; i < costumersToSpawn; i++)
        {
            costumerQueForDay.Add(MakeCostumer());
            costumerQueForDay[i].SetAttributes(Sprite, newElement, newPossibleOrders, false);
        }*/

        //For Debug
        costumerQueForDay.Add(cos1);
        costumerQueForDay.Add(cos2);
        costumerQueForDay.Add(cos3);
        spawnActive = true;
    }

    public void ClearDayData()
    {
        costumerQueForDay.Clear();
    }

    /*public GameObject MakeCostumer() //TODO creats a random costumer from the bank
    {
        
    }*/

    public void SpawnCostumer()
    {
        if (spawnActive)
        {
            GameObject costumerToSpawn = costumerQueForDay[dailySpawnIndex];
            if (isTimeToSpawn(costumerToSpawn))
            {
                GameObject costumer = Instantiate(costumerToSpawn, spawnPos, Quaternion.identity);
                Debug.Log("time pass check");
                dailySpawnIndex++;
            }
            if(dailySpawnIndex == MaxOrdersPerDay - 1) //no more orders in que
            {
                spawnActive = false;
            }
        }
    }

    private bool isTimeToSpawn(GameObject costumerToSpawn)
    {
        Costumer tempCos = costumerToSpawn.GetComponent<Costumer>();
        if (tempCos.arriveHour == clockManager.GetHour() && Mathf.Approximately(tempCos.arriveMin,clockManager.GetMin())) //on time
        {
            if (tempCos.IsMainCharacter()) //main character on time
            {
                if (tempCos.arriveDay == clockManager.GetDay())
                {
                    return true; //main character on time and on day
                }
                else
                {
                    return false; //main character on time not on day
                }
            }
            else //not main character on time
            {
                return true;
            }
        }
        else // not on time
        {
            return false;
        }
    }
}
