using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cauldronState
{
    mid,
    left,
    right
}
public class CauldronScreen : MonoBehaviour
{
    //Statics for positioning the cauldronPanel
    private float RightPos = -280f;
    private float leftPos = 280f;
    private float middleBothPos = 0f;
    private cauldronState state;
    private bool moving = false;
    private Vector2 targetPos;
    [SerializeField] private RectTransform cauldronPanel;

    private void Start()
    {
        state = cauldronState.mid;
    }

    private void Update()
    {
        if (moving)
        {
            cauldronPanel.anchoredPosition = Vector2.Lerp(cauldronPanel.anchoredPosition, targetPos, 0.001f);
            if (cauldronPanel.anchoredPosition.x == targetPos.x){
                moving = false;
            }
        }
    }

    public void GoMiddle()
    {
        targetPos = new Vector2(middleBothPos, cauldronPanel.anchoredPosition.y);
        state = cauldronState.mid;
        moving = true;
    }

    public void GoLeft()
    {
        if (state == cauldronState.right)
        {
            GoMiddle();
            return;
        }
        else {
            targetPos = new Vector2(leftPos, cauldronPanel.anchoredPosition.y);
            state = cauldronState.left;
            moving = true;
        }
    }

    public void GoRight()
    {
        if (state == cauldronState.left)
        {
            GoMiddle();
            return;
        }
        else {
            targetPos = new Vector2(RightPos, cauldronPanel.anchoredPosition.y);
            state = cauldronState.right;
            moving = true;
        }
    }

}
