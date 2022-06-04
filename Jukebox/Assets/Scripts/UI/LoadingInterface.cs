using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingInterface : MonoBehaviour
{
    void Start()
    {
        EventManager.finishedLoadingLevel += disable;

        if (GameObject.FindGameObjectWithTag("UI") != null)
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void disable()
    {
        if (GameObject.FindGameObjectWithTag("UI") != null)
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<CanvasGroup>().alpha = 1;
        }
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        EventManager.finishedLoadingLevel -= disable;
    }
}
