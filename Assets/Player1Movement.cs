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
        if (canMove) 
        {
            playerRigidbody.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
        }
        
    }
    //Animation for going to Idle after hitting an enemy fighter or another friendl fighter
    
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Units Player2")){ //col.CompareTag("Units Player1") || 
            canMove = false;
            animator.SetTrigger("Idle");
        }
    }
    public void StartMoving() {   
        canMove = true;
        animator.SetTrigger("Moving");
    }
}
