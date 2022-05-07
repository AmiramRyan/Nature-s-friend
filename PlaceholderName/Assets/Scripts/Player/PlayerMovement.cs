using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D myRb;
    private Vector3 change; //from input 
    private Vector3 motionVector; //after calculations
 
    void MoveCharacter()
    {
        myRb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change != Vector3.zero)
        {
            MoveCharacter();
            Float.idle = false;
        }
        else
        {
            Float.originalY = this.transform.position.y;
            Float.idle = true;
        }
    }
        /*private Vector2 PosToAsim(Vector2 position) //convert point to asimetric point
    {
        return new Vector2(position.x - position.y, (position.x - position.y) / 2);
    }*/


}
