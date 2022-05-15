using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CauldronInteractions : GenericInteractable
{
    //Specific script that dictate the action taken once the player interacted with the cauldron

    public static bool readyForInteraction { get; set; }

    [Header("Managers refrances")]
    public GameManager gameManager;
    public List<GenericInventoryResource> selectedIngredientList = new List<GenericInventoryResource>();
    public GameObject MakePotionParticles;
    public GameObject resultObj;
    public GameObject cauldronScreen;

    private void Start()
    {
        readyForInteraction = true;
        UpdatePredictedResult();
        resultObj.SetActive(false);
        MakePotionParticles.SetActive(false);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        ShelfItem.highlightStatusChanged += UpdatePredictedResult;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        ShelfItem.highlightStatusChanged -= UpdatePredictedResult;
    }

    public override void InteractAction()
    {
        if (readyForInteraction)
        {
            gameManager.uiManager.ActivateUiPanel("cauldron"); //opens the Ui potion making window
            readyForInteraction = false;
        }
        else
        {
            //the player has inputed the interaction button -> close the window
            //uiManager.DisablePanels();
            gameManager.uiManager.CloseCauldronPanel();
        }
    }



    #region Potions
    public void MakePotion() //called when the "make potion" button is pressed
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
    }
    public void AddPotionToInv(GenericInventoryProduct makeThisProduct)
    {
        makeThisProduct.IncreaseAmount(1);
    }
    private void UpdatePredictedResult()
    {
        //get the current list from the uiManager
        selectedIngredientList = gameManager.uiManager.selectedIngredientList;
        //calculate the predicted result if the uesr were to press "make potion"
        GenericInventoryProduct tempPredictedProduct = CheckRecipe(selectedIngredientList);
        //updtae predicted result sprite
        gameManager.uiManager.ChangePredictedSprite(tempPredictedProduct); //can be null if no recipe matches
    }
    public IEnumerator MakePotionCo(GenericInventoryProduct potionToMake)
    {
        Cursor.visible = false;
        gameManager.uiManager.CloseConfirmPanel();
        if (potionToMake != null)
        {
            resultObj.GetComponent<Image>().sprite = potionToMake.itemSprite;
            //Add to the inventory 
            AddPotionToInv(potionToMake);
            MakePotionParticles.SetActive(true);
            MakePotionParticles.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(1.2f);
            resultObj.SetActive(true);
        }
        else
        {
            //play visual representation
            MakePotionParticles.SetActive(true);
            MakePotionParticles.GetComponent<ParticleSystem>().Play();
            //uiManager.MakeIngredientsFall();
        }
        //consume the ingredients
        for (int i = 0; i < selectedIngredientList.Count; i++)
        {
            selectedIngredientList[i].DecreaseAmount(1);
        }

        yield return new WaitForSeconds(3f);
        resultObj.SetActive(false);
        //Clean up
        //clear the list
        selectedIngredientList.Clear();

        //advance time
        gameManager.clockManager.TimePass(gameManager.hoursConsumed, 0); //move the clock forward by the time it takes to make a potion

        //disable panels
        gameManager.uiManager.CloseCauldronPanel();
        Cursor.visible = true;
        yield return null;
    }
    public GenericInventoryProduct CheckRecipe(List<GenericInventoryResource> selectedIng)
    {
        GenericInventoryProduct checkingProductValidity;
        GenericRecipe thisProductRecipe;
        bool isCorrect = false;
        for (int i = 0; i < gameManager.inventoryManager.playerInventory.playerProducts.Count; i++) //for each product
        {
            checkingProductValidity = gameManager.inventoryManager.playerInventory.playerProducts[i];
            if (checkingProductValidity.thisProductType == ProductType.potion) //skip non potion recipes
            {
                thisProductRecipe = checkingProductValidity.productRecipe;
                for (int j = 0; j < thisProductRecipe.ingredientsRequierment.Length; j++) //for each ingredient req
                {
                    isCorrect = true; //the recpie is correct by default, if we fail one of the tests to validate it then the recipe failed
                                      //compare the amount selected to the amount req
                    List<GenericInventoryResource> sameIngredientList = selectedIng.FindAll(ingredient => ingredient == thisProductRecipe.ingredientsRequierment[j].reqResource); //list of the same ingredient
                    int ingredientAmount = sameIngredientList.Count;
                    if (ingredientAmount != thisProductRecipe.ingredientsRequierment[j].amount) //no way to make that specific product
                    {
                        isCorrect = false;
                        break;
                    } //shelf item resource data is a generic inventory resource
                    sameIngredientList.Clear();
                }
                if (isCorrect) //passed all the tests
                {
                    //make an item ref
                    GenericInventoryProduct recipeResult = gameManager.inventoryManager.playerInventory.playerProducts.Find(product => product == checkingProductValidity);
                    //Debug.Log(recipeResult.itemName + " product has been made!");
                    //recipeResult.IncreaseAmount(1);
                    return recipeResult;
                }
            }
        }
        return null;
    }

    #endregion




}
