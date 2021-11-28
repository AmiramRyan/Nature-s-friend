
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBtns : MonoBehaviour
{
    [Header("refrance to Managers")]
    public ClockManager clockManager;
    public UiManager uiManager;


    #region Buttons actions

    //Debugging time buttons
    public void IncreaseTime1Hour30Min()
    {
        clockManager.TimePass(1,30);
    }

    public void IncreaseTime2Hour()
    {
        clockManager.TimePass(2, 0);
    }

    public void IncreaseTime30Min()
    {
        clockManager.TimePass(0, 30);
    }

    public void IncreaseTime1Hour45Min()
    {
        clockManager.TimePass(1, 45);
    }

    //Functional buttons

    public void CloseAllGamePanels()
    {
        uiManager.DisablePanels();
    }

    #endregion
}
