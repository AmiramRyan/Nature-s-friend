using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Script : GenericSingletonClass_Canvas<MonoBehaviour>
{
    public GameObject panelCanvas;
    //For Singelton Implamentation
    private void Start()
    {
        panelCanvas.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
