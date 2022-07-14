using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{

    void Start()
    {
        EventManager.bossRoomClearEvent += LoadNextLevel;
    }

    void OnEnable()
    {
        EventManager.roomCompletedEvent += bossCleared;
    }

    void OnDisable()
    {
        EventManager.roomCompletedEvent -= bossCleared;
    }

    void OnDestroy()
    {
        EventManager.roomCompletedEvent -= bossCleared;
        EventManager.bossRoomClearEvent -= LoadNextLevel;
    }

    void bossCleared()
    {
        EventManager.bossRoomCompleted();
    }

    void EndScreen()
    {
        EventManager.OnPlayerDeath();
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
