using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyCountP2 : MonoBehaviour
{
    public int money;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        money = 100;
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        moneyText.text = money.ToString();
    }

    public void substractMoney(int moneyToSubstract)
    {
        if(money - moneyToSubstract < 0)
        {
            //Debug.log("Attempted negative transaction");
        }

        else
        {
            money -= moneyToSubstract;
            moneyText.text = money.ToString();
        }
    }
}
