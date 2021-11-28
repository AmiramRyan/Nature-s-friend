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
        for (int i = 0; i< uiManager.tempIngredientGameobjectsList.Count; i++)
        {
            if (uiManager.tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().highlighted) //if the item is selected
            {
                selectedIngredientList.Add(uiManager.tempIngredientGameobjectsList[i]); //add it to the list
            }
        }
        if (selectedIngredientList != null && selectedIngredientList.Count >=2) //if enough ingrediant is selected
        {
            //TODO check if the potion is valid
            //TODO add the potion to the inventory
            //consume ingredient
            for (int i = 0; i < selectedIngredientList.Count; i++)
            {
                selectedIngredientList[i].GetComponent<ShelfItem>().UesItem();
            }
            //clear the list
            selectedIngredientList.Clear();
            clockManager.TimePass(hoursConsumed, minutesConsumed); //move the clock forward by the time it takes to make a potion
            uiManager.DisablePanels();
        }
        else
        {
            //no ingrediant was selected do nothing
            Debug.Log("No ingreiant is selected");
        }
    }
}
