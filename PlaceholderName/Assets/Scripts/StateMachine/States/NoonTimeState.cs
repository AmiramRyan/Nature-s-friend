using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoonTimeState : BaseTimeState
{
    bool pause;
    bool isNoon;
    public bool timesSetForTheDayNoon;
    public List<Vector2> morningCostumersTime; //arrive time for the costumers
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Noon State");
        timesSetForTheDayNoon = timeManager.timesSetForTheDayNoon;
        isNoon = true;
        ClockManager.onTimeChange += isNoonStill;
        GameManager.pauseTime += PauseState;
        pause = false;
        timeManager.readyToSpawn = true;
        //make a costumer que
        if (!timesSetForTheDayNoon)
        {
            morningCostumersTime = timeManager.CostumerTimeToArrive(2, 14, 17);
            GameManager.NoonQueDone?.Invoke();
            timesSetForTheDayNoon = true;
        }
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        morningCostumersTime.Clear();
        timeManager.timesList.Clear();
        ClockManager.onTimeChange -= isNoonStill;
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
                    timeManager.gameManager.costumerManager.SpawnRandomCostumer();
                }
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
    public void PauseState()
    {
        pause = true;
    }
}
