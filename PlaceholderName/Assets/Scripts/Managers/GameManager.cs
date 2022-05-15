using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GamseState
{
    intro,
    firstWeek,
    secoundWeek,
    finalWeek
}

public class GameManager : MonoBehaviour
{
    public ClockManager clockManager;
    public CostumerManager costumerManager;
    public InventoryManager inventoryManager;
    public OrderManager orderManager;
    public UiManager uiManager;

    [SerializeField]private GamseState currentGameState;
    public static Action gameStateChanged;
    public GamseState newGameState;
    public int hoursConsumed;
    public int minutesConsumed;
    public static Action pauseTime;
    public static Action resumeTime;

    private void OnEnable()
    {
        gameStateChanged += OnGameStageChange;
    }

    private void OnDisable()
    {
        gameStateChanged -= OnGameStageChange;
    }
    public GamseState GetCurrentGameState()
    {
        return currentGameState;
    }

    private void SetGameState(GamseState newGameState) //only for debugging force without checks
    {
        DeinitializeGameState(currentGameState);
        InitializeGameState(newGameState);
        currentGameState = newGameState;
    }

    private void InitializeGameState(GamseState gameState) //all the actions a game state need to do before he start
    {
        switch (gameState)
        {
            case GamseState.intro:

                break;
            case GamseState.firstWeek:

                break;
            case GamseState.finalWeek:

                break;
            default:
                break;
        }
    }
    private void DeinitializeGameState(GamseState gameState) // all the acitons a game statea need to do before he die
    {
            switch (gameState)
            {
                case GamseState.intro:

                    break;
                case GamseState.firstWeek:

                    break;
                case GamseState.finalWeek:

                    break;
                default:
                    break;
            }
        }

    private void OnGameStageChange()
    {
        DeinitializeGameState(currentGameState);
        InitializeGameState(newGameState);
        currentGameState = newGameState;
    }

}
