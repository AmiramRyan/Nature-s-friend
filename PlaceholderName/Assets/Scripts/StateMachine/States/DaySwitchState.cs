using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySwitchState : BaseTimeState
{
    public override void EnterState(TimeStateManager timeManager)
    {
        //reset everything day related
        //resetSpawnQue
        timeManager.ResetSpawnQue();
        //black screen to signfy day
        //forward the clock
        timeManager.gameManager.clockManager.TimePass(12,0);
        timeManager.readyToSpawn = false;
        ExitState(timeManager);
    }

    public override void ExitState(TimeStateManager timeManager)
    {
        timeManager.SwtichState(timeManager.morningTimeState);
    }

    public override void UpdateState(TimeStateManager timeManager)
    {
        //no need to stay here :)
    }

}
