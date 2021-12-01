using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Recipe/resipeIngredientReq")]
public class GenericRecipeIngredient : ScriptableObject
{
    public GenericInventoryResource reqResource;
    public int amount;
}
