using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawner : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public int maxEnemies;
    private int randNumEnemies;

    Vector2 targetCircleCenter;


    // Start is called before the first frame update
    void Start()
    {
        targetCircleCenter = new Vector2(transform.position.x, transform.position.y);

        randNumEnemies = Random.Range(1, maxEnemies + 1); //limite superior exclusivo e inferior inclusivo.
        for (int i = 0; i < randNumEnemies; i++)
        {
            Vector2 pos = targetCircleCenter + Random.insideUnitCircle * Random.Range(10, 50);
            GameObject child = Instantiate(spawnPoints[0], pos, Quaternion.identity);
            child.transform.parent = transform;
        }
    }

}
