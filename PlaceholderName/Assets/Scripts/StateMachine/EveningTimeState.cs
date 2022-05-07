using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveningTimeState : BaseTimeState
{
    bool isEvning;
    public List<Vector2> morningCostumersTime; //arrive time for the costumers
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Evening State");
        isEvning = true;
        ClockManager.onTimeChange += isEvningStill;
        //Rest the forest flowers

        //chance to spawn quest costumer
        morningCostumersTime = timeManager.CostumerTimeToArrive(1, 18, 21);
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        morningCostumersTime.Clear();
        timeManager.timesList.Clear();
        ClockManager.onTimeChange -= isEvningStill;
        timeManager.readyToSpawn = true;
    }

    public override void UpdateState(TimeStateManager timeManager)
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
            timeManager.costumerManager.SpawnRandomCostumer();
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
}
