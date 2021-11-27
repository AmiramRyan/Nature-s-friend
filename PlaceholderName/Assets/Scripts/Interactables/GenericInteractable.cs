using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInteractable : MonoBehaviour
{
    private bool inRangeOfPlayer; //updated every tick
    public int minutesConsumed = 0; //time to consume in minutes
    public int hoursConsumed = 0; //time to cunsome in hours

    #region General Actions
    private void OnEnable() //event subscribe
    {
        PlayerInteractions.interactedAction += PlayerInteracted;
    }

    private void OnDisable() //events unsubscribe
    {
        PlayerInteractions.interactedAction -= PlayerInteracted;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfPlayer = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfPlayer = false;

        }
    }

    private void PlayerInteracted() //general check for all interactable activated when the interact action is send
    {
        if (inRangeOfPlayer)
        {
            InteractAction();
        }
    }

    #endregion


    public virtual void InteractAction() //to be changed for each object
    {
        Debug.Log("Interacted with" + transform.name);
    }



}
