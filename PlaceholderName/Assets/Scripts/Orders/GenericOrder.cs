using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Element
{
    fire,
    water,
    earth
}
[CreateAssetMenu(menuName = "Orders/PremadeOrder")]
public class GenericOrder : ScriptableObject
{
    public string title;
    public string description;
    public List<GenericRequest> OrderRequests;
    public Sprite costumerSprite;
    public Element thisOrderElement;
    public int PositiveRelationEffect;
    public int NegetiveRelationEffect;
}
