using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameObject[] allObjects;
    List<GameObject> objectsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        allObjects = FindObjectsOfType<GameObject>();
        objectsToDisable = new List<GameObject>(allObjects);

        EventManager.GameOverEvent += gameOver;

        gameObject.SetActive(false);
    }

    void gameOver()
    {      
        //activate gameover screen
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        EventManager.GameOverEvent -= gameOver;
    }
}
