using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver_Buttons : MonoBehaviour
{

    public void Back2MainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameOverMenu"));
        Destroy(GameObject.FindGameObjectWithTag("EventManager"));
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
