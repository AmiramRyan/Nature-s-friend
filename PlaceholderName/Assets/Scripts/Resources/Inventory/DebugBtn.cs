using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBtn : MonoBehaviour
{
    public InventoryManager inv;
    public GenericInventoryResource theInvRes;
    public bool increase;

    public void Ues()
    {
        inv.UpdateResourceCount(2, theInvRes, increase);
    }
}
