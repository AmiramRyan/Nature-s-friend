using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D myRb;

    [Header("Effects")]
    public GameObject particleTrail;
    public GameObject particleIdle;

    private Vector3 change; //from input 
    private Vector3 motionVector; //after calculations
    [SerializeField]private float floatStrength = 0.5f;
    private float lastYpos;
    public bool stopMoving;

    private void OnEnable()
    {
        stopMoving = false;
        GameManager.StopPlayerMovement += StopPlayerMovement;
        GameManager.ResumePlayerMovement += ResumePlayerMovement;
    }

    private void OnDisable()
    {
        GameManager.StopPlayerMovement -= StopPlayerMovement;
        GameManager.ResumePlayerMovement -= ResumePlayerMovement;
    }
    void MoveCharacter()
    {
        myRb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (!stopMoving)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if (change != Vector3.zero)
            {
                if (change.x > 0)
                {
                    transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
                    particleTrail.transform.localScale = new Vector3(-1f, -1f, particleTrail.transform.localScale.z);

                }
                else
                {
                    transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
                    particleTrail.transform.localScale = new Vector3(1f, 1f, particleTrail.transform.localScale.z);
                }
                lastYpos = transform.position.y;
                particleIdle.SetActive(false);
                particleTrail.SetActive(true);
                MoveCharacter();
            }
            else
            {
                particleTrail.SetActive(false);
                particleIdle.SetActive(true);
                /*float floatValue = (float)Mathf.Sin(Time.time) * floatStrength;
                if (transform.position.y >= transform.position.y + 5f) //go down
                {
                    transform.position = new Vector3(transform.position.x, lastYpos - floatValue, transform.position.z);
                }
                else //go up
                {
                    transform.position = new Vector3(transform.position.x, lastYpos + floatValue, transform.position.z);
                }*/
            }
        }

        }
    public void StopPlayerMovement()
    {
        stopMoving = true;
        Debug.Log("StopPlayer");
    }

    public void ResumePlayerMovement()
    {
        stopMoving = false;
        Debug.Log("ResumePlayer");
    }
}
