using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : GenericSingletonClass_Task<MonoBehaviour>
{
    public GameManager gameManager;
    public Sprite taksComplete;

    [Header("Tasks Reff")]
    public Image task1Sp;
    public Image task2Sp;
    public Image task3Sp;


    public int ordersComplete;
    public int plantsGatherd;
    public int coinsEarned;
    public bool[] TasksCompleteArr = new bool[3];


    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        GameManager.taskRelated += CheckTasks;
        ordersComplete = 0;
        plantsGatherd = 0;
        coinsEarned = 0;
        for (int i = 0; i < TasksCompleteArr.Length; i++)
        {
            TasksCompleteArr[i] = false;
        }
    }

    private void OnDisable()
    {
        GameManager.taskRelated -= CheckTasks;
    }

    public void CheckTasks() //CHECKS TASKS
    {
        if (ordersComplete >= 3)
        {
            task3Sp.sprite = taksComplete;
            TasksCompleteArr[2] = true;
            TriggerGameEnd(CheckIfAllComplete());
        }

        if(plantsGatherd >= 20)
        {
            task1Sp.sprite = taksComplete;
            TasksCompleteArr[0] = true;
            TriggerGameEnd(CheckIfAllComplete());
        }

        if(coinsEarned >= 5000)
        {
            task2Sp.sprite = taksComplete;
            TasksCompleteArr[1] = true;
            TriggerGameEnd(CheckIfAllComplete());
        }
    }

    public bool CheckIfAllComplete()
    {
        for (int i = 0; i < TasksCompleteArr.Length; i++)
        {
            if (!TasksCompleteArr[i])
            {
                return false;
            }
        }
        return true;
    }

    public void TriggerGameEnd(bool isDone)
    {
        if (isDone)
        {
            gameManager.uiManager.endPanel.SetActive(true);
        }
    }
}
