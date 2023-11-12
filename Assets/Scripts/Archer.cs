using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : Unit
{

    [SerializeField] private AudioSource attackSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    //Raycast variable for the sight
    public float sight;
    //Raycast variable for the range
    public float range;
    //Reference to the enemy unit in range
    private Unit enemyInRange;
    private bool inMelee;
    protected override void Start()
    {
        maxHealth = 70;
        speed = 6f;
        canAttack = true;
        attackSpeed = 2f;
        damageOutput = 10;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        StartMoving();

        unitRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (canMove) {
            //Debugging here
            if (player == GameManager.Player.Player1) {
                unitRigidbody.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
            } else {
                unitRigidbody.MovePosition(transform.position + Vector3.back * speed * Time.deltaTime);
            }
        }


        //Raycast to get the Unit in front of this one
        //Since it's a ranged Unit another Raycast is needed to detect enemies in range
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
        if (Physics.Raycast(sightRay, out RaycastHit hitData, sight))
        {
            //Debugging
            Debug.Log(hitData.collider.gameObject.name);

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
            }
            //Attacks if the respective Unit is an enemy
            if (player == GameManager.Player.Player1) {
                if (unitInFront.CompareTag("Units Player2")) {
                    inMelee = true;
                    if (canAttack) {
                        Attack();
                    }
                }
            } else {
                if (unitInFront.CompareTag("Units Player1"))
                {
                    inMelee = true;
                    if (canAttack)
                    {
                        Attack();
                    }
                }
            }
        } else if (!canMove && !inMelee) {
            canMove = true;
        }
        //Detect if enemy is in range (Ray collided with an enemy)
        if (!inMelee) {
            if (player == GameManager.Player.Player1) {
                if (Physics.Raycast(shootRay, out RaycastHit hit, range, 1<<7)) {
                    enemyInRange = hit.transform.gameObject.GetComponent<Unit>();
                    if (canAttack) {
                        Attack();
                    }
                }

            } else {
                if (Physics.Raycast(shootRay, out RaycastHit hit, range, 1<<6)) {
                    enemyInRange = hit.transform.gameObject.GetComponent<Unit>();
                    if (canAttack) {
                        Attack();
                    }
                }
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
        if (inMelee) {
            damage = damageOutput / 2;
            toAttack = unitInFront;
        }

        
        StartCoroutine(AnimationAttackCoroutine(toAttack, damage));
        attackSoundEffect.Play();
    }

    IEnumerator AnimationAttackCoroutine(Unit toAttack, int damage) {
        canAttack = false;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.15f);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            yield return null;
        }

        toAttack.TakeDamage(damage);

        if (!toAttack.animator.IsUnityNull()) {
                while (toAttack.animator.GetCurrentAnimatorStateInfo(0).IsName("GettingHit")) {
                    yield return null;
                }
        }
        /**if (toAttack.isDead) {
            if(!isDead) {
                StartMoving();
            }
        }
        **/
        yield return new WaitForSeconds(attackSpeed / 1.5f);
        inMelee = false;
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
        canMove = false;
        isDead = true;

        this.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(this.unitRigidbody);
        this.enabled = false;

    }

    protected override void StartMoving() {
        canMove = true;
        animator.SetTrigger("Moving");
    }
    
    void OnCollisionEnter(Collision other)
    {
        canMove = false;
    }
}
