using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    resource, //uesd to make products
    product //uesd to complete orders
}
public class GenericInventoryItem : ScriptableObject
{
    #region Private sets

    public Sprite itemSprite; //image of the resource
    public string itemName; //name of the resource
    [SerializeField] private string toolTipText; //tooltip
    private static int maxNumInInv = 999; //maximum amount of the item inventory can hold

    #endregion

    #region Public vars

    public int numInInv; //how many are in the inventory
    public ItemType itempType;

    #endregion

    #region Change Inventory Amount
    public void DecreaseAmount(int amount)
    {
        numInInv -= amount;
        if (numInInv < 0)
        {
            numInInv = 0;
            Debug.LogError("Item " + itemName + "is lower then zero");
        }
    }

    public void DecreaseAmount()
    {
        numInInv--;
        if (numInInv < 0)
        {
            numInInv = 0;
            Debug.LogError("Item " + itemName + "is lower then zero");
        }
    }

    public void IncreaseAmount(int amount)
    {
        numInInv += amount;
        if (numInInv > maxNumInInv)
        {
            numInInv = maxNumInInv;
            Debug.LogError("Item " + itemName + "is over the maximum value");
        }
    }

    #endregion
}
