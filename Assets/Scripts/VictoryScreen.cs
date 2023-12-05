using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void Setup(){
        gameObject.SetActive(true);
    }

    public void MainMenuButton() 
    {
        SceneManager.LoadScene(0);
    }
    
    public void RestartButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
}