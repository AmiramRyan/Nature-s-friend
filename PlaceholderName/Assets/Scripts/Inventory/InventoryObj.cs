using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory/player Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<GenericInventoryResource> playerResources;
    public List<GenericInventoryProduct> playerProducts;

    public void UesItem(GenericInventoryItem thisItem)
    {
        thisItem.DecreaseAmount();
    }
}
