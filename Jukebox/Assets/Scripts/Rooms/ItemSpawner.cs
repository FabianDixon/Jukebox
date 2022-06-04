using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    private int randomChance;
    private int probability = 50;
    private int randomItem;

    void OnEnable()
    {
        EventManager.roomCompletedEvent += SpawnItem;

        //probability -= playerLuck;
        randomChance = Random.Range(0, probability);
    }

    void OnDisable()
    {
        EventManager.roomCompletedEvent -= SpawnItem;
    }

    void OnDestroy()
    {
        EventManager.roomCompletedEvent -= SpawnItem;
    }

    void SpawnItem()
    {
        if (randomChance == 7)
        {
            randomItem = Random.Range(0, items.Length);
            GameObject child = Instantiate(items[randomItem], transform.position, Quaternion.identity);
            child.transform.parent = transform;
        }

        EventManager.roomCompletedEvent -= SpawnItem;
    }
}
