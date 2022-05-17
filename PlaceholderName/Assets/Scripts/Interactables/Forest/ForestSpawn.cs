using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    tree,
    ground
}
[System.Serializable]
[CreateAssetMenu(fileName = "New forest Spawn")]
public class ForestSpawn : ScriptableObject
{
    //Stores data about Spawn point for a plant on the forest
    public SpawnType spawnType; //ground or tree

    //The bounding box
    public Vector3[] possibleSpawns;
    [SerializeField]private int maxSpotsToSpawn;
    public int availableSpotsToSpawn;

    public void ResetSpots()
    {
        availableSpotsToSpawn = maxSpotsToSpawn;
    }

    public bool isFull()
    {
        if(availableSpotsToSpawn == 0) { return true; }
        return false;
    }

    public Vector3 GetRandomV3()
    {
        int i = Random.Range(0, possibleSpawns.Length);
        return possibleSpawns[i];
    }
}
