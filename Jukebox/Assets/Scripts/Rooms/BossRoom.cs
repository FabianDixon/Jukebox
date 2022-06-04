using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
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

    void LoadNextLevel()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;

                scenesToLoad[i].allowSceneActivation = false;
                yield return new WaitForSeconds(4);
                scenesToLoad[i].allowSceneActivation = true;

                yield return null;
            }
        }
    }
}
