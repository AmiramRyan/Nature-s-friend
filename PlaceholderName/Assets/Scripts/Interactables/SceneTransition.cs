using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : GenericInteractable
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SpriteRenderer highlightSprite;

    public override void OnEnable()
    {
        base.OnEnable();
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (inRangeOfPlayer)
        {
            highlightSprite.enabled = true;
        }
        else
        {
            highlightSprite.enabled = false;
        }
    }

    public override void InteractAction()
    {
        //pop up the confirm panel 
        gameManager.uiManager.OpenTransitionPanel();
    }

    public void GoToForest()
    {
        gameManager.clockManager.TimePass(hoursConsumed, minutesConsumed); //forward time
        gameManager.timeStateManager.SwtichState(gameManager.timeStateManager.pauseTimeState); // switch to pauseTimeState
        SceneManager.LoadScene("ForestScene"); //load the scene (after scene is loaded need to resume time)
    }
}
