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
    public bool[] spotFilled;
    [SerializeField]private int maxSpotsToSpawn;
    public int availableSpotsToSpawn;

    private void OnEnable()
    {
        ResetSpots();
    }

    public void ResetSpots()
    {
        availableSpotsToSpawn = maxSpotsToSpawn;
        spotFilled = new bool[possibleSpawns.Length];
        for (int i = 0; i < possibleSpawns.Length; i++)
        {
            spotFilled[i] = false;
        }
    }

    public bool isFull()
    {
        if(availableSpotsToSpawn == 0) { return true; }
        return false;
    }

    public Vector3 GetRandomV3()
    {
        int spotIndex = Random.Range(0, spotFilled.Length - 1);
        if (spotFilled[spotIndex]) //spot is full on that parent
        {
            if (spotIndex != 0) //start from 0
            {
                spotIndex = 0;
            }
            while (spotFilled[spotIndex]) //iterate the arr to find an empty spot to spawn
            {
                spotIndex++;
                if (spotIndex == spotFilled.Length) //no more room on this parent
                {
                    Debug.Log("No more room on this Parent");
                    return Vector3.zero;
                }
            }
        }
        availableSpotsToSpawn--;
        spotFilled[spotIndex] = true;
        return possibleSpawns[spotIndex];
    }
}
