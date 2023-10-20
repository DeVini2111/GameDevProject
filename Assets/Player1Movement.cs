using UnityEngine;

public class Player1Movement : MonoBehaviour
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
            
            playerRigidbody.MovePosition(transform.position + Vector3.forward 
            * speed * Time.deltaTime);
            
            
        }
        
    }
    //Animation for going to Idle after hitting an enemy fighter or another friendl fighter
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player 2")
        {
            canMove = false;
            animator.ResetTrigger("Moving");
        }
    }
    void OnCollisionExit(Collision col)
    {
        canMove = true;
        animator.SetTrigger("Moving");
    }
    
}
