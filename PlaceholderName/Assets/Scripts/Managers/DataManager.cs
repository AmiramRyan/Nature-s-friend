using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<GenericOrderProduct> product;
    public static List<GenericOrderProduct> data_ProductList; //all resource in the game
    private void Awake() //set the resources list
    {
        for(int i=0; i< product.Count - 1; i++)
        {
            data_ProductList.Add(product[i]);
        }
    }
}
