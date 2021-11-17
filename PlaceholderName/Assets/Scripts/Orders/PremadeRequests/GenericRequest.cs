using UnityEngine;

[CreateAssetMenu(menuName = "Orders/PremadeRequest")]
public class GenericRequest : ScriptableObject
{
    public GenericOrderResource theResource;
    public int amount;
}
