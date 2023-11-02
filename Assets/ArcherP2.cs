using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ArcherP2 : MonoBehaviour
{

    public int maxHealth = 75;
    public int currentHealth;

    public int damageOutput = 25;
    public charachterHealth healthBar;

    //public Animator animator;

    public Transform attackPoint;
    public Collider range; //this is the "range" of the archer, it is another collider that triggers the attack from a distance
                           //this is to be combined with the attackPoint, which is not ideal at the moment
                           //TODO
                           //Implement a proper solution with the collider and not the attack point

    public float attackRange = 50f;

    public LayerMask enemyUnits;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider col) // Used collision Stay instead of Enter. Seemed to work better.
    {
        if (col.CompareTag("Units Player1"))
        {
            print("proute");
            InvokeRepeating("Attack", 2f, 2f);
        }

    }

    void Attack()
    {
        //Play attack animation
        //Detect enemies in range
        //Damage enemies in range


        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyUnits);

        Collider ennemy = hitEnemies[0];

        if (ennemy)
        {
            if (ennemy.GetComponent<WarriorPlayer1>() == true)
            {
                ennemy.GetComponent<WarriorPlayer1>().TakeDamage(damageOutput);
            }

            else if (ennemy.GetComponent<BaseHealth>() == true)
            {
                ennemy.GetComponent<BaseHealth>().TakeDamage(damageOutput);
            }
        }
        /*{
            try
            {
                ennemy.GetComponent<WarriorPlayer2>().TakeDamage(damageOutput);
                if (ennemy.GetComponent<WarriorPlayer2>().currentHealth < 0)
                {
                    CancelInvoke("Attack");
                    GetComponent<Player1Movement>().StartMoving();
                }
            }

            catch (Exception e)
            {
                ennemy.GetComponent<BaseHealth>().TakeDamage(damageOutput);
            }
        }*/

        /*foreach (Collider enemy in hitEnemies)
        {
            //Only works for one Unit, another solution required for hitting different types of Units
            //Coroutine needed for substracting the health a bit later
            //Only works for one Unit, another solution required for hitting different types of Units
            //TODO
            //Refactor to make "enemy" a universal tag by using children of classes
            //REMOVE THE TRY CATCH ATROCITY!!!
            try
            {
                enemy.GetComponent<WarriorPlayer2>().TakeDamage(damageOutput);
                if (enemy.GetComponent<WarriorPlayer2>().currentHealth < 0)
                {
                    CancelInvoke("Attack");
                    GetComponent<Player1Movement>().StartMoving();
                }
            }

            catch (Exception e)
            {
                enemy.GetComponent<BaseHealth>().TakeDamage(damageOutput);
            }
        


        }
        */

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

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Play DIe animation
        //disable Unit
        GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Player1Movement>().enabled = false;
        this.enabled = false;

    }
}
