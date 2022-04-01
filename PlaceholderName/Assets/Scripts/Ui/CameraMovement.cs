using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Vector2 centerOfCam;
    private float upperEdge, lowerEdge, rightEdge, leftEdge;
    private float distanceFromTarget;

    private void Start()
    {
        centerOfCam = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector2.Distance(target.position, transform.position);
        if (distanceFromTarget >= 4f)
        {
            centerOfCam = new Vector2(target.position.x, target.position.y);
            SmoothMoveTo();
        }
        Debug.Log(distanceFromTarget);
    }

    public void SetEdges(float leftX, float downY, float rightX, float upY) //camera bounds 
    {
        upperEdge = upY;
        lowerEdge = downY;
        rightEdge = rightX;
        leftEdge = leftX;
    }

    public void SmoothMoveTo()
    {
        while(distanceFromTarget >= 4f)
        {
            transform.position = Vector2.MoveTowards(centerOfCam, new Vector2(target.position.x, target.position.y), 0.0001f);
        }
    }
}
