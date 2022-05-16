using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Managers
    public ClockManager clockManager;
    public CostumerManager costumerManager;
    public InventoryManager inventoryManager;
    public OrderManager orderManager;
    public UiManager uiManager;
    public TimeStateManager timeStateManager;

    //Data
    public int hoursConsumed;
    public int minutesConsumed;

    //actions
    public static Action pauseTime;
    public static Action resumeTime;
    public static Action StopPlayerMovement;
    public static Action ResumePlayerMovement;
    public static Action MorningQueDone;
    public static Action NoonQueDone;
    public static Action EveningQueDone;


}
