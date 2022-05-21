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
    public SpawnTime spawnTime;
    public SpawnType growsOn; // tree / ground
    private GameManager gameManager;

    //Bar Reff
    public float clickEffectivnes; //how effective is the players click 0.2 is realy hard so thats the min
    public GameObject proggresBar;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        proggresBar.transform.localScale = new Vector3(0f, proggresBar.transform.localScale.y, proggresBar.transform.localScale.z); //will start at y = 0 and grow
    }

    private void Update()
    {
        //Always try to get back to 0 on the x scale
        if (proggresBar.transform.localScale.x > 0f)
        {
            proggresBar.transform.localScale = new Vector3(proggresBar.transform.localScale.x - 0.1f * Time.deltaTime, proggresBar.transform.localScale.y, proggresBar.transform.localScale.z);
        }
        else
        {
            proggresBar.transform.localScale = new Vector3(0, proggresBar.transform.localScale.y, proggresBar.transform.localScale.z);
        }

    }
    public void ClickOnPlant()
    {
        proggresBar.transform.localScale = new Vector3(proggresBar.transform.localScale.x + clickEffectivnes, proggresBar.transform.localScale.y, proggresBar.transform.localScale.z);
        if (proggresBar.transform.localScale.x > 1f) //overClick
        {
            gameManager.taskManager.plantsGatherd++;
            GameManager.taskRelated?.Invoke();
            //add to inventory 
            gameManager.inventoryManager.UpdateResourceCount(1, thisPlant, true);
            //destroy this gameobject
            Destroy(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        ClickOnPlant();
    }
}
