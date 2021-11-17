using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Orders/PremadeOrder")]
public class GenericOrder : ScriptableObject
{
    public string title;
    public string description;
    public bool affectRelationship;
    public List<GenericRequest> OrderRequests;
}
