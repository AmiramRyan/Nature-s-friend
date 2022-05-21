using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;


public class UiManager : GenericSingletonClass_UI<MonoBehaviour>
{
    public GameManager gameManager;
    public GameObject canvasObj;

    [Header("UiPanels")]
    [SerializeField] private GameObject cauldronPanel;
    [SerializeField] private GameObject bookPanel;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject ConfirmPanelCauldron;
    [SerializeField] private GameObject ConfirmPanelTransition;
    [SerializeField] private GameObject TaskPanel;
    public GameObject endPanel;


    [Header("PanelsComponents")]
    public GameObject shelfStorageLeftUp;
    public GameObject shelfStorageLeftDown;
    public GameObject shelfStorageMiddleUp;
    public GameObject shelfStorageMiddleDown;
    public GameObject shelfStorageRightUp;
    public GameObject shelfStorageRightDown;
    public GameObject[] cauldronBtns;
    public GameObject orderBookBtn;
    public GameObject goToShopBtn;
    public GameObject InventoryBookProductContainer;
    public GameObject InventoryBookIngredientContainer;
    public GameObject OrdersBookContainer;
    public GameObject fadePanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject cauldronShelfImgPrefab; //the image prefab for a shelf cauldron ingredient
    [SerializeField] private GameObject orderBookPrefab;
    [SerializeField] private GameObject inventoryBookPrefab;


    [Header("Refrances")]
    public Image discoverImg;
    [SerializeField] private Sprite discoverImgDefault; 
    public List<GenericInventoryResource> selectedIngredientList = new List<GenericInventoryResource>(); //the ingredients currently selected (updates in runtime)
    private List<GameObject> selectedGameObjectList = new List<GameObject>(); //the ingredients currently selected GAME OBJECTS
    public List<GenericInventoryResource> inventoryIngredientsList = new List<GenericInventoryResource>(); //spread out the ingrideint from the inventory into a list
    public List<GameObject> tempIngredientGameobjectsList = new List<GameObject>(); // a refarance to the actual gameobjects renderd on screen
    public List<GenericOrder> ordersList; //orders register soon as you hit accept on an order
    public List<GameObject> ordersListOnUi; //the gameobjects renderd on the UI pulls from the ordersList and instantiate
    public List<GameObject> ingredientListOnUi; //the gameobjects renderd on the UI pulls from the ingredientList and instantiate
    public List<GameObject> productListOnUi; //the gameobjects renderd on the UI pulls from the products and instantiate
    [SerializeField] private TextMeshProUGUI timeToBrewText;
    public GameObject cauldron;

