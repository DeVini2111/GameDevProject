using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


public class WarriorPlayer2 : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public charachterHealth healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()    // For debug
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (currentHealth <= 0)
        {
            //Animation for Death
            //animator.SetBool("IsDead", true);
            Destroy(GetComponent<Rigidbody>());
            Destroy(gameObject);
        }
    }

    //Animations for Hit, Attack1 or Attack2have to be set
    void OnCollisionStay(Collision col) // Used collision Stay instead of Enter. Seemed to work better.
    {
        if (col.gameObject.name == "Player 1")
        {
            TakeDamage(2);
            //animator.SetBool("IsGettingHit", false);
        }
        
    }

    void TakeDamage(int damage)
    {
        //animator.SetBool("IsGettingHit", true);
        currentHealth -= damage;
                                                                                                                                                                          
        healthBar.setHealth(currentHealth);
    }
    
}
