using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnabler : MonoBehaviour
{
    
    private Transform Parent;

    void Start()
    {
        Parent = transform.parent.parent.parent;
        transform.parent = Parent;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            int numChild = Parent.childCount;
            Parent.GetChild(1).gameObject.SetActive(true);
        }
    }
    
}
