using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLike : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D myRb;

    private Vector3 change; //from input 
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
        }
    }

}
