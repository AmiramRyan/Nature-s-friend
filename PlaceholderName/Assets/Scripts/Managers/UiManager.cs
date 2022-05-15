using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("UiPanels")]
    public GameObject cauldronPanel;
    public GameObject bookPanel;
    public GameObject shelfStorageLeftUp;
    public GameObject shelfStorageLeftDown;
    public GameObject shelfStorageMiddleUp;
    public GameObject shelfStorageMiddleDown;
    public GameObject shelfStorageRightUp;
    public GameObject shelfStorageRightDown;
    public GameObject[] cauldronBtns;
    public GameObject orderBookBtn;

    [Header("Refrances")]
    public Image discoverImg;
    [SerializeField] private Sprite discoverImgDefault; //TOOD: move to a data manager
    [SerializeField] private GameObject cauldronShelfImgPrefab; //the image prefab for a shelf cauldron ingredient
    [SerializeField] private InventoryObj playerInventory; //the player scriptable obj inventory
    public List<GenericInventoryResource> selectedIngredientList = new List<GenericInventoryResource>(); //the ingredients currently selected (updates in runtime)
    private List<GameObject> selectedGameObjectList = new List<GameObject>(); //the ingredients currently selected GAME OBJECTS
    public List<GenericInventoryResource> inventoryIngredientsList = new List<GenericInventoryResource>(); //spread out the ingrideint from the inventory into a list
    public List<GameObject> tempIngredientGameobjectsList = new List<GameObject>(); // a refarance to the actual gameobjects renderd on screen
    public GameObject orderBookPrefab;
    public List<GenericOrder> ordersList; //orders register soon as you hit accept on an order
    public List<GameObject> ordersListOnUi; //the gameobjects renderd on the UI pulls from the ordersList and instantiate
    public GameObject OrdersBookContainer;
    [SerializeField] private GameObject requestPrefab;
    [SerializeField] private GameObject requestHolderBook;
    [SerializeField] private GameObject costumerManager;

    private void OnEnable()
    {
        ShelfItem.highlightStatusChanged += CheckForSelectedIngredients;
    }

    private void OnDisable()
    {
        ShelfItem.highlightStatusChanged -= CheckForSelectedIngredients;
    }

    #region PanelsControls
    public void ActivateUiPanel(string uiPanelToActivate) //activates the slected panel while closing others using the panels name string
    {
        switch (uiPanelToActivate)
        {
            case "cauldron":
                //fill up the panel shelfes
                FillPotionIngredient();
                ChangePredictedSprite(null);

                //activate correct panel
                cauldronPanel.GetComponent<CauldronScreen>().GoMiddle();
                SetPanels(true, false);
                SetCauldronBtn();
                break;

            case "orderBook":
                //fill the book of orders
                orderBookBtn.SetActive(true);
                for (int i = 0; i < ordersList.Count; i++)
                {
                    if (ordersList[i])
                    {
                        GameObject order = Instantiate(orderBookPrefab);
                        order.transform.position = new Vector3(order.transform.position.x, order.transform.position.y, 0);
                        order.transform.SetParent(OrdersBookContainer.transform);
                        order.GetComponent<OrderInfoBlock>().orderData = ordersList[i];
                        order.GetComponent<OrderInfoBlock>().ID = i;
                        ordersListOnUi.Add(order);
                    }
                }
                //activate correct panel
                OpenPanel(bookPanel);
                break;

            default:
                Debug.LogError("Ui manager could not find a panel with this name: " + uiPanelToActivate);
                break;
        }
    }

    private void SetPanels(bool isCauldronActive, bool isOrderBookActive) //[bool1 * bool2 * bool3 *.... bool n] -> set panels 1 - n active acording to the bools (n = num of panels in game) 
    {
        cauldronPanel.SetActive(isCauldronActive);
        bookPanel.SetActive(isOrderBookActive);
        //panelN.SetActive(boolN);
    }

    public void DisablePanels() //disable all game mini panels 
    {
        //disable all panels
        //Cauldron panel
        cauldronPanel.GetComponent<Animator>().SetTrigger("close");
        for (int i = 0; i < cauldronBtns.Length; i++)
        {
            cauldronBtns[i].SetActive(false);
        }
        cauldronPanel.SetActive(false);

        //order panel
        orderBookBtn.SetActive(false);
        bookPanel.SetActive(false);



        //clear ui elements
        ClearUiIngridientList();
        //ready all interactables
        CauldronInteractions.readyForInteraction = true;
    }

    public void SetCauldronBtn()
    {
        for (int i = 0; i < cauldronBtns.Length ; i++)
        {
            cauldronBtns[i].SetActive(true);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void CloseCauldronPanel()
    {
        StartCoroutine(ClosingAnimationCauldronPanelCo());
    }

    public void CloseOrderPanel()
    {
        StartCoroutine(ClosingAnimationBookPanelCo());
        //Arrange the order list to contain no null index
        List<GenericOrder> temp = new List<GenericOrder>();
        for (int i = 0; i < ordersList.Count; i++)
        {
            if (ordersList[i])
            {
                temp.Add(ordersList[i]);
            }
        }
        ordersList.Clear();
        ordersList = temp;
        for (int i = 0; i < ordersListOnUi.Count; i++)
        {
            Destroy(ordersListOnUi[i]);
        }
        ordersListOnUi.Clear();
    }

    public IEnumerator ClosingAnimationBookPanelCo()
    {
        
        bookPanel.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(0.4f);
        bookPanel.SetActive(false);
        orderBookBtn.SetActive(false);
    }

    public IEnumerator ClosingAnimationCauldronPanelCo()
    {
        for (int i = 0; i < cauldronBtns.Length; i++)
        {
            cauldronBtns[i].SetActive(false);
        }
        cauldronPanel.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(0.4f);
        //clear ui elements
        ClearUiIngridientList();
        cauldronPanel.SetActive(false);
        //ready all interactables
        CauldronInteractions.readyForInteraction = true;
    }

    public void RemoveOrderFromList(int ID)
    {
        ordersList.RemoveAt(ID);
    }

    #endregion

    #region Potions

    public void MakeIngredientsFall()
    {
        for (int i = 0; i < selectedGameObjectList.Count; i++)
        {
            Rigidbody2D temp = selectedGameObjectList[i].GetComponent<Rigidbody2D>();
            temp.bodyType = RigidbodyType2D.Dynamic;
            temp.AddForce(Vector2.up, ForceMode2D.Impulse);
        }
    } //TODO
    public void ChangePredictedSprite(GenericInventoryProduct product)
    {
        if (selectedIngredientList.Count >= 2)
        {
            discoverImg.enabled = true;
            if (product != null)
            {
                if (product.discoverd)
                {
                    discoverImg.sprite = product.itemSprite;
                }
                else
                {
                    discoverImg.sprite = discoverImgDefault;
                }
            }
            else
            {
                discoverImg.sprite = discoverImgDefault;
            }
        }
        else
        {
            discoverImg.enabled = false;
        }
    }
    public void CheckForSelectedIngredients()
    {
        for (int i = 0; i < tempIngredientGameobjectsList.Count; i++)
        {
            bool isHighlighted = tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().highlighted;
            bool isInsideTheList = selectedGameObjectList.Contains(tempIngredientGameobjectsList[i]);
            if (isHighlighted) //if the item is selected
            {
                if (!isInsideTheList) //if not already on the list
                {
                    selectedGameObjectList.Add(tempIngredientGameobjectsList[i]); //add it to the game objects list
                    selectedIngredientList.Add(tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().resourceData); //add it to the Resource list
                }
            }
            else //if the item is not selected
            {
                if (isInsideTheList) //if its in the list
                {
                    selectedGameObjectList.Remove(tempIngredientGameobjectsList[i]);
                    selectedIngredientList.Remove(tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().resourceData); //remove from the list
                }
            }
        }
    }
    public void ClearUiIngridientList()
    {
        for (int i = 0; i < tempIngredientGameobjectsList.Count; i++)
        {
            tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().DestroyItem(); //destroy the gameobject 
        }
        tempIngredientGameobjectsList.Clear();
        inventoryIngredientsList.Clear();
        selectedIngredientList.Clear();
        selectedGameObjectList.Clear();
    }
    private void FillPotionIngredient()
    {

        for (int i = 0; i < playerInventory.playerResources.Count; i++) //for each item
        {
            if (playerInventory.playerResources[i].numInInv != 0) //there IS such resource
            {
                for (int j = 0; j < playerInventory.playerResources[i].numInInv; j++) //for each of the spesific item
                {
                    inventoryIngredientsList.Add(playerInventory.playerResources[i]); //add it to the temp list
                }
            }
        }

        //set up the shelf with the temp list
        int itemNum = 1;
        for (int i = 0; i < inventoryIngredientsList.Count; i++)
        {
            if (inventoryIngredientsList[i].thisResourceType == ResourceType.plant) //only instantiate room for plants
            {
                GameObject tempIngredientRef = Instantiate(cauldronShelfImgPrefab, transform.position, Quaternion.identity) as GameObject;
                tempIngredientRef.GetComponent<ShelfItem>().InitiateItem(inventoryIngredientsList[i]);
                tempIngredientGameobjectsList.Add(tempIngredientRef);
                if (itemNum <= 10) //left top shelf
                {
                    tempIngredientRef.transform.SetParent(shelfStorageLeftUp.transform, false);
                }
                else if (itemNum <= 20) //left down shelf
                {
                    tempIngredientRef.transform.SetParent(shelfStorageLeftDown.transform, false);
                }
                else if (itemNum <= 30) //middle up shelf
                {
                    tempIngredientRef.transform.SetParent(shelfStorageMiddleUp.transform, false);
                }
                else if (itemNum <= 40) //middle down shelf
                {
                    tempIngredientRef.transform.SetParent(shelfStorageMiddleDown.transform, false);
                }
                else if (itemNum <= 50) //right up shelf
                {
                    tempIngredientRef.transform.SetParent(shelfStorageRightUp.transform, false);
                }
                else if (itemNum <= 60) //right down shelf
                {
                    tempIngredientRef.transform.SetParent(shelfStorageRightDown.transform, false);
                }
                else
                {
                    Debug.Log("No More Room For Me :(");
                }
                itemNum++;
            }
        }
    }

    #endregion

}
