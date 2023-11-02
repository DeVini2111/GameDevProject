using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using OpenCover.Framework.Model;

public class WarriorPlayer2 : MonoBehaviour
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
    void OnTriggerEnter(Collider col) // Used collision Stay instead of Enter. Seemed to work better.
    {
        if (col.CompareTag("Units Player1"))
        {
            InvokeRepeating("Attack", 2f, 2f);
        }
        
    }

    void Attack()
    {
        //Play attack animation
        //Detect enemies in range
        //Damage enemies in range
        if (UnityEngine.Random.value < 0.2f)
        {
            animator.SetTrigger("Attack1");


        }
        else
        {
            animator.SetTrigger("Attack2");

        }

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyUnits);

        foreach (Collider enemy in hitEnemies)
        {


            if (enemy.GetComponent<WarriorPlayer1>() == true)
            {
                enemy.GetComponent<WarriorPlayer1>().TakeDamage(damageOutput);
            }

            else if (enemy.GetComponent<ArcherP1>() == true)
            {
                enemy.GetComponent<ArcherP1>().TakeDamage(damageOutput);
            }

            else if (enemy.GetComponent<BaseHealth>() == true)
            {
                enemy.GetComponent<BaseHealth>().TakeDamage(damageOutput);
            }
            //Only works for one Unit, another solution required for hitting different types of Units
            //TODO
            //Refactor to make "enemy" a universal tag by using children of classes
            //REMOVE THE TRY CATCH ATROCITY!!!
            /*try
            {
                enemy.GetComponent<WarriorPlayer1>().TakeDamage(damageOutput);
                
            }


            catch (Exception e) {
                enemy.GetComponent<BaseHealth>().TakeDamage(damageOutput);
            }
            
        }*/


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
        Destroy(this);
        GetComponent<Rigidbody>();
        GetComponent<Collider>().enabled = false;
        GetComponent<Player2Movement>().enabled = false;
        this.enabled = false;

    }
    
    void OnTriggerExit(Collider col)
    {
        Debug.Log("Out of combat");
        CancelInvoke("Attack");
        //animator.SetTrigger("Idle");
    }
}
