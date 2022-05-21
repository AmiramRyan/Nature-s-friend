using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerManager : GenericSingletonClass_Costumers<MonoBehaviour>
{
    //-----Referances----
    private GameManager gameManager;
    public int openingTime = 10; //time the store can start accepting costumers
    public int closingTime = 22; //time the store stops getting costumers
    public int[] workDays = { 0, 1, 2, 3, 4, 5 };
    //-----State variables-----
    public bool spawnActive;
    public Vector3 spawnPos;

    //-----Orders Bank-----
    public GenericOrder[] earthOrders;
    public GenericOrder[] waterOrders;
    public GenericOrder[] fireOrders;
    public GenericOrder[] windOrders;
    public GenericOrder[] storyOrders;
    private int storyOrdersIndex = 0;

    //----Costumers Bank-----
    [SerializeField]private List<GameObject> costumerQueForDay;
    public List<GameObject> costumerBank;
    public int stateSpawnIndex;
    public GameObject currentCostumer;
    private Costumer currCostumerReff;


    //list all story characters
    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        spawnActive = true;
    }

    public void SpawnRandomCostumer()
    {
        int rnd = Random.Range(0, costumerBank.Count);
        GameObject costumerToSpawn = costumerBank[rnd];
        GameObject objCostumer = Instantiate(costumerToSpawn, spawnPos, Quaternion.identity);
        currentCostumer = objCostumer;
        currentCostumer.GetComponent<Costumer>().possibleOrders[0].costumerSprite = currentCostumer.GetComponent<Costumer>().costumerSprite;
        Debug.Log("time pass check");
    }

    public void ClearDayData()
    {
        costumerQueForDay.Clear();
    }

    public void MakeCostumerLeave()
    {
        gameManager.orderManager.DisableChoiceBtns();
        gameManager.orderManager.ClearActiveOrder();
        currentCostumer.GetComponent<Costumer>().LeaveStore();
    }

    public void AcceptOrder()
    {
        //add to order book
        Debug.Log("order have been added to the orderbook");
        gameManager.uiManager.ordersList.Add(currentCostumer.GetComponent<Costumer>().possibleOrders[0]); //TOODO 0? SET IT UP!
        gameManager.orderManager.DisableChoiceBtns();
        //costumer leave
        currentCostumer.GetComponent<Costumer>().LeaveStore();
    }
}
