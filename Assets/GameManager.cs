using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    //Manage the Player association for a unit
    public enum Player {
        Player1,
        Player2
    }

    private Unit basePlayer1;

    private Unit basePlayer2;

    //Manage connection to SPawnButton

    //Manage Ressources

    //Manage Menus and Victory
    //Dictionary to keep track of the spawned Units
    private Dictionary<Player, List<Unit>> unitsByPlayer = new Dictionary<Player, List<Unit>>();




    // Start is called before the first frame update
    //If Player1 spawns a Unit it gets added to the unitsByPlayer List
    //Same for Player2
    //Assign the Bases for the different Players, to check if they won
    void Start()
    {
        List<Unit> units = new List<Unit>();
        basePlayer1 = GameObject.Find("BasePlayer1").GetComponent<Base>();
        units.Add(basePlayer1);
        unitsByPlayer.Add(Player.Player1, units);

        basePlayer2 = GameObject.Find("BasePlayer2").GetComponent<Base>();

        units = new List<Unit>
        {
            basePlayer2
        };
        unitsByPlayer.Add(Player.Player2, units);
   }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddUnit (Unit unit, Player player) {
        if (player == Player.Player1) {
            unitsByPlayer[Player.Player1].Add(unit);
        } else {
            unitsByPlayer[Player.Player2].Add(unit);
        }
        
    }
}
