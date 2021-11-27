using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<GenericOrderResource> resources;
    public static List<GenericOrderResource> data_resourceList; //all resource in the game
    private void Awake() //set the resources list
    {
        for(int i=0; i< resources.Count - 1; i++)
        {
            data_resourceList.Add(resources[i]);
        }
    }
}
