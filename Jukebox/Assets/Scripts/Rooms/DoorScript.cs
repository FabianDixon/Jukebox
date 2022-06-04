using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Collider2D BoxCollider;
    private GameObject enemyCounter;
    private RoomTemplates templates;

    public Collider2D triggerCollider;

    private bool enemyCheck = false;
    public bool completed = false;

    public int doorType;
    //1 -> Top door
    //2 -> Right door
    //3 -> Bottom door
    //4 -> Left door

    void Start()
    {
        BoxCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        EventManager.roomCompletedEvent += doorOpen;
        EventManager.roomCompletedEvent += isCompleted;
    }

    void Update()
    {
        if (templates.disableRooms == true && enemyCheck == false)
        {
            enemyCounter = GameObject.FindGameObjectWithTag("EnemyCounter");
            enemyCheck = true;
        }
    }

    public void doorOpen()
    {
        BoxCollider.enabled = false;
        if (enemyCounter != null){ enemyCounter.SetActive(false); }
        EventManager.roomCompletedEvent -= doorOpen;
    }

    public void isCompleted()
    {
        completed = true;
    }

    void OnEnable()
    {
        BoxCollider.enabled = true;
        EventManager.roomCompletedEvent += doorOpen;
        completed = false;
    }

    void OnDisable()
    {
        EventManager.roomCompletedEvent -= doorOpen;
        completed = false;
    }

    void OnDestroy()
    {
        EventManager.roomCompletedEvent -= doorOpen;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            if (completed == true)
            {
                triggerCollider.enabled = false;
                EventManager.crossedDoor(doorType);
            }
        }
    }
}
