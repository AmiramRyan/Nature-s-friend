using UnityEngine;

[CreateAssetMenu(menuName = "Orders/PremadeRequest")]
public class GenericRequest : ScriptableObject
{
    public GenericOrderProduct theProduct;
    public int amount;
}
