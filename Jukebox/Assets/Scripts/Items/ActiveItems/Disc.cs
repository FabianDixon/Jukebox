using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.useActiveItemEvent += disc;
    }

    void OnDisable()
    {
        EventManager.useActiveItemEvent -= disc;
    }

    void disc()
    {
        Debug.Log("Using Disc");
    }
}
