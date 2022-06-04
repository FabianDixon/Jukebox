using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isFull; //meaning you already have an item

    public GameObject currentItem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            useActiveItem();
        }
    }

    void useActiveItem()
    {
        if (isFull == true)
        {
            EventManager.activeItemUse();
        }
        else
        {
            Debug.Log("There's no item to use");
        }
    }
}
