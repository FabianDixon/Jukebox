using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private PlayerStats luckStat;
    public int luck;

    public bool isActiveFull; //meaning you already have an item
    public bool isConsumableFull;

    public GameObject currentActiveItem;
    public GameObject currentConsumable;

    void Start()
    {
        luck = luckStat.luck;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            useActiveItem();
        }

        if (Input.GetKeyDown(KeyCode.F))
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

    public void gainLuck(int luckGained)
    {
        luck += luckGained;
    }

    public void looseLuck(int luckLost)
    {
        luck -= luckLost;
    }
}
