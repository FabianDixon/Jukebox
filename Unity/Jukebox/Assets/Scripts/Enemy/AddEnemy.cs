using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{
    public EnemyCount enemyCounter;
    public bool Added = false;

    private RoomTemplates templates;
    //private Transform enemyCounterTransform;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }
    
    void Update()
    {
        if (templates.disableRooms == true && Added == false)
        {
            add();
        }
    }

    void OnEnable()
    {
        enemyCounter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCount>();
    }

    void OnDisable()
    {
        delete();
        enemyCounter = null;
        Added = false;
    }

    void add()
    { 
        enemyCounter.enemies.Add(this.gameObject);
        Added = true;
    }

    public void delete()
    {
        enemyCounter.enemies.Remove(this.gameObject);
    }
}
