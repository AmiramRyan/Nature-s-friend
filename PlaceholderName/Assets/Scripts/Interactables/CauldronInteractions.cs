using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CauldronInteractions : GenericInteractable
{
    //Specific script that dictate the action taken once the player interacted with the cauldron

    public static bool readyForInteraction { get; set; }

    [Header("Managers refrances")] //TODO: connect the game manager
    public ClockManager clockManager;
    public UiManager uiManager;
    public InventoryObj myInventory;

    private void Start()
    {
        readyForInteraction = true;
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
        //make a new list for the selected ingredient and add all the highlighted ones
        List<GameObject> selectedIngredientList = new List<GameObject>();
        for (int i = 0; i < uiManager.tempIngredientGameobjectsList.Count; i++)
        {
            if (uiManager.tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().highlighted) //if the item is selected
            {
                selectedIngredientList.Add(uiManager.tempIngredientGameobjectsList[i]); //add it to the list
            }
        }
        if (selectedIngredientList != null && selectedIngredientList.Count >= 2) //if enough ingrediant is selected
        {
            //if the potion is valid add it to the player inventory
            List<GenericInventoryResource> tempList = new List<GenericInventoryResource>();
            for(int i =0; i< selectedIngredientList.Count; i++)
            {
                tempList.Add(selectedIngredientList[i].GetComponent<ShelfItem>().resourceData);
            }
            CheckRecipe(tempList);
            tempList.Clear();

            //consume ingredient
            for (int i = 0; i < selectedIngredientList.Count; i++)
            {
                selectedIngredientList[i].GetComponent<ShelfItem>().UesItem();
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

    private void CheckRecipe(List<GenericInventoryResource> selectedIng)
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
                        //Debug.Log("recipe failed");
                        isCorrect = false;
                        break;
                    } //shelf item resource data is a generic inventory resource
                    sameIngredientList.Clear();
                }
                if (isCorrect) //passed all the tests
                {
                    //make the item
                    GenericInventoryProduct recipeResult = myInventory.playerProducts.Find(product => product == checkingProductValidity);
                    Debug.Log(recipeResult.itemName + " product has been made!");
                    recipeResult.IncreaseAmount(1);
                    return;
                }
            }
        }
    }
}
