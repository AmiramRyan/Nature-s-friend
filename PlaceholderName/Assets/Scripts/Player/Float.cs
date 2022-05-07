using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{ 
    public static bool idle;
    public static float originalY;

    public float floatStrength = 0.5f;

    void FixedUpdate()
    {
        if (idle)
        { 
            transform.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.deltaTime) * floatStrength), transform.position.z);
        }
    }
}
