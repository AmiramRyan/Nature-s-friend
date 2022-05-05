using UnityEngine;

[CreateAssetMenu(menuName = "Orders/PremadeRequest")]
public class GenericRequest : ScriptableObject
{
    public GenericOrderProduct theOrderProduct;
    public GenericInventoryProduct theInvProduct; 
    public int amount;
}
