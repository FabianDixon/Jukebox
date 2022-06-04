using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.useActiveItemEvent += radio;
    }

    void OnDisable()
    {
        EventManager.useActiveItemEvent -= radio;
    }

    void radio()
    {
        Debug.Log("Using Radio");
    }
}
