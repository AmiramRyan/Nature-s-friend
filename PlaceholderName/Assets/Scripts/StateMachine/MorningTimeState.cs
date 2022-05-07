using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningTimeState : BaseTimeState
{
    bool isMorning;
    int costumerIndex = 0;
    public List<Vector2> morningCostumersTime; //arrive time for the costumers
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Morning State");
        //subscribe to the clock event
        isMorning = true;
        timeManager.readyToSpawn = true;
        ClockManager.onTimeChange += isMorningStill;
        //Restart the forest flowers
        //make a costumer que
        morningCostumersTime = timeManager.CostumerTimeToArrive(1, 10, 13);
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        morningCostumersTime.Clear();
        timeManager.timesList.Clear();
        ClockManager.onTimeChange -= isMorningStill;
    }

    public override void UpdateState(TimeStateManager timeManager)
    {
        if (!isMorning)
        {
            ExitState(timeManager);
            timeManager.SwtichState(timeManager.noonTimeState);
        }
        else if (TimeToSpawnCostumer() && timeManager.readyToSpawn)
        {
            timeManager.readyToSpawn = false;
            timeManager.StartSpawnDelayCo();
            Debug.Log("spawn costumer");
            timeManager.costumerManager.SpawnRandomCostumer();
        }
    }

    private void isMorningStill()
    {
        if (ClockManager.hour >= 14)
        {
            isMorning = false;
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
