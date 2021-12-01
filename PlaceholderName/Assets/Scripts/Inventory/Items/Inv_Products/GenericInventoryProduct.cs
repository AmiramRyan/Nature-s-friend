using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    potion,
    scroll,
}
[CreateAssetMenu(menuName = "Inventory/InvProduct")]
public class GenericInventoryProduct : GenericInventoryItem
{
    public ProductType thisProductType;
    public int sellValue;
    public GenericRecipe productRecipe;
}
