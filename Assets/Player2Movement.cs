using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public float speed = 5f;

    private bool canMove;

    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (canMove == true) 
        {
            playerRigidbody.MovePosition(transform.position + Vector3.back 
            * speed * Time.deltaTime);
        }
        
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player 1")
        {
            canMove = false;
        }
    }
    void OnCollisionExit(Collision col)
    {
        canMove = true;
    }
}

