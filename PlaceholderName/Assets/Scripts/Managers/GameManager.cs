using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class GameManager : GenericSingletonClass_GameManager<MonoBehaviour>
{
    //Managers
    public ClockManager clockManager;
    public CostumerManager costumerManager;
    public InventoryManager inventoryManager;
    public OrderManager orderManager;
    public UiManager uiManager;
    public TimeStateManager timeStateManager;
    public ForestManager forestManager;
    public RelationsManager relationsManager;
    public GameObject player;
    public TaskManager taskManager;

    //Data
    public int hoursConsumed;
    public int minutesConsumed;
    public PolygonCollider2D forestCollider;
    public PolygonCollider2D shopCollider;

    //actions
    public static Action pauseTime;
    public static Action resumeTime;
    public static Action StopPlayerMovement;
    public static Action ResumePlayerMovement;
    public static Action MorningQueDone;
    public static Action NoonQueDone;
    public static Action EveningQueDone;
    public static Action taskRelated;

    private void OnEnable()
    {
        InitiateGame();
    }

    private void InitiateGame()
    {
        timeStateManager.currentTimeState = timeStateManager.morningTimeState;
        relationsManager.GenerateRelationForToday();
    }
}
