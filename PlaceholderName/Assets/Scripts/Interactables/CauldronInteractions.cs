using System.Collections.Generic;
using UnityEngine;

public class CauldronInteractions : GenericInteractable
{
    //Specific script that dictate the action taken once the player interacted with the cauldron

    public static bool readyForInteraction { get; set; }

    [Header("Managers refrances")] //TODO: connect the game manager
    public ClockManager clockManager;
    public UiManager uiManager;
    public InventoryObj myInventory;
    public List<GenericInventoryResource> selectedIngredientList = new List<GenericInventoryResource>();

    private void Start()
    {
        readyForInteraction = true;
        UpdatePredictedResult();
    }
    public override void OnEnable()
    {
        base.OnEnable();
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
            uiManager.ActivateUiPanel("cauldron"); //opens the Ui potion making window
            readyForInteraction = false;
        }
        else
        {
            //the player has inputed the interaction button -> close the window
            uiManager.DisablePanels();
        }
    }

    public void MakePotion() //called when the "make potion" button is pressed
    {
        //get the current list from the uiManager
        selectedIngredientList = uiManager.selectedIngredientList;
        //check if the combination is a valid potion
        if (selectedIngredientList != null && selectedIngredientList.Count >= 2) //if enough ingrediant is selected
        {
            GenericInventoryProduct potionToMake = CheckRecipe(selectedIngredientList);
            if (potionToMake != null)
            {
                //make the potion 
                AddPotionToInv(potionToMake);
                //consume the ingredients
                for (int i = 0; i < selectedIngredientList.Count; i++)
                {
                    selectedIngredientList[i].DecreaseAmount(1);
                }
            }
            else
            {
                Debug.Log("recipe failed");
            }

            //clear the list
            selectedIngredientList.Clear();

            //advance time
            clockManager.TimePass(hoursConsumed, minutesConsumed); //move the clock forward by the time it takes to make a potion

            //disable panels
            uiManager.DisablePanels();
        }
        else
        {
            //no ingrediant was selected do nothing
            Debug.Log("No ingredient is selected");
        }
    }

    public GenericInventoryProduct CheckRecipe(List<GenericInventoryResource> selectedIng)
    {
        GenericInventoryProduct checkingProductValidity;
        GenericRecipe thisProductRecipe;
        bool isCorrect = false;
        for (int i = 0; i < myInventory.playerProducts.Count; i++) //for each product
        {
            checkingProductValidity = myInventory.playerProducts[i];
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
                    GenericInventoryProduct recipeResult = myInventory.playerProducts.Find(product => product == checkingProductValidity);
                    //Debug.Log(recipeResult.itemName + " product has been made!");
                    //recipeResult.IncreaseAmount(1);
                    return recipeResult;
                }
            }
        }
        return null;
    }

    public void AddPotionToInv(GenericInventoryProduct makeThisProduct)
    {
        makeThisProduct.IncreaseAmount(1);
    }

    private void UpdatePredictedResult()
    {
        //get the current list from the uiManager
        selectedIngredientList = uiManager.selectedIngredientList;
        //calculate the predicted result if the uesr were to press "make potion"
        GenericInventoryProduct tempPredictedProduct = CheckRecipe(selectedIngredientList);
        //updtae predicted result sprite
        uiManager.ChangePredictedSprite(tempPredictedProduct); //can be null if no recipe matches
    }

}
