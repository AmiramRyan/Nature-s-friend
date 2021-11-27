using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractions : MonoBehaviour
{
    //TurnPlayerIntup into a signal to invoke the interact action for objects in range
    public static Action interactedAction;
    private void Update()
    {
        if (Input.GetButtonDown("interact")) // set to ' K '
        {
            interactedAction?.Invoke();
        }
    }
}