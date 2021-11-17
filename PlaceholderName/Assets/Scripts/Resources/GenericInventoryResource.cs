using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/InvResource")]
public class GenericInventoryResource : GenericOrderResource
{
    #region Private sets

    [SerializeField] private string toolTipText; //tooltip
    private static int maxNumInInv = 999; //maximum amount of the item inventory can hold

    #endregion

    #region Public vars

    public int numInInv; //how many are in the inventory

    #endregion

    public void DecreaseAmount(int amount)
    {
        numInInv -= amount;
        if (numInInv < 0)
        {
            numInInv = 0;
            Debug.LogError("Item " + thisResourceType + "is lower then zero");
        }
    }

    public void IncreaseAmount(int amount)
    {
        numInInv += amount;
        if (numInInv > maxNumInInv)
        {
            numInInv = maxNumInInv;
            Debug.LogError("Item " + thisResourceType + "is over the maximum value");
        }
    }
}
