using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public InventoryObj myInventory;
    [SerializeField] private TextMeshProUGUI title; //order title
    [SerializeField] private TextMeshProUGUI description; //order description
    [SerializeField] private Image costumerPortrait;
    [SerializeField] private GameObject requestHolderLeft;
    [SerializeField] private GameObject requestHolderRight;
    //[SerializeField] private List<GenericOrder> ordersList; //for random debug, TODO take the right(randomly?) order based on the character asking for it
    [SerializeField] private GameObject requestPrefab;
    [SerializeField] private GameObject acceptBtn, rejectBtn;
    public List<GameObject> activeRequests;
    private GenericOrder activeOrder;

    public void SetActiveOrder(GenericOrder orderToSetup , Sprite newCostumerPortrait, bool newQuest)
    {
        ClearActiveOrder();
        //change the text and image
        var currentOrder = orderToSetup;
        title.text = currentOrder.title;
        description.text = currentOrder.description;
        costumerPortrait.sprite = newCostumerPortrait;
        costumerPortrait.enabled = true;

        //setup the products requierments
        for (int i = 0; i< currentOrder.OrderRequests.Count; i++)
        {
            var thisRequest = currentOrder.OrderRequests[i];
           
            if (i <= 1) //left side
            {
                GameObject req = Instantiate(requestPrefab, requestHolderLeft.transform.position, Quaternion.identity) as GameObject;
                req.transform.SetParent(requestHolderLeft.transform);
                req.GetComponent<Request>().SetUpRequest(thisRequest.theOrderProduct.productSprite, thisRequest.amount.ToString());
                activeRequests.Add(req);
            }
            else //right side
            {
                GameObject req = Instantiate(requestPrefab, requestHolderRight.transform.position, Quaternion.identity) as GameObject;
                req.transform.SetParent(requestHolderRight.transform);
                req.GetComponent<Request>().SetUpRequest(thisRequest.theOrderProduct.productSprite, thisRequest.amount.ToString());
                activeRequests.Add(req);
            }
            
        }
        if (newQuest)
        {
            acceptBtn.SetActive(true);
            rejectBtn.SetActive(true);
        }
        activeOrder = orderToSetup;
    }

    public void ClearActiveOrder()
    {
        //clear order data
        title.text = "";
        description.text = "";
        costumerPortrait.sprite = null;
        costumerPortrait.enabled = false;
        //clear order request list
        if (activeRequests != null)
        {
            for (int i = 0; i < activeRequests.Count; i++)
            {
                activeRequests[i].GetComponent<Request>().DestroyItem();
            }
            activeRequests.Clear();
        }
        activeOrder = null;
    }

    public void DisableChoiceBtns()
    {
        acceptBtn.SetActive(false);
        rejectBtn.SetActive(false);
    }

    public bool CanCompleateOrder(GenericOrder order) //can be used when the order book is called to activate/deactivate "Send Order" btn
    {
        Debug.Log("Making sure you have evrything");
        //if you have all the items you need in the bag return true
        for (int i = 0; i < order.OrderRequests.Count; i++)
        {
            int indexOfItem = FindIndexOfProduct(i, order);
            if(myInventory.playerProducts[indexOfItem].numInInv < order.OrderRequests[i].amount)
            {
                Debug.LogError("No item of this kind is found in inventory: " + activeOrder.OrderRequests[i].theInvProduct.itemName);
                return false;
            }
        }
        return true;
    }

    public bool CompleateOrder()
    {
        if (CanCompleateOrder(activeOrder))
        {
            //Remove items
            Debug.Log("Compleating Order....");
            for (int i = 0; i < activeOrder.OrderRequests.Count; i++)
            {
                //if its a resource
                int indexOfItem = FindIndexOfProduct(i, activeOrder);
                if(indexOfItem == 999)
                {
                    Debug.LogError("No item of this kind is found in inventory: " + activeOrder.OrderRequests[i].theInvProduct.itemName);
                    return false;
                }
                else 
                { 
                    myInventory.UesItem(myInventory.playerProducts[indexOfItem], activeOrder.OrderRequests[i].amount); //subtract the amount
                }
            }
            //clean up the order panel and remove from order book
            ClearActiveOrder();
            Debug.Log("Order Sent");
            return true;
        }
        return false;
    }

    public int FindIndexOfProduct(int indexOfRequest , GenericOrder order)
    {
        GenericRequest request = order.OrderRequests[indexOfRequest];
        string nameToFind = request.theInvProduct.itemName;
        for (int i = 0; i < myInventory.playerProducts.Count; i++)
        {
            if(myInventory.playerProducts[i].itemName == nameToFind)
            {
                return i;
            }
        }
        return 999;
    }

    /*public void SetActiveOrder()
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
            req.GetComponent<Request>().SetUpRequest(thisRequest.theProduct.productSprite, thisRequest.amount);
        }

    }*/
}
