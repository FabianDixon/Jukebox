using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool spawned = false;

    private int randEnemy;
    public GameObject[] enemies;
    
    void Start()
    {
        Invoke("spawnEnemy", 0.2f);
    }

    void spawnEnemy()
    {
        if (spawned == false)
        {
            randEnemy = Random.Range(0, enemies.Length);
            GameObject child = Instantiate(enemies[randEnemy], transform.position, transform.rotation);
            child.transform.parent = transform;
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpawnPoint" && spawned == false)
        {
            Destroy(this.gameObject);
        }
    }
}
