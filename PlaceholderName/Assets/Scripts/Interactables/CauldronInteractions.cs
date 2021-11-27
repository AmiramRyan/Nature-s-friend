using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CauldronInteractions : GenericInteractable
{
    //Specific script that dictate the action taken once the player interacted with the cauldron

    public bool readyForInteraction = true; //true for debugging

    [Header("Managers refrances")] //TODO: connect the game manager
    public ClockManager clockManager;
    public UiManager uiManager;
    public override void InteractAction()
    {
        if (readyForInteraction)
        {
            uiManager.ActivateUiPanel("cauldron"); //opens ui window to mix stuff up

        }
        else
        {
            //TODO: some ui message telling the player the cauldron is busy/not available/no resources etc..
            Debug.Log("cauldron cannot be interacted at the moment");
        }
    }

    public void SpendTime() //called when the action to make a potion is selected
    {
        clockManager.TimePass(hoursConsumed, minutesConsumed); //move the clock forward by the time it takes to make a potion
        uiManager.DisablePanels();
    }
}
