using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab; // The unit you want to spawn
    public Transform spawnPoint;  // The position where the unit should spawn

    private Button spawnButton;    // Reference to your button

    private void Start()
    {
        spawnButton = GetComponent<Button>();
        spawnButton.onClick.AddListener(SpawnUnit);
    }

    void Update(){
        if(Input.GetButtonDown("Jump")){
            SpawnUnit();
        }
    }

    private void SpawnUnit()
    {
        Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
