using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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

    
}
