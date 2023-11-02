using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public charachterHealth healthBar;

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

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;                                                                                                                                                               
        healthBar.setHealth(currentHealth);

        if (currentHealth < 0) {
            Die();
        }
    }
    
     void Die() {
        //Play DIe animation
        //disable Unit
        GetComponent<Rigidbody>();
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        this.enabled = false;

    }
}
