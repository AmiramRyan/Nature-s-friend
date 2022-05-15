using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningTimeState : BaseTimeState
{
    bool isMorning;
    bool pause;
    int costumerIndex = 0;
    public List<Vector2> morningCostumersTime; //arrive time for the costumers
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Morning State");
        //subscribe to the clock event
        isMorning = true;
        timeManager.readyToSpawn = true;
        ClockManager.onTimeChange += isMorningStill;
        GameManager.pauseTime += PauseState;
        pause = false;
        //Restart the forest flowers
        //make a costumer que
        morningCostumersTime = timeManager.CostumerTimeToArrive(1, 10, 13);
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        morningCostumersTime.Clear();
        timeManager.timesList.Clear();
        ClockManager.onTimeChange -= isMorningStill;
        GameManager.pauseTime -= PauseState;
    }

    public override void UpdateState(TimeStateManager timeManager)
    {
        if (pause)
        {
            timeManager.SwtichState(timeManager.pauseTimeState);
        }
        else
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
                timeManager.gameManager.costumerManager.SpawnRandomCostumer();
            }
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

    public void PauseState()
    {
        pause = true;
    }

}
