using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Orders/OrderResource")]
public class GenericOrderResource : ScriptableObject
{
    public enum ResourceType
    {
        plant,
        papyrus,
        crystal
    }

    #region Private sets

    public Sprite resourceSprite; //image of the resource
    [SerializeField] private string resourceName; //name of the resource

    #endregion

    #region Public vars

    public ResourceType thisResourceType; //what is that resource
    public int rawValue; //how much dose it sells for

    #endregion

}
