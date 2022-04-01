using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    plant,
    crystal,
    papyrus
}

[CreateAssetMenu(menuName = "Inventory/InvResource")]

public class GenericInventoryResource : GenericInventoryItem
{
    public ResourceType thisResourceType;
    public GameObject splashEffect;
    //stuff
}
