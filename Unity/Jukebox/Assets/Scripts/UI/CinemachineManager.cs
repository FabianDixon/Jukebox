using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam1;

    [SerializeField]
    private CinemachineVirtualCamera vcam2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.finishedLoadingLevel += switchPriorities;
    }

    void switchPriorities()
    {
        vcam1.Priority = 1;
        vcam2.Priority = 0;
    }

    void OnDestroy()
    {
        EventManager.finishedLoadingLevel -= switchPriorities;
    }
}
