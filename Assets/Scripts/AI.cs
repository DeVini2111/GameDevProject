using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AI : MonoBehaviour
{
    public GameObject warrior; //this means 1 variable by units... not good, TO CHANGE
    public GameObject archer;
    public GameObject wizard;
    public GameObject king;
    public Transform spawnPoint;
    public GameManager gameManager;
    public GameManager.Player Player2;
    public float cooldownTime = 2.0f; // Adjust this as needed for your cooldown time
    private float nextSpawnTime = 0.0f;
    public GameObject moneyRef_P2;   // Reference to player money
    int unitspawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnUnit();
        }
    }


    //add to redo the sale code for test purposes but bad, TO CHANGE
    public void SpawnUnit()
    {
        //unitspawn = Random.Range(1, 11); debug only
        print(unitspawn);
        if (Time.time > nextSpawnTime && unitspawn < 3 && moneyRef_P2.GetComponent<moneyCountP2>().money >= warrior.GetComponent<Warrior>().GetCost())
        {
            Instantiate(warrior, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(warrior.GetComponent<Warrior>(), Player2);
            nextSpawnTime = Time.time + cooldownTime;
        }
        else if(Time.time > nextSpawnTime && (unitspawn >= 4 && unitspawn <= 6) && moneyRef_P2.GetComponent<moneyCountP2>().money >= archer.GetComponent<Archer>().GetCost())
        {
            Instantiate(archer, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(archer.GetComponent<Archer>(), Player2);
            nextSpawnTime = Time.time + cooldownTime;
        }

        if (Time.time > nextSpawnTime && (unitspawn > 6 && unitspawn <= 8) && moneyRef_P2.GetComponent<moneyCountP2>().money >= wizard.GetComponent<Wizard>().GetCost())
        {
            Instantiate(wizard, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(wizard.GetComponent<Wizard>(), Player2);
            nextSpawnTime = Time.time + cooldownTime;
        }
        else if (Time.time > nextSpawnTime && (unitspawn >= 9 && unitspawn <= 10) && moneyRef_P2.GetComponent<moneyCountP2>().money >= king.GetComponent<King>().GetCost())
        {
            Instantiate(king, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(king.GetComponent<King>(), Player2);
            nextSpawnTime = Time.time + cooldownTime;
        }


    }
}
