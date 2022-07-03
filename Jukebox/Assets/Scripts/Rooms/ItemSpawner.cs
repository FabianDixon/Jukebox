using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    [SerializeField]
    private int randomChance;
    [SerializeField]
    private int probability = 50;
    private int randomItem;

    [SerializeField]
    private bool isPlayer;
    private Item playerItem;
    [SerializeField]
    private int playerLuck;

    private GameObject item;

    void Start()
    {
        isPlayer = false;
        EventManager.finishedLoadingLevel += getProbability;
    }

    void OnEnable()
    {
        EventManager.roomCompletedEvent += SpawnItem;

        if (isPlayer == true)
        {
            playerItem = GameObject.FindGameObjectWithTag("Player").GetComponent<Item>();
            playerLuck = playerItem.luck;

            probability -= playerLuck;
        }
        randomChance = Random.Range(0, probability);
    }

    void OnDisable()
    {
        EventManager.roomCompletedEvent -= SpawnItem;
        probability = 50;
        if (item != null)
        {
            Destroy(item);
        }
    }

    void OnDestroy()
    {
        EventManager.roomCompletedEvent -= SpawnItem;
    }

    void getProbability()
    {
        isPlayer = true;
    }

    void SpawnItem()
    {
        if (randomChance <= 10)
        {
            randomItem = Random.Range(0, items.Length);
            item = Instantiate(items[randomItem], transform.position, Quaternion.identity);
            item.transform.parent = transform;
        }

        EventManager.roomCompletedEvent -= SpawnItem;
    }
}
