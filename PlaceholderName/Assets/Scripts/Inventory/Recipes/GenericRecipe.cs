using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecipeType
{
    plantBase,
    crystalBase
}
[CreateAssetMenu(menuName = "Inventory/Recipe/ProductRecipe")]
public class GenericRecipe : ScriptableObject
{
    public GenericRecipeIngredient[] ingredientsRequierment; //array of the requierd ingredients and amounts neede to make the product
}
