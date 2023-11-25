using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Unity;
using static GameManager;
public class GameManager : MonoBehaviour
{
    public DefeatScreen DefeatScreen;
    public VictoryScreen VictoryScreen;
    private bool soundplayed = false;

    [SerializeField] private AudioSource victoryMelody;
    [SerializeField] private AudioSource defeatMelody;

    public GameObject moneyRef_P1;   // Reference to player money
    public GameObject moneyRef_P2;   // Reference to player money

    private bool gameOver;
    //Manage the Player association for a unit

    public enum Player {
        Player1,
        Player2
    }

    //Reference to the Base of each Player
    //Gets set at the OnEnable method
    private Unit basePlayer1;

    private Unit basePlayer2;

    //Manage connection to SPawnButton
    //Manage Menus and Victory
    //Dictionary to keep track of the spawned Units
    //Queue of Units because only the first one gets deleted -> stand in a Queue
    private Dictionary<Player, Queue<Unit>> unitsByPlayer = new Dictionary<Player, Queue<Unit>>();

    // OnEnable is called when the script gets enabled
    //If Player1 spawns a Unit it gets added to the unitsByPlayer List
    //Same for Player2
    //Assign the Bases for the different Players, to check if they won
    //Add the two methos baseDestroyed and DeleteUnit to the Event when a Unit dies
    private void OnEnable()
    {
        Unit.OnUnitDeath += BaseDestroyed;
        Unit.OnUnitDeath += DeleteUnit;

        basePlayer1 = GameObject.Find("BasePlayer1").GetComponent<Base>();
        basePlayer2 = GameObject.Find("BasePlayer2").GetComponent<Base>();

        unitsByPlayer.Add(Player.Player1, new Queue<Unit>());
        unitsByPlayer.Add(Player.Player2, new Queue<Unit>());
    }

    // AddUnit method is used to add a specific Unit to the respective Player
    // Checks wether the Player has enough money to actually buy the Unit
    public void AddUnit (Unit unit, Player player) {
        //Reads the individual Unit Cost which can vary between different types of Units
        int cost = unit.GetCost();

        if (player == Player.Player1)
        {   
            // Resource management:
            //Check if Player1 has enough money to buy the Unit
            //Double Check
            if(moneyRef_P1.GetComponent<moneyCountP1>().money >= cost)
            {
                moneyRef_P1.GetComponent<moneyCountP1>().substractMoney(cost);
                //Enque the Unit in the Queue for the respective Player
                unitsByPlayer[Player.Player1].Enqueue(unit);
            }
            else
            {
                //TO-DO: Pop-Up Message?, UI feedback in some way
                Debug.Log("Not enough money!");
            }

        }
        else
        {
            // Resource management:
            //Check if Player2 has enough money to buy the Unit
            //Double Check
            if(moneyRef_P2.GetComponent<moneyCountP2>().money >= cost)
            {
                moneyRef_P2.GetComponent<moneyCountP2>().substractMoney(cost);
                //Enque the Unit in the Queue for the respective Player
                unitsByPlayer[Player.Player2].Enqueue(unit);
            }
            else
            {
                //TO-DO: Pop-Up Message?, UI feedback in some way
                Debug.Log("Not enough money!");
            }
        }
        
    }

    //Deletes the first Unit which is in the Queue of the respective Player
    //Doesn't have to check what Unit it is since, the Units stand in a Queue
    //TO-DO: Reward System has to be tested and tweaked accordingly
    public void DeleteUnit(Player player) {
        //Method will not be called if one of the Bases is destroyed
        if (!gameOver) {
            if (player == Player.Player1) {
                //Double check that the reference is not null
                if (moneyRef_P2 == null) {
                    return;
                }
                //Get the unit cost + 20 as a reward
                int returnMoney = unitsByPlayer[Player.Player1].Peek().GetCost() + 20;

                //Access money reference of oppisite Player
                moneyRef_P2.GetComponent<moneyCountP2>().addMoney(returnMoney);

                //Delete first Unit of Queue
                unitsByPlayer[Player.Player1].Dequeue();

            } else {
                //Double check that the reference is not null
                if (moneyRef_P1 == null) {
                    return;
                }
                //Get the unit cost + 20 as a reward
                int returnMoney = unitsByPlayer[Player.Player2].Peek().GetCost() + 20;

                //Access money reference of oppisite Player
                moneyRef_P1.GetComponent<moneyCountP1>().addMoney(returnMoney);

                //Delete first Unit of Queue
                unitsByPlayer[Player.Player2].Dequeue();
            }
        }
       
    }

    //Method checks wether the Base of any Player is destroyed
    private void BaseDestroyed(Player player) {
        if (player == Player.Player1) {
            //Checks if Reference is null (for security reasons)
            if (basePlayer1 == null)
            {
                return;
            }
            //When the Box Collider is disabled the Base got destroyed
            //Initiate Defeat Sequence
            if (!basePlayer1.GetComponent<BoxCollider>().enabled) {
                DefeatScreen.Setup();
                gameOver = true;
                if (!soundplayed) {
                    defeatMelody.Play();
                    soundplayed = true;
                }
            }
        } else {
            //Checks if Reference is null (for security reasons)
            if (basePlayer2 == null) {
                return;
            }
            //When the Box Collider is disabled the Base got destroyed
            //Initiate Victory Sequence
            if (!basePlayer2.GetComponent<BoxCollider>().enabled) {  
                VictoryScreen.Setup();
                gameOver = true;
                if (!soundplayed) {
                    victoryMelody.Play();
                    soundplayed = true;
                }
            }
        }
    }

    //For security reasons:
    //Delete the Methods from Event when script is disabled
    private void OnDisable()
    {
        Unit.OnUnitDeath -= BaseDestroyed;
        Unit.OnUnitDeath -= DeleteUnit;
    }
}
