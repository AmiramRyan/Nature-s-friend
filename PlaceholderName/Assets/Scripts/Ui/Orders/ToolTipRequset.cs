using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipRequset : Request
{
    public GameObject ToolTipTextContainer;
    public TextMeshProUGUI ToolTipTextContainertext;

    private void OnMouseOver()
    {
        ToolTipTextContainer.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        ToolTipTextContainer.gameObject.SetActive(false);
    }
}
