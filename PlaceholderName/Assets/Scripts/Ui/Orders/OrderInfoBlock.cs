using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderInfoBlock : MonoBehaviour
{
    //Data
    public  GenericOrder orderData; //passed On Setup
    public GameObject orderReqPrefab;
    public int ID;

    //Reffrances
    public OrderManager orderManager;
    public UiManager uiManager;
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject RewardsContainer;
    [SerializeField] private GameObject RequiermentsContainer;

    private void Start()
    {
        orderManager = GameObject.FindGameObjectWithTag("orderManager").GetComponent<OrderManager>();
        uiManager = GameObject.FindGameObjectWithTag("uiManager").GetComponent<UiManager>();
        SetUpInfoBlock();
    }

    public void SetUpInfoBlock()
    {
        //SetUp Size
        transform.position = new Vector3(transform.position.x, transform.position.y, 1000);
        transform.localScale = new Vector3(1, 1, 1);

        for (int j = 0; j < orderData.OrderRequests.Count; j++) //Requests
        {
            var thisRequest = orderData.OrderRequests[j];
            GameObject temp = Instantiate(orderReqPrefab);
            temp.transform.SetParent(RequiermentsContainer.transform, false);
            temp.GetComponent<Image>().sprite = orderData.OrderRequests[j].theOrderProduct.productSprite;
            temp.GetComponent<TextMesh>().text = "X " + orderData.OrderRequests[j].amount;
        }
        Image.GetComponent<Image>().sprite = orderData.costumerSprite; //Picture

        //SetUp rewards

    }

    public void SetActive()
    {
        orderManager.SetActiveOrder(orderData, orderData.costumerSprite, false);
    }

    public void CompleateOrder()
    {
        SetActive();
        if (orderManager.CompleateOrder())
        {
            uiManager.RemoveOrderFromList(ID);
            Destroy(gameObject); //Done
        }
    }

}
