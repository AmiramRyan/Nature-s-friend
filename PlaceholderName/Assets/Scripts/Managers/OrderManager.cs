using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private Text title; //order title
    [SerializeField] private Text description; //order description
    [SerializeField] private GameObject requestHolder; //hold all the requests
    [SerializeField] private List<GenericOrder> ordersList; //for random debug, TODO take the right(randomly?) order based on the character asking for it
    [SerializeField] private GameObject requestPrefab;
    public void SetActiveOrder()
    {
        int rnd = Random.Range(0, ordersList.Count);
        var currentOrder = ordersList[rnd];
        title.text = currentOrder.title;
        description.text = currentOrder.description;
        for(int i = 0; i< currentOrder.OrderRequests.Count; i++)
        {
            var thisRequest = currentOrder.OrderRequests[i];
            GameObject req =Instantiate(requestPrefab) as GameObject;
            req.transform.SetParent(requestHolder.transform);
            req.GetComponent<Request>().SetUpRequest(thisRequest.theResource.resourceSprite, thisRequest.amount);
        }

    }
}
