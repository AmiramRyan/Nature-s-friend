using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnTime
{
    morning,
    noon,
    evning
}

public class Plant : MonoBehaviour
{
    //data
    public int spawnChance; //in %
    public GenericInventoryResource thisPlant;
    public float pickResistance; //how hard is it to get the plant
    public SpawnTime spawnTime;
    public string growsOn; // tree / ground

    //Bar Reff

}