    private void OnEnable()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvasObj.GetComponent<Canvas>().worldCamera = cam;
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        ShelfItem.highlightStatusChanged += CheckForSelectedIngredients;
    }

    private void OnDisable()
    {
        ShelfItem.highlightStatusChanged -= CheckForSelectedIngredients;
    }

    #region PanelsControls
    public void ActivateUiPanel(string uiPanelToActivate) //activates the slected panel while closing others using the panels name string
    {
        GameManager.pauseTime?.Invoke();
        GameManager.StopPlayerMovement?.Invoke();
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
            case "taskPanel":
                //activate correct panel
                OpenPanel(TaskPanel);
                break;
                
            case "inventory":
                //fill the inventory book
                //ingredients
                for (int i = 0; i < gameManager.inventoryManager.playerInventory.playerResources.Count; i++)
                {
                    if (gameManager.inventoryManager.playerInventory.playerResources[i])  //if exist
                    { 
                        GameObject item = Instantiate(inventoryBookPrefab);
                        item.transform.SetParent(InventoryBookIngredientContainer.transform);
                        item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 1000);
                        item.GetComponent<Request>().SetUpRequest(gameManager.inventoryManager.playerInventory.playerResources[i].itemSprite, gameManager.inventoryManager.playerInventory.playerResources[i].numInInv.ToString());
                        ingredientListOnUi.Add(item);
                        item.GetComponent<ToolTipRequset>().ToolTipTextContainertext.text = gameManager.inventoryManager.playerInventory.playerResources[i].toolTipText; //set tool tip
                    }
                }
                //products
                for (int i = 0; i < gameManager.inventoryManager.playerInventory.playerProducts.Count; i++)
                {
                    if (gameManager.inventoryManager.playerInventory.playerProducts[i])
                    {
                        if (gameManager.inventoryManager.playerInventory.playerProducts[i].numInInv > 0)
                        {
                            GameObject item = Instantiate(inventoryBookPrefab);
                            item.transform.SetParent(InventoryBookProductContainer.transform);
                            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 1000);
                            item.GetComponent<Request>().SetUpRequest(gameManager.inventoryManager.playerInventory.playerProducts[i].itemSprite, gameManager.inventoryManager.playerInventory.playerProducts[i].numInInv.ToString());
                            productListOnUi.Add(item);
                        }
                    }
                }
                OpenPanel(InventoryPanel);
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
        GameManager.resumeTime?.Invoke();
        GameManager.ResumePlayerMovement?.Invoke();
        //disable all panels
        //Cauldron panel
        ConfirmPanelCauldron.SetActive(false);
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
        GameManager.StopPlayerMovement?.Invoke();
    }

    public void CloseCauldronPanel()
    {
        GameManager.resumeTime?.Invoke();
        GameManager.ResumePlayerMovement.Invoke();
        ConfirmPanelCauldron.SetActive(false);
        StartCoroutine(ClosingAnimationCauldronPanelCo());
    }

    public void OpenOrderPanel()
    {
        if (!bookPanel.activeInHierarchy && !InventoryPanel.activeInHierarchy)
        {
            ActivateUiPanel("orderBook");
        }
        else
        {
            CloseInventoryPanel();
        }
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
        GameManager.resumeTime?.Invoke();
        GameManager.ResumePlayerMovement?.Invoke();
    }

    public void OpenTaskPanel()
    {
        if (!bookPanel.activeInHierarchy && !InventoryPanel.activeInHierarchy)
        {
            ActivateUiPanel("taskPanel");

        }
    }

    public void CloseTaksPanel()
    {
        StartCoroutine(ClosingAnimationTaskPanelCo());
        GameManager.resumeTime?.Invoke();
        GameManager.ResumePlayerMovement.Invoke();
    }
    public void OpenConfirmPanel()
    {
        timeToBrewText.text = "Will finish at: " + (ClockManager.hour + gameManager.hoursConsumed) + ":" + (ClockManager.minute);
        ConfirmPanelCauldron.SetActive(true);

    }

    public void CloseConfirmPanel()
    {
        ConfirmPanelCauldron.SetActive(false);
    }

    public void OpenTransitionPanel()
    {
        if(!bookPanel.activeInHierarchy && !InventoryPanel.activeInHierarchy)
        {
            ConfirmPanelTransition.SetActive(true);
            GameManager.StopPlayerMovement?.Invoke();
            GameManager.pauseTime?.Invoke();
        }
    }

    public void CloseTransitionPanel()
    {
        ConfirmPanelTransition.SetActive(false);
        GameManager.ResumePlayerMovement?.Invoke();
        GameManager.resumeTime?.Invoke();
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

    public IEnumerator ClosingAnimationTaskPanelCo()
    {
        TaskPanel.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(0.4f);
        TaskPanel.SetActive(false);
    }

    public void RemoveOrderFromList(int ID)
    {
        ordersList.RemoveAt(ID);
    }

    public void OpenInventoryPanel()
    {
        if (!bookPanel.activeInHierarchy && !InventoryPanel.activeInHierarchy)
        {
            ActivateUiPanel("inventory");
        }
        else
        {
            CloseOrderPanel();
        }
    }

    public void CloseInventoryPanel()
    {
        //corutine
        //delete from screen
        for (int i = 0; i < ingredientListOnUi.Count; i++)
        {
            Destroy(ingredientListOnUi[i]);
        }
        for (int i = 0; i < productListOnUi.Count; i++)
        {
            Destroy(productListOnUi[i]);
        }
        ingredientListOnUi.Clear();
        productListOnUi.Clear();
        GameManager.resumeTime?.Invoke();
        GameManager.ResumePlayerMovement?.Invoke();
        StartCoroutine(ClosingAnimationInventoryPanelCo());
    }

    public IEnumerator ClosingAnimationInventoryPanelCo()
    {
        InventoryPanel.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(0.4f);
        //clear ui elements
        InventoryPanel.SetActive(false);
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

        for (int i = 0; i < gameManager.inventoryManager.playerInventory.playerResources.Count; i++) //for each item
        {
            if (gameManager.inventoryManager.playerInventory.playerResources[i].numInInv > 0) //there IS such resource
            {
                for (int j = 0; j < gameManager.inventoryManager.playerInventory.playerResources[i].numInInv; j++) //for each of the spesific item
                {
                    inventoryIngredientsList.Add(gameManager.inventoryManager.playerInventory.playerResources[i]); //add it to the temp list
                }
            }
        }

        //set up the shelf with the temp list
        int itemNum = 1;
        for (int i = 0; i < inventoryIngredientsList.Count; i++)
        {
            if (inventoryIngredientsList[i].thisResourceType == ResourceType.plant && inventoryIngredientsList[i].numInInv > 0) //only instantiate room for plants
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

    public void GotToShop()
    {
        StartCoroutine(FadeCoToShop());
    }
    public void GoToForest()
    {
        StartCoroutine(FadeCoToForest());
    }

    public IEnumerator FadeCoToForest()
    {

        //gameManager.timeStateManager.SwtichState(gameManager.timeStateManager.pauseTimeState); // switch to pauseTimeState
        gameManager.clockManager.TimePass(0, 30); //forward time
        CloseTransitionPanel();
        fadePanel.GetComponent<Animator>().SetTrigger("fadeIn");
        cauldron.SetActive(false);
        goToShopBtn.SetActive(true);
        gameManager.costumerManager.spawnActive = false;
        gameManager.forestManager.ActivePlants();
        fadePanel.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Forest"); //load the scene (after scene is loaded need to resume time)
    }
    public IEnumerator FadeCoToShop()
    {
        gameManager.clockManager.TimePass(0, 30);
        fadePanel.GetComponent<Animator>().SetTrigger("fadeIn");
        goToShopBtn.SetActive(false);
        gameManager.costumerManager.spawnActive = true;
        gameManager.forestManager.DeactivePlants();
        fadePanel.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        cauldron.SetActive(true);
        SceneManager.LoadScene("Shop"); //load the scene (after scene is loaded need to resume time)
    }

    public void Quit()
    {
        Application.Quit();
    }

    /*public void MakePotion() //called when the "make potion" button is pressed
    {
        cauldronScreen.GetComponent<CauldronScreen>().GoMiddle();
        //get the current list from the uiManager
        selectedIngredientList = gameManager.uiManager.selectedIngredientList;
        //check if the combination is a valid potion
        if (selectedIngredientList != null && selectedIngredientList.Count >= 2) //if enough ingrediant is selected
        {
            GenericInventoryProduct potionToMake = CheckRecipe(selectedIngredientList);
            if (potionToMake != null)
            {
                //valid potion and ingredients make the potion
                StartCoroutine(MakePotionCo(potionToMake));
            }
            else
            {
                Debug.Log("recipe failed");
                StartCoroutine(MakePotionCo(potionToMake));
            }


        }
        else
        {
            //no ingrediant was selected do nothing
            Debug.Log("No ingredient is selected");
        }
    }*/
}
