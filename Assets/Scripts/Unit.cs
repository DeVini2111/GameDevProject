using System;
using UnityEngine;
using static GameManager;

//General parent class of any Units in the Game, has basic functions and variables
public class Unit : MonoBehaviour
{
    //Refernces and variables that need to be set in the Start method of child classes
    public charachterHealth healthBar;
    public Animator animator;
    public LayerMask enemyUnits;
    public Player player;
    //Action (special event) with the Player as a parameter gets triggered when a Unit dies
    public static event Action<Player> OnUnitDeath;
    protected Rigidbody unitRigidbody;
    protected float speed;
    protected bool canMove;
    protected int maxHealth;
    protected int currentHealth;
    protected int damageOutput;
    protected bool canAttack;
    protected float attackSpeed;
    //Save the unitin front of the current Unit, only one for melee combatants
    protected Unit unitInFront;

    // Start is called before the first frame update
    //Set Up all other variables like health and player in the cild classes
    protected virtual void Start()
    {
    }

    //Unit movement, move back or forward depending Player 1 or Player 2
    //Manage Unit Sight and Attack as well
    protected virtual void FixedUpdate()
    {
        
    }

    //Attack function when an enemy is in sight, sight = range for melee combatants
    protected virtual void Attack(){
    }


    //Deals damage to the current Unit and plays the hit animation
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        //Checks if Unit died
        if (currentHealth <= 0) {
            Die();
        }
    }

    //Identifies that the Unit can move again
    protected virtual void StartMoving(){
        
    }
    
    //Disables the Unit and it's collider upon call
    //Triggers the OnUnitDeath Event if the Event is not null
    protected virtual void Die(){
        OnUnitDeath?.Invoke(this.player);
    }

    //Get Method for the cost of a unit
    public virtual int GetCost()
    {
        return 0;
    }
}
