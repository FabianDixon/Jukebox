using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] doorFix;

    private GameObject[] Spawn;

    private int randNumDoors;
    private int randDoorPos;
    [SerializeField]
    private List<int> doorType = new List<int> { 0, 1, 2, 3 };
    [SerializeField]
    private List<int> doorFixType = new List<int> { 0, 1, 2, 3 };
    //0 -> Top door
    //1 -> Right door
    //2 -> Bottom door
    //3 -> Left door

    private RoomTemplates templates;
    private GameObject child;

    public int openingDirection;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        switch (openingDirection)
        {
            case 1:
                doorType.RemoveAt(2);
                doorFixType.RemoveAt(2);
                break;
            case 2:
                doorType.RemoveAt(0);
                doorFixType.RemoveAt(0);
                break;
            case 3:
                doorType.RemoveAt(3);
                doorFixType.RemoveAt(3);
                break;
            case 4:
                doorType.RemoveAt(1);
                doorFixType.RemoveAt(1);
                break;
            default:
                break;
        }
        Invoke("DoorsToSpawn", 0.8f);
    }

    void DoorsToSpawn()
    {
        int maxRooms = templates.MaxRooms;
        int count = templates.rooms.Count;
        if (count < maxRooms)
        {
            
            if ((maxRooms - count) > 2)
            {
                randNumDoors = Random.Range(1, doorType.Count + 1); //limite superior exclusivo e inferior inclusivo.
                for (int i = 0; i < randNumDoors; i++)
                {
                    randDoorPos = Random.Range(0, doorType.Count);
                    spawnDoor(doorType[randDoorPos], true);
                    doorType.RemoveAt(randDoorPos);
                    doorFixType.RemoveAt(randDoorPos);
                }
            }
            else if ((maxRooms - count) == 2)
            {
                randNumDoors = Random.Range(1, doorType.Count);
                for (int i = 0; i < randNumDoors; i++)
                {
                    randDoorPos = Random.Range(0, doorType.Count);
                    spawnDoor(doorType[randDoorPos], true);
                    doorType.RemoveAt(randDoorPos);
                    doorFixType.RemoveAt(randDoorPos);
                }
            }
            else if ((maxRooms - count) == 1)
            {
                randNumDoors = 1;
                for (int i = 0; i < randNumDoors; i++)
                {
                    randDoorPos = Random.Range(0, doorType.Count);
                    spawnDoor(doorType[randDoorPos], true);
                    doorType.RemoveAt(randDoorPos);
                    doorFixType.RemoveAt(randDoorPos);
                }
            }
        }
        for (int i = 0; i < doorFixType.Count; i++)
        {
            spawnDoor(doorFixType[i], false);
        }
    }

    void spawnDoor(int doorPos, bool door)
    {
        
        Vector2 pos = transform.position;
        if (door == true)
        {
            Spawn = doors;
        }
        else if (door == false)
        {
            Spawn = doorFix;
        }
        switch (doorPos)
        {
            case 3:
                pos.x = pos.x - 256f;
                child = Instantiate(Spawn[3], pos, Quaternion.identity);
                child.transform.parent = transform;
                break;
            case 2:
                pos.y = pos.y - 124f;
                child = Instantiate(Spawn[2], pos, Quaternion.identity);
                child.transform.parent = transform;
                break;
            case 1:
                pos.x = pos.x + 256f; 
                child = Instantiate(Spawn[1], pos, Quaternion.identity);
                child.transform.parent = transform;
                break;
            case 0:
                pos.y = pos.y + 116f;
                child = Instantiate(Spawn[0], pos, Quaternion.identity);
                child.transform.parent = transform;
                break;
            default:
                break;
        }
    }
}
