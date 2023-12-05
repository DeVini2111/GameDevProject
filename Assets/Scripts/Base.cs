
using UnityEngine;

public class Base : Unit
{

    // Start is called before the first frame update
    protected override void Start()
    {
        maxHealth = 500;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        //Gets the Rigidbody Component of the current Unit
        unitRigidbody = GetComponent<Rigidbody>();
        animator = null;

    }
    
     protected override void Die() {
        //disable Unit or destroy Unit
        this.GetComponent<BoxCollider>().enabled = false;
        base.Die();
        this.enabled = false;
    }
}
