using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTimeState
{
    public abstract void EnterState(TimeStateManager timeManager);
    public abstract void ExitState(TimeStateManager timeManager);
    public abstract void UpdateState(TimeStateManager timeManager);

}
