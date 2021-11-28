using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory/player Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<GenericInventoryResource> playerResources;

    public void UesItem(GenericInventoryResource thisResource)
    {
        thisResource.DecreaseAmount();
    }
}
