using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.Animations;


public class WarriorPlayer1 : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int damageOutput = 20;
    public charachterHealth healthBar;

    public Animator animator;

    public Transform attackPoint;

    public float attackRange = 0.5f;

    public LayerMask enemyUnits;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()    // For debug
    {
        
    }

    
    //Animations for Hit, Attack1 or Attack2have to be set
    void OnTriggerEnter(Collider col) // Used collision Stay instead of Enter. Seemed to work better.
    {
        if (col.CompareTag("Units Player2"))
        {
            InvokeRepeating("Attack", 2f, 2f);
        }
        
    }

    void Attack()
    {
        //Play attack animation
        //Detect enemies in range
        //Damage enemies in range
        if (UnityEngine.Random.value < 0.2f) {
            animator.SetTrigger("Attack1");
            
            
        } else {
            animator.SetTrigger("Attack2");
            
        }

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyUnits);

        foreach (Collider enemy in hitEnemies)
        {
            //Only works for one Unit, another solution required for hitting different types of Units
            //Coroutine needed for substracting the health a bit later
            enemy.GetComponent<WarriorPlayer2>().TakeDamage(damageOutput);
            if (enemy.GetComponent<WarriorPlayer2>().currentHealth < 0)
            {
                CancelInvoke("Attack");
                GetComponent<Player1Movement>().StartMoving();
            }

        }


    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) 
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;                                                                                                                                                               
        healthBar.setHealth(currentHealth);
        animator.ResetTrigger("Idle");
        animator.SetTrigger("GettingHit");

        if (currentHealth < 0) {
            Die();
        }
    }
    
    void Die() {
        //Play DIe animation
        animator.SetBool("isDead", true);
        //disable Unit
        GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Player1Movement>().enabled = false;
        this.enabled = false;

    }
    
}
