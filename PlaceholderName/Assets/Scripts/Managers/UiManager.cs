using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("UiPanels")] 
    public GameObject cauldronPanel;

    public void ActivateUiPanel(string uiPanelToActivate) //activates the slected panel while closing others using the panels name string
    {
        switch (uiPanelToActivate)
        {
            case "cauldron":
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
        cauldronPanel.SetActive(false);
    }
}
