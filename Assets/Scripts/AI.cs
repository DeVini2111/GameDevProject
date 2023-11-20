using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AI : MonoBehaviour
{
    public GameObject warrior; //this means 1 variable by units... not good, TO CHANGE
    public GameObject archer;
    public Transform spawnPoint;
    public GameManager gameManager;
    public GameManager.Player Player2;
    public float cooldownTime = 2.0f; // Adjust this as needed for your cooldown time
    private float nextSpawnTime = 0.0f;
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
        unitspawn = Random.Range(1, 3);
        if (Time.time > nextSpawnTime && unitspawn == 1)
        {
            Instantiate(warrior, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(warrior.GetComponent<Warrior>(), Player2);
            nextSpawnTime = Time.time + cooldownTime;
        }
        else if(Time.time > nextSpawnTime && unitspawn == 2)
        {
            Instantiate(archer, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(archer.GetComponent<Archer>(), Player2);
            nextSpawnTime = Time.time + cooldownTime;
        }
    }
}
