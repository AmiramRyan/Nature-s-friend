using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Orders/OrderProduct")]
public class GenericOrderProduct : ScriptableObject
{
    #region Public vars

    public ProductType thisProductType; //what is that resource
    public Sprite productSprite;
    public string productName;

    #endregion

}
