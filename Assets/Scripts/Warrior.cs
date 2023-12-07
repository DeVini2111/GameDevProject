using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;

public class Warrior : Unit
{
    [SerializeField] private AudioSource attackSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource runSoundEffect;

    //Sight Range for Raycast
    public float sight;

    //Unit Cost
    private int cost = 20;

    //Set all variables that needs to be used
    protected override void Start()
    {
        maxHealth = 100;
        speed = 5f;
        canAttack = true;
        attackSpeed = 2f;
        damageOutput = 15;
        canMove = true;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        animator.SetTrigger("Moving");
        runSoundEffect.Play();

        //Gets the Rigidbody Component of the current Unit
        unitRigidbody = GetComponent<Rigidbody>();
  
    }


    //Moves the Unit according to the Player, if the Unit can move
    //Does a Raycast in order to simulate the current sight of the Unit
    //Stops if a friendly Unit is in sight and attacks enemy units
    protected override void FixedUpdate()
    {
        if (canMove) {
            if (player == GameManager.Player.Player1) {
                unitRigidbody.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
            } else {
                unitRigidbody.MovePosition(transform.position + Vector3.back * speed * Time.deltaTime);
            }
        }

        //Raycast to get the Unit in front of this one
        //For Range Units it needs another raycast to hit enemies
        Ray ray;
        if (player == GameManager.Player.Player1) {
                ray = new Ray (transform.position, Vector3.forward * sight);

                //Draws the Ray on the scene view when in Play Mode
                Debug.DrawRay(transform.position, Vector3.forward * sight, Color.green);
            } else {
                ray = new Ray (transform.position, Vector3.back * sight);
                Debug.DrawRay(transform.position, Vector3.back * sight, Color.green);
            }
        //True, if the Raycast hits another collider
        if (Physics.Raycast(ray, out RaycastHit hitData, sight))
        {

            //Return if the collider is not a Unit (should not happen)
            if (!(hitData.transform.gameObject.CompareTag("Units Player1") 
            || hitData.transform.gameObject.CompareTag("Units Player2"))) {
                return;
            }

            //Sets the Unit in Front to the object 
            unitInFront = hitData.transform.gameObject.GetComponent<Unit>();

            //Stops the Unit if it moves
            if (canMove) {
                canMove = false;
                animator.SetTrigger("Idle");
                runSoundEffect.Stop();
            }
            //Attacks if the respective Unit is an enemy
            if (player == GameManager.Player.Player1) {
                if (unitInFront.CompareTag("Units Player2")) {
                    if (canAttack) {
                        Attack();
                    }
                }
            } else {
                if (unitInFront.CompareTag("Units Player1"))
                {
                    if (canAttack)
                    {
                        Attack();
                    }
                }
            }
        } else if (!canMove){
            StartMoving();
        }
    }

    //Starts moving the Unit again
    protected override void StartMoving() {
        canMove = true;
        animator.SetTrigger("Moving");
        runSoundEffect.Play();
    }

    //Called when enemy Unit is in sight of current Unit
    //Calls an Coroutine to perform an Attack
    protected override void Attack()
    {
        //Double checks if current Unit is allowed to Attack again
        if (!canAttack) return;
        //Starts the Coroutine, which waits for the animation to play before attacking again
        StartCoroutine(AnimationAttackCoroutine());

    }
    //Coroutine with waiting for animation
    IEnumerator AnimationAttackCoroutine() {
        //Disable further attacks from this Unit
        canAttack = false;
        runSoundEffect.Stop();
        animator.SetBool("isAttacking", true);

        int damage = damageOutput;

        //Randomly chosses a heavy attack (other animation and double damage) or normal attack
        if (UnityEngine.Random.value < 0.2f) {
            animator.SetTrigger("Attack1");
            damage = damage *2;
        } else {
            animator.SetTrigger("Attack2");
        }
        attackSoundEffect.Play();

        //Wait for Transition to Attack Animation
        yield return new WaitForSeconds(0.15f);
        //Wait till Attack Animation is over
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            yield return null;
        }
        //Deal damage to Unit in front (enemy Unit in sight)
        unitInFront.TakeDamage(damage);

        //Wait till the Hit Animation of the unit in front played
        //If Unit has an animator
        if (!unitInFront.animator.IsUnityNull()) {
            while (unitInFront.animator.GetCurrentAnimatorStateInfo(0).IsName("GettingHit")) {
                yield return null;
            }
        }
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackSpeed);
        //Unlock canAttack
        canAttack = true;
    }

    //Sets the correct animation triggers, then calls parent function 
    public override void TakeDamage(int damage)
    {
        animator.ResetTrigger("Idle");
        animator.SetTrigger("GettingHit");
        base.TakeDamage(damage);
    }
    
    //Diables the collider, rigidbody and the script itself
     protected override void Die() {
        //Play Die animation
        animator.SetBool("isDead", true);
        deathSoundEffect.Play();
        runSoundEffect.Stop();
        canMove = false;

        //disable Unit or destroy Unit
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponentInChildren<Canvas>().enabled = false;
        Destroy(this.unitRigidbody);
        //Trigger OnUnitDeath Event
        base.Die();
        this.enabled = false;
    }

    //Set Unit cost to the new variable
    public override int GetCost()
    {
        return cost;
    }
}
