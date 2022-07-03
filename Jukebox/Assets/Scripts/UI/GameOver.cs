using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //GameObject[] allObjects;
    //List<GameObject> objectsToDisable;

    [SerializeField]
    private GameObject GameOverUI;

    void Start()
    {
        //allObjects = FindObjectsOfType<GameObject>();
        //objectsToDisable = new List<GameObject>(allObjects);
        DontDestroyOnLoad(this.gameObject);
        EventManager.GameOverEvent += gameOver;
    }

    void gameOver()
    {
        //activate gameover screen
        GameOverUI.SetActive(true);
    }

    void OnDestroy()
    {
        EventManager.GameOverEvent -= gameOver;
    }
}
