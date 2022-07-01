using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isActiveFull; //meaning you already have an item
    public bool isConsumableFull;

    public GameObject currentActiveItem;
    public GameObject currentConsumable;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            useActiveItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            useConsumable();
        }
    }

    void useActiveItem()
    {
        if (isActiveFull == true)
        {
            EventManager.activeItemUse();
        }
        else
        {
            Debug.Log("There's no item to use");
        }
    }

    void useConsumable()
    {
        if (isConsumableFull == true)
        {
            EventManager.consumableUse();
        }
        else
        {
            Debug.Log("There's no item to use");
        }
    }
}
