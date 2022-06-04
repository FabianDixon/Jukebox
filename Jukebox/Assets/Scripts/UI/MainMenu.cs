using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingInterface;
    public GameObject mainMenu;
    public GameObject SettingsMenu;
    public Image LoadingProgressBar;

    private GameObject[] UItoDestroy;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        UItoDestroy = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject UI in UItoDestroy)
        {
            Destroy(UI);
        }
    }

    public void PlayGame()
    {
        HideMenu(mainMenu);
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadingScreen());
    }

    public void Settings()
    {
        HideMenu(mainMenu);
        SettingsMenu.SetActive(true);
    }

    public void Back()
    {
        HideMenu(SettingsMenu);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void HideMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    //private void finishLoad()
    //{
    //    scenesToLoad.allowSceneActivation = true;
    //}

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                LoadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                //scenesToLoad[i].allowSceneActivation = false;

                yield return null;
            }
        }
    }
}
