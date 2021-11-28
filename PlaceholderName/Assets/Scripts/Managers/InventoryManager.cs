using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObj playerInventory;
    public void UpdateResourceCount(int amount, GenericInventoryResource resourceToAdd, bool increase) //amount=> how much the value is changed, resource scriptableoobj, to increase or decrease
    {
        int I = playerInventory.playerResources.IndexOf(resourceToAdd); //get the index of this item 
        if (increase) 
        {
           playerInventory.playerResources[I].IncreaseAmount(amount); //increase by the amount
        }
        else
        {
            playerInventory.playerResources[I].DecreaseAmount(amount);
        }
    }
}
