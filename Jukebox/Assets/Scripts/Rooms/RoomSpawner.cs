using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    //1 --> need bottom door
    //2 --> need top door
    //3 --> need left door
    //4 --> need right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    private bool otherSpawned = false;

    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x == 0 && transform.position.y == 0)
        {
            spawned = true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, waitTime);
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            Invoke("Spawn", 0.8f);
        }  
    }

    // Update is called once per frame
    void Spawn()
    {
        if ((templates.rooms.Count < templates.MaxRooms) && spawned == false && otherSpawned == false)
        {
            if (openingDirection == 1)
            {
                //Need room with Bottom.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                //Need room with top door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                //Need room with left door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                //Need room with right door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
        else if ((templates.rooms.Count >= templates.MaxRooms) && templates.bossRoom == false && spawned == false)
        {
            GameObject Boss = Instantiate(templates.boss, transform.position, Quaternion.identity);
            if (openingDirection == 1)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - 15.5f);
                GameObject enabler = Instantiate(templates.roomEnablers[0], pos, Quaternion.identity);
                enabler.transform.parent = Boss.transform;
                GameObject bossRoom = Instantiate(templates.bossRooms[0], transform.position, Quaternion.identity);
                bossRoom.transform.parent = Boss.transform;
                bossRoom.SetActive(false);
            }
            else if (openingDirection == 2)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y + 11f);
                GameObject enabler = Instantiate(templates.roomEnablers[1], pos, Quaternion.identity);
                enabler.transform.parent = Boss.transform;
                GameObject bossRoom = Instantiate(templates.bossRooms[1], transform.position, Quaternion.identity);
                bossRoom.transform.parent = Boss.transform;
                bossRoom.SetActive(false);
            }
            else if (openingDirection == 3)
            {
                Vector2 pos = new Vector2(transform.position.x - 17f, transform.position.y);
                GameObject enabler = Instantiate(templates.roomEnablers[2], pos, Quaternion.identity);
                enabler.transform.parent = Boss.transform;
                GameObject bossRoom = Instantiate(templates.bossRooms[2], transform.position, Quaternion.identity);
                bossRoom.transform.parent = Boss.transform;
                bossRoom.SetActive(false);
            }
            else if (openingDirection == 4)
            {
                Vector2 pos = new Vector2(transform.position.x + 17f, transform.position.y);
                GameObject enabler = Instantiate(templates.roomEnablers[3], pos, Quaternion.identity);
                enabler.transform.parent = Boss.transform;
                GameObject bossRoom = Instantiate(templates.bossRooms[3], transform.position, Quaternion.identity);
                bossRoom.transform.parent = Boss.transform;
                bossRoom.SetActive(false);
            }
            templates.bossRoom = true;
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpawnPointDestroyer")
        {
            spawned = true;
        }
        else if (other.gameObject.tag == "RoomSpawnPoint")
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //Poner algo en el medio de dos cuartos que dan a la nada
                Instantiate(templates.secretRoom, transform.position, Quaternion.identity);
                if (openingDirection == 1)
                {
                    GameObject newDoor1 = Instantiate(templates.doorFix[0], transform.parent.Find("DoorUp").position, transform.parent.Find("DoorUp").rotation);
                    newDoor1.transform.parent = transform.parent;
                    switch (other.GetComponent<RoomSpawner>().openingDirection)
                    {
                        case 4:
                            if (other.transform.Find("DoorLeft") != null)
                            {
                                GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("DoorLeft").position, other.transform.Find("DoorLeft").rotation);
                                newDoor.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorLeft").gameObject);
                            }
                            break;
                        case 3:
                            if (other.transform.Find("DoorRight") != null)
                            {
                                GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("DoorRight").position, other.transform.Find("DoorRight").rotation);
                                newDoor2.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorRight").gameObject);
                            }
                            break;
                        case 2:
                            if (other.transform.Find("DoorDown") != null)
                            {
                                GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("DoorDown").position, other.transform.Find("DoorDown").rotation);
                                newDoor3.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorDown").gameObject);
                            }
                            break;
                        case 1:
                            if (other.transform.Find("DoorUp"))
                            {
                                GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("DoorUp").position, other.transform.Find("DoorUp").rotation);
                                newDoor4.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorUp").gameObject);
                            }
                            break;
                        default:
                            break;
                    }
                    Destroy(transform.parent.Find("DoorUp").gameObject);
                }
                else if (openingDirection == 2)
                {
                    GameObject newDoor1 = Instantiate(templates.doorFix[1], transform.parent.Find("DoorDown").position, transform.parent.Find("DoorDown").rotation);
                    newDoor1.transform.parent = transform.parent;
                    switch (other.GetComponent<RoomSpawner>().openingDirection)
                    {
                        case 4:
                            if (other.transform.Find("DoorLeft") != null)
                            {
                                GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("DoorLeft").position, other.transform.Find("DoorLeft").rotation);
                                newDoor.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorLeft").gameObject);
                            }
                            break;
                        case 3:
                            if (other.transform.Find("DoorRight") != null)
                            {
                                GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("DoorRight").position, other.transform.Find("DoorRight").rotation);
                                newDoor2.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorRight").gameObject);
                            }
                            break;
                        case 2:
                            if (other.transform.Find("DoorDown") != null)
                            {
                                GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("DoorDown").position, other.transform.Find("DoorDown").rotation);
                                newDoor3.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorDown").gameObject);
                            }
                            break;
                        case 1:
                            if (other.transform.Find("DoorUp"))
                            {
                                GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("DoorUp").position, other.transform.Find("DoorUp").rotation);
                                newDoor4.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorUp").gameObject);
                            }
                            break;
                        default:
                            break;
                    }
                    Destroy(transform.parent.Find("DoorDown").gameObject);
                }
                else if (openingDirection == 3)
                {
                    GameObject newDoor1 = Instantiate(templates.doorFix[2], transform.parent.Find("DoorRight").position, transform.parent.Find("DoorRight").rotation);
                    newDoor1.transform.parent = transform.parent;
                    switch (other.GetComponent<RoomSpawner>().openingDirection)
                    {
                        case 4:
                            if (other.transform.Find("DoorLeft") != null)
                            {
                                GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("DoorLeft").position, other.transform.Find("DoorLeft").rotation);
                                newDoor.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorLeft").gameObject);
                            }
                            break;
                        case 3:
                            if (other.transform.Find("DoorRight") != null)
                            {
                                GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("DoorRight").position, other.transform.Find("DoorRight").rotation);
                                newDoor2.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorRight").gameObject);
                            }
                            break;
                        case 2:
                            if (other.transform.Find("DoorDown") != null)
                            {
                                GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("DoorDown").position, other.transform.Find("DoorDown").rotation);
                                newDoor3.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorDown").gameObject);
                            }
                            break;
                        case 1:
                            if (other.transform.Find("DoorUp"))
                            {
                                GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("DoorUp").position, other.transform.Find("DoorUp").rotation);
                                newDoor4.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorUp").gameObject);
                            }
                            break;
                        default:
                            break;
                    }
                    Destroy(transform.parent.Find("DoorRight").gameObject);
                }
                else if (openingDirection == 4)
                {
                    GameObject newDoor1 = Instantiate(templates.doorFix[3], transform.parent.Find("DoorLeft").position, transform.parent.Find("DoorLeft").rotation);
                    newDoor1.transform.parent = transform.parent;
                    switch (other.GetComponent<RoomSpawner>().openingDirection)
                    {
                        case 4:
                            if (other.transform.Find("DoorLeft") != null)
                            {
                                GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("DoorLeft").position, other.transform.Find("DoorLeft").rotation);
                                newDoor.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorLeft").gameObject);
                            }
                            break;
                        case 3:
                            if (other.transform.Find("DoorRight") != null)
                            {
                                GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("DoorRight").position, other.transform.Find("DoorRight").rotation);
                                newDoor2.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorRight").gameObject);
                            }
                            break;
                        case 2:
                            if (other.transform.Find("DoorDown") != null)
                            {
                                GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("DoorDown").position, other.transform.Find("DoorDown").rotation);
                                newDoor3.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorDown").gameObject);
                            }
                            break;
                        case 1:
                            if (other.transform.Find("DoorUp"))
                            {
                                GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("DoorUp").position, other.transform.Find("DoorUp").rotation);
                                newDoor4.transform.parent = other.transform.parent.transform;
                                Destroy(other.transform.parent.Find("DoorUp").gameObject);
                            }
                            break;
                        default:
                            break;
                    }
                    Destroy(transform.parent.Find("DoorLeft").gameObject);
                }
                spawned = true;
            }
            else if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
            {
                otherSpawned = true;
            }
            else if (other.GetComponent<RoomSpawner>().spawned == false && spawned == true)
            {
                switch (other.GetComponent<RoomSpawner>().openingDirection)
                {
                    case 4:
                        if (other.transform.Find("DoorLeft") != null)
                        {
                            GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("DoorLeft").position, other.transform.Find("DoorLeft").rotation);
                            newDoor.transform.parent = other.transform.parent.transform;
                            Destroy(other.transform.parent.Find("DoorLeft").gameObject);
                        }
                        break;
                    case 3:
                        if (other.transform.Find("DoorRight") != null)
                        {
                            GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("DoorRight").position, other.transform.Find("DoorRight").rotation);
                            newDoor2.transform.parent = other.transform.parent.transform;
                            Destroy(other.transform.parent.Find("DoorRight").gameObject);
                        }
                        break;
                    case 2:
                        if (other.transform.Find("DoorDown") != null)
                        {
                            GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("DoorDown").position, other.transform.Find("DoorDown").rotation);
                            newDoor3.transform.parent = other.transform.parent.transform;
                            Destroy(other.transform.parent.Find("DoorDown").gameObject);
                        }
                        break;
                    case 1:
                        if (other.transform.Find("DoorUp"))
                        {
                            GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("DoorUp").position, other.transform.Find("DoorUp").rotation);
                            newDoor4.transform.parent = other.transform.parent.transform;
                            Destroy(other.transform.parent.Find("DoorUp").gameObject);
                        }
                        break;
                    default:
                        break;
                }
                other.GetComponent<RoomSpawner>().spawned = true;
            }
        }
    }
}
