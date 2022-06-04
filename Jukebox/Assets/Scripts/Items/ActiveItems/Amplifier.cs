using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplifier : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.useActiveItemEvent += amplifier;
    }

    void OnDisable()
    {
        EventManager.useActiveItemEvent -= amplifier;
    }

    void amplifier()
    {
        Debug.Log("Using Amplifier");
    }
}
