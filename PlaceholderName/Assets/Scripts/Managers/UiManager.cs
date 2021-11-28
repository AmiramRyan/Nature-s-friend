using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("UiPanels")] 
    public GameObject cauldronPanel;
    public GameObject ShelfStorage;

    private int shelfeStorageSpace = 22; //maximum amount on 1 shelf
    [SerializeField] private GameObject cauldronShelfImgPrefab; //the image prefab for a shelf cauldron ingredient
    [SerializeField] private InventoryObj playerInventory; //the player scriptable obj inventory

    public List<GenericInventoryResource> inventoryIngredientsList = new List<GenericInventoryResource>(); //spread out the ingrideint from the inventory into a list
    public List<GameObject> tempIngredientGameobjectsList = new List<GameObject>(); // a refarance to the actual gameobjects renderd on screen

    #region PanelsControls
    public void ActivateUiPanel(string uiPanelToActivate) //activates the slected panel while closing others using the panels name string
    {
        switch (uiPanelToActivate)
        {
            case "cauldron":
                //fill up the panel shelfes
                FillPotionIngredient();
                SetPanels(true);
                break;
            default:
                Debug.LogError("Ui manager could not find a panel with this name: " + uiPanelToActivate);
                break;
        }
    }

    private void SetPanels(bool isCauldronActive) //[bool1 * bool2 * bool3 *.... bool n] -> set panels 1 - n active acording to the bools (n = num of panels in game) 
    {
        cauldronPanel.SetActive(isCauldronActive);
        //panelN.SetActive(boolN);
    }

    public void DisablePanels() //disable all game mini panels 
    {
        //disable all panels
        cauldronPanel.SetActive(false);
        //clear ui elements
        ClearUiIngridientList();
        //ready all interactables
        CauldronInteractions.readyForInteraction = true;
    }

    #endregion

    #region PanelsFunctionality
    private void FillPotionIngredient()
    {
        
        for (int i = 0; i < playerInventory.playerResources.Count; i++) //for each item
        {
            if(playerInventory.playerResources[i].numInInv != 0) //there IS such resource
            {
                for(int j = 0; j < playerInventory.playerResources[i].numInInv; j++) //for each of the spesific item
                {
                    inventoryIngredientsList.Add(playerInventory.playerResources[i]); //add it to the temp list
                }
            }
        }

        //set up the shelf with the temp list
       
        for(int i = 0; i < inventoryIngredientsList.Count; i++)
        {
            GameObject tempIngredientRef = Instantiate(cauldronShelfImgPrefab, transform.position, Quaternion.identity) as GameObject;
            tempIngredientRef.GetComponent<ShelfItem>().InitiateItem(inventoryIngredientsList[i]);
            tempIngredientGameobjectsList.Add(tempIngredientRef);
            if (i < shelfeStorageSpace)
            {
                tempIngredientRef.transform.SetParent(ShelfStorage.transform, false);
            }
            else
            {
                Debug.Log("No More Room For Me :(");
            }
        }
    }

    public void ClearUiIngridientList()
    {
        for(int i = 0; i < tempIngredientGameobjectsList.Count; i++)
        {
            tempIngredientGameobjectsList[i].GetComponent<ShelfItem>().DestroyItem(); //destroy the gameobject 
        }
        tempIngredientGameobjectsList.Clear();
        inventoryIngredientsList.Clear();
    }

    #endregion

}
