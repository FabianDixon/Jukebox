using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UI : MonoBehaviour
{
    void Start()
    {
        EventManager.spawnPlayerEvent += enable;
        DontDestroyOnLoad(this.gameObject);
        gameObject.SetActive(false);
    }

    void enable()
    {
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        EventManager.spawnPlayerEvent -= enable;
    }
}
