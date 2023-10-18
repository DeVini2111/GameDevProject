using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterTest : MonoBehaviour
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.setHealth(currentHealth);
    }
}
