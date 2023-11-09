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
    Dictionary<Player, List<Unit>> unitsByPlayer = new Dictionary<Player, List<Unit>>();



    // Start is called before the first frame update
    //If Player1 spawns a Unit it gets added to the unitsByPlayer List
    //Same for Player2
    //Assign the Bases for the different Players, to check if they won
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
