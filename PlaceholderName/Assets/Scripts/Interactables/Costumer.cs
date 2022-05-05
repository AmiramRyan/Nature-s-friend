using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costumer : GenericInteractable
{
    public Sprite costumerSprite;
    private string elemnent; //element afillietion
    public GameObject orderManager;
    public GenericOrder[] possibleOrders;
    [SerializeField] private bool mainCharacter; //story base character -> true Random character -> false

    public override void OnEnable()
    {
        base.OnEnable();
        orderManager = GameObject.FindGameObjectWithTag("orderManager");
    }

    //---Only for main characters---
    public int arriveHour = 0;
    public int arriveMin = 0;
    public int arriveDay = 0;
    public override void InteractAction()
    {
        //get the order manager to set up an order object on screen
        orderManager.GetComponent<OrderManager>().SetActiveOrder(possibleOrders[0], costumerSprite , true);
    }

    public void SetAttributes(Sprite newSprite, string newElement, GenericOrder[] newPossibleOrders, bool newMainCharacter)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
        elemnent = newElement;
        possibleOrders = newPossibleOrders;
        mainCharacter = newMainCharacter;
    }

    public void SetAttributes(Sprite newSprite, string newElement, GenericOrder[] newPossibleOrders, bool newMainCharacter, int newArriveHour, int newArriveMin, int newArriveDay)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
        elemnent = newElement;
        possibleOrders = newPossibleOrders;
        mainCharacter = newMainCharacter;
        arriveHour = newArriveHour;
        arriveMin = newArriveMin;
        arriveDay = newArriveDay;
    }

    #region Get/Set Functions

    public string GetElement()
    {
        return elemnent;
    }

    public GenericOrder[] GetPossibleOrders()
    {
        return possibleOrders;
    }

    public bool IsMainCharacter()
    {
        return mainCharacter;
    }

    #endregion

}
