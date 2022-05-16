using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimeState : BaseTimeState
{
    public bool pause;
    public override void EnterState(TimeStateManager timeManager)
    {
        Debug.Log("Pause State");
        //subscribe to the clock event
        timeManager.readyToSpawn = false;
        GameManager.resumeTime += ResumeState;
        pause = true;
        //Stop the clock
        timeManager.gameManager.clockManager.StopTheClock();
        //StopInteractions
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        //Let the clock Continue
        timeManager.gameManager.clockManager.ResumeTheClock();
        GameManager.resumeTime -= ResumeState;

        //Return to 1 of the 3 base states
        float hour = ClockManager.hour;
        if(hour < 14)
        {
            timeManager.SwtichState(timeManager.morningTimeState);
        }
        else if(hour>=14 && hour < 18)
        {
            timeManager.SwtichState(timeManager.noonTimeState);
        }
        else
        {
            timeManager.SwtichState(timeManager.evneningTimeState);
        }
    }

    public override void UpdateState(TimeStateManager timeManager)
    {
        if (!pause)
        {
            ExitState(timeManager);
        }
    }

    public void ResumeState()
    {
        pause = false;
    }


}
