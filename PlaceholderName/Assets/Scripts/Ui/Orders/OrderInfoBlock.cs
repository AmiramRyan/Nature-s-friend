using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderInfoBlock : MonoBehaviour
{
    //Data
    public  GenericOrder orderData; //passed On Setup
    public GameObject orderReqPrefab;
    public int ID;

    //Reffrances
    private GameManager gameManager;
    [SerializeField] private GameObject Image;
    [SerializeField] private TextMeshProUGUI RewardsContainer;
    [SerializeField] private GameObject RequiermentsContainer;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
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
        RewardsContainer.text = orderData.goldAmountWorth + "";
    }

    public void SetActive()
    {
        gameManager.orderManager.SetActiveOrder(orderData, orderData.costumerSprite, false);
    }

    public void CompleateOrder()
    {
        SetActive();
        if (gameManager.orderManager.CompleateOrder())
        {
            gameManager.uiManager.RemoveOrderFromList(ID);
            Destroy(gameObject); //Done
        }
    }

}
