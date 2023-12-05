using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab ; // The unit you want to spawn
    public Transform spawnPoint;  // The position where the unit should spawn
    private Button spawnButton;    // Reference to your button
    private TextMeshProUGUI spawnButtonText;  // Text component of the button

    public GameManager gameManager;
    public GameObject moneyRef_P1;   // Reference to player money
    public GameManager.Player player;
    public float cooldownTime = 2.0f; // Adjust this as needed for your cooldown time
    private float nextSpawnTime = 0.0f;


    private void Start()
    {
        spawnButton = GetComponent<Button>();
        spawnButtonText = GetComponentInChildren<TextMeshProUGUI>(); // Make sure to have a TextMeshProUGUI component as a child of the button
        spawnButton.onClick.AddListener(SpawnUnit);
        nextSpawnTime = Time.time + cooldownTime;
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            spawnButtonText.text = "";
            spawnButton.interactable = true;
        }
        else
        {
            float timeLeft = nextSpawnTime - Time.time;
            spawnButtonText.text = timeLeft.ToString("F1") + "s";
            spawnButton.interactable = false;
        }
    }

    public void SpawnUnit()
    {
        if (Time.time > nextSpawnTime && moneyRef_P1.GetComponent<moneyCountP1>().money >= unitPrefab.GetComponent<Unit>().GetCost())
        {
            Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
            gameManager.AddUnit(unitPrefab.GetComponent<Unit>(), player);
            nextSpawnTime = Time.time + cooldownTime;
        }
        else
        {
            //Insert Pop-Up
            Debug.Log("Not enough money!");
        }
    }
}