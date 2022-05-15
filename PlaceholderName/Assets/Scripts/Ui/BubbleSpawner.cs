using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject[] bubblesPrefab; //dataset

    //Bounding box for bubble spawn
    [SerializeField] private float maxX; //56
    //[SerializeField] private float maxY;
    [SerializeField] private float minX; //-59
    //[SerializeField] private float minY;
    [SerializeField] private float maxTimeBetweenSpawn;
    [SerializeField] private float minTimeBetweenSpawn;
    private bool spawnActive;

    private void OnEnable()
    {
        spawnActive = true;
    }

    void Update()
    {
        if (spawnActive)
        {
            //spawn
            int index = Random.Range(0, bubblesPrefab.Length);
            GameObject bubble =  Instantiate(bubblesPrefab[index]);
            bubble.transform.SetParent(this.transform,true);
            bubble.transform.position = RandomVector();
            spawnActive = false;
            StartCoroutine(SpawnTimerCo());
        }
    }

    private Vector3 RandomVector()
    {
        float x = Random.Range(minX, maxX);
        return new Vector3(x, 28, 0);
    }

    private float RandomTime()
    {
        return Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    public IEnumerator SpawnTimerCo()
    {
        yield return new WaitForSeconds(RandomTime());
        spawnActive = true;
    }
}
