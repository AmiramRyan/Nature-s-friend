using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoonTimeState : BaseTimeState
{
    bool isNoon;
    public List<Vector2> morningCostumersTime; //arrive time for the costumers
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Noon State");
        isNoon = true;
        ClockManager.onTimeChange += isNoonStill;
        timeManager.readyToSpawn = true;
        //make a costumer que
        morningCostumersTime = timeManager.CostumerTimeToArrive(2, 14, 17);
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        morningCostumersTime.Clear();
        timeManager.timesList.Clear();
        ClockManager.onTimeChange -= isNoonStill;
    }

    public override void UpdateState(TimeStateManager timeManager)
    {
        if (!isNoon)
        {
            ExitState(timeManager);
            timeManager.SwtichState(timeManager.evneningTimeState);
        }
        else if (TimeToSpawnCostumer())
        {
            if (timeManager.readyToSpawn)
            {
                timeManager.readyToSpawn = false;
                timeManager.StartSpawnDelayCo();
                timeManager.costumerManager.SpawnRandomCostumer();
            }
        }
    }

    private void isNoonStill()
    {
        if (ClockManager.hour >= 18)
        {
            isNoon = false;
        }
    }

    private bool TimeToSpawnCostumer()
    {
        for (int i = 0; i < morningCostumersTime.Count; i++)
        {
            float h = morningCostumersTime[i].x;
            float hour = ClockManager.hour;
            if (hour == h)
            {
                float m = morningCostumersTime[i].y;
                float min = ClockManager.minute;
                if (min == m)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
