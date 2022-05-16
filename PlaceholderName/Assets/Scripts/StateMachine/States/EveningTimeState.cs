using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveningTimeState : BaseTimeState
{
    bool pause;
    bool isEvning;
    public bool timesSetForTheDayEvening;
    public List<Vector2> morningCostumersTime; //arrive time for the costumers
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Evening State");
        timesSetForTheDayEvening = timeManager.timesSetForTheDayEvening;
        isEvning = true;
        ClockManager.onTimeChange += isEvningStill;
        GameManager.pauseTime += PauseState;
        pause = false;
        //Rest the forest flowers

        //chance to spawn quest costumer
        if (!timesSetForTheDayEvening)
        {
            morningCostumersTime = timeManager.CostumerTimeToArrive(1, 18, 21);
            GameManager.EveningQueDone?.Invoke();
            timesSetForTheDayEvening = true;
        }
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        morningCostumersTime.Clear();
        timeManager.timesList.Clear();
        ClockManager.onTimeChange -= isEvningStill;
        GameManager.pauseTime -= PauseState;
        timeManager.readyToSpawn = true;
    }

    public override void UpdateState(TimeStateManager timeManager)
    {
        if (pause)
        {
            timeManager.SwtichState(timeManager.pauseTimeState);
        }
        else
        {
            if (!isEvning)
            {
                ExitState(timeManager);
                timeManager.SwtichState(timeManager.daySwitchState);
            }
            else if (TimeToSpawnCostumer() && timeManager.readyToSpawn)
            {
                timeManager.readyToSpawn = false;
                timeManager.StartSpawnDelayCo();
                Debug.Log("spawn costumer");
                timeManager.gameManager.costumerManager.SpawnRandomCostumer();
            }
        }
    }

    private void isEvningStill()
    {
        if (ClockManager.hour >= 22)
        {
            isEvning = false;

        }
    }

    private bool TimeToSpawnCostumer()
    {
        for (int i = 0; i < morningCostumersTime.Count; i++)
        {
            float hour = ClockManager.hour;
            float h = morningCostumersTime[i].x;
            if (hour == h)
            {
                float min = ClockManager.minute;
                float m = morningCostumersTime[i].y;
                if (min == m)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void PauseState()
    {
        pause = true;
    }
}
