using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class CombinePlayer1 : MonoBehaviour
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
            Destroy(GetComponent<Rigidbody>());
            Destroy(gameObject);
        }
    }

    void OnCollisionStay(Collision col) // Used collision Stay instead of Enter. Seemed to work better.
    {
        if (col.gameObject.name == "Player 2")
        {
            TakeDamage(2);

        }
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
                                                                                                                                                                                          
        healthBar.setHealth(currentHealth);
    }
}
