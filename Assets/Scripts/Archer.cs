using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Archer : Unit
{

    [SerializeField] private AudioSource attackSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource runSoundEffect;

    //Raycast variable for the sight
    public float sight;
    //Raycast variable for the range
    public float range;
    //Reference to the enemy unit in range
    private Unit enemyInRange;

    //Checks if Unit can fire
    private bool inRange;

    //Unit cost
    private int cost = 10;

    //public bool isDead;
    protected override void Start()
    {
        maxHealth = 70;
        speed = 5f;
        canAttack = true;
        attackSpeed = 2f;
        damageOutput = 10;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        StartMoving();
        runSoundEffect.Play();
        unitRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //Moving the Unit
    protected override void FixedUpdate()
    {
        if (canMove) {
            if (player == GameManager.Player.Player1) {
                unitRigidbody.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
            } else {
                unitRigidbody.MovePosition(transform.position + Vector3.back * speed * Time.deltaTime);
            }
        }

        //Since it's a ranged Unit Raycast is needed to detect enemies in range
        //Stops when enemie is in range or any unit is in sight
        Ray sightRay;
        Ray shootRay;
        if (player == GameManager.Player.Player1) {
                sightRay = new Ray (transform.position, Vector3.forward * sight);
                
                //Draws the Ray on the scene view when in Play Mode
                Debug.DrawRay(transform.position, Vector3.forward * sight, Color.green);

                shootRay = new Ray (transform.position, Vector3.forward * range);

                Debug.DrawRay(transform.position, Vector3.forward * range, Color.red);
            } else {
                sightRay = new Ray (transform.position, Vector3.back * sight);

                //Draws the Ray on the scene view when in Play Mode
                Debug.DrawRay(transform.position, Vector3.back * sight, Color.green);

                shootRay = new Ray (transform.position, Vector3.back * range);

                Debug.DrawRay(transform.position, Vector3.back * range, Color.red);
            }

        //True, if the Raycast hits another collider
        //controls the movement if a another Unit is in Front
        if (Physics.Raycast(sightRay, out RaycastHit hitData, sight))
        {
            //Return if the collider is not a Unit (should not happen)
            if (!(hitData.transform.gameObject.CompareTag("Units Player1") 
            || hitData.transform.gameObject.CompareTag("Units Player2"))) {
                return;
            }

            //Stops the Unit if it moves
            canMove = false;
            animator.SetTrigger("Idle");
            runSoundEffect.Stop();
        } else if (!inRange){
            StartMoving();
        }
        //Detect if enemy is in range (Ray collided with an enemy)
        if (player == GameManager.Player.Player1) {
            if (Physics.Raycast(shootRay, out RaycastHit hit, range, 1<<7)) {
                inRange = true;
                enemyInRange = hit.transform.gameObject.GetComponent<Unit>();
                canMove = false;
                animator.SetTrigger("Idle");
                if (canAttack) {
                    Attack();
                }
            } else {
                inRange = false;
            }

        } else {
            if (Physics.Raycast(shootRay, out RaycastHit hit, range, 1<<6)) {
                inRange = true;
                enemyInRange = hit.transform.gameObject.GetComponent<Unit>();
                canMove = false;
                animator.SetTrigger("Idle");
                if (canAttack) {
                    Attack();
                }
            } else {
                inRange = false;
            }
        }

    }

    protected override void Attack()
    {
        //Play attack animation
        //Detect enemies in range
        //Damage enemies in range
        if (!canAttack) {
            return;
        }
        int damage = damageOutput;
        Unit toAttack = enemyInRange;

        
        StartCoroutine(AnimationAttackCoroutine(damage));
        attackSoundEffect.Play();
        runSoundEffect.Stop();
    }

    IEnumerator AnimationAttackCoroutine(int damage) {
        canAttack = false;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.15f);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            yield return null;
        }

        enemyInRange.TakeDamage(damage);

        if (!enemyInRange.animator.IsUnityNull()) {
                while (enemyInRange.animator.GetCurrentAnimatorStateInfo(0).IsName("GettingHit")) {
                    yield return null;
                }
        }
        yield return new WaitForSeconds(attackSpeed / 1.5f);
        canAttack = true;

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        //Play die animation
        animator.SetBool("isDead", true);
        deathSoundEffect.Play();
        runSoundEffect.Stop();
        canMove = false;
        //Disable or Destroy the Unit
        this.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(this.unitRigidbody);
        //Trigger OnUnitDeath Event
        base.Die();
        this.enabled = false;

    }

    protected override void StartMoving() {
        canMove = true;
        animator.SetTrigger("Moving");
    }

    //Set the new cost variable
    public override int GetCost()
    {
        return cost;
    }
}
