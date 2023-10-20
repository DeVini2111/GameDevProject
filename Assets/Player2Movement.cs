using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public float speed = 5f;

    private bool canMove;

    private Rigidbody playerRigidbody;

    public Animator animator;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        canMove = true;
        animator.SetTrigger("Moving");
        
    }

    // Update is called once per frame
    //Animation for moving and stopping have to be set
    void FixedUpdate()
    { 
        if (canMove == true) 
        {
            
            playerRigidbody.MovePosition(transform.position + Vector3.back 
            * speed * Time.deltaTime);
            
            
        }
        
    }
    //Animation for going to Idle after hitting an enemy fighter or another friendl fighter
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player 1")
        {
            canMove = false;
            animator.SetTrigger("Moving");
        }
    }
    void OnCollisionExit(Collision col)
    {
        canMove = true;
        animator.SetTrigger("Moving");
    }
}
