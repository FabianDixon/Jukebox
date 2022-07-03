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
    private int doorID;

    public Vector3 relativetransform;
    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    private bool otherSpawned = false;

    private GameObject[] doorFix;

    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        doorFix = transform.parent.parent.gameObject.GetComponent<DoorSpawner>().doorFix;
        switch (openingDirection)
        {
            case 1:
                doorID = 0;
                break;
            case 2:
                doorID = 2;
                break;
            case 3:
                doorID = 1;
                break;
            case 4:
                doorID = 3;
                break;
            default:
                break;
        }
        if (transform.position.x == 0 && transform.position.y == 0)
        {
            spawned = true;
        }
        else
        {
            Destroy(this.gameObject, 4.0f);
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            //Invoke("Spawn", 0.8f);
            Spawn();
        }
    }

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpawnPointDestroyer")
        {
            spawned = true;
            Instantiate(doorFix[doorID], transform.parent.position, transform.parent.rotation);
            Destroy(this.gameObject.transform.parent.gameObject, 4.0f);
        }
        else if (other.gameObject.tag == "RoomSpawnPoint")
        {
            //if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            //{
            //    //Poner algo en el medio de dos cuartos que dan a la nada
            //    Instantiate(templates.secretRoom, transform.position, Quaternion.identity);
            //    if (openingDirection == 1)
            //    {
            //        GameObject newDoor1 = Instantiate(templates.doorFix[0], transform.parent.Find("TopDoor").position, transform.parent.Find("TopDoor").rotation);
            //        newDoor1.transform.parent = transform.parent;
            //        switch (other.GetComponent<RoomSpawner>().openingDirection)
            //        {
            //            case 4:
            //                if (other.transform.Find("LeftDoor") != null)
            //                {
            //                    GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("LeftDoor").position, other.transform.Find("LeftDoor").rotation);
            //                    newDoor.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("LeftDoor").gameObject);
            //                }
            //                break;
            //            case 3:
            //                if (other.transform.Find("RightDoor") != null)
            //                {
            //                    GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("RightDoor").position, other.transform.Find("RightDoor").rotation);
            //                    newDoor2.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("RightDoor").gameObject);
            //                }
            //                break;
            //            case 2:
            //                if (other.transform.Find("BottomDoor") != null)
            //                {
            //                    GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("BottomDoor").position, other.transform.Find("BottomDoor").rotation);
            //                    newDoor3.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("BottomDoor").gameObject);
            //                }
            //                break;
            //            case 1:
            //                if (other.transform.Find("TopDoor"))
            //                {
            //                    GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("TopDoor").position, other.transform.Find("TopDoor").rotation);
            //                    newDoor4.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("TopDoor").gameObject);
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //        Destroy(transform.parent.Find("TopDoor").gameObject);
            //    }
            //    else if (openingDirection == 2)
            //    {
            //        GameObject newDoor1 = Instantiate(templates.doorFix[1], transform.parent.Find("BottomDoor").position, transform.parent.Find("BottomDoor").rotation);
            //        newDoor1.transform.parent = transform.parent;
            //        switch (other.GetComponent<RoomSpawner>().openingDirection)
            //        {
            //            case 4:
            //                if (other.transform.Find("LeftDoor") != null)
            //                {
            //                    GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("LeftDoor").position, other.transform.Find("LeftDoor").rotation);
            //                    newDoor.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("LeftDoor").gameObject);
            //                }
            //                break;
            //            case 3:
            //                if (other.transform.Find("RightDoor") != null)
            //                {
            //                    GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("RightDoor").position, other.transform.Find("RightDoor").rotation);
            //                    newDoor2.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("RightDoor").gameObject);
            //                }
            //                break;
            //            case 2:
            //                if (other.transform.Find("BottomDoor") != null)
            //                {
            //                    GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("BottomDoor").position, other.transform.Find("BottomDoor").rotation);
            //                    newDoor3.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("BottomDoor").gameObject);
            //                }
            //                break;
            //            case 1:
            //                if (other.transform.Find("TopDoor"))
            //                {
            //                    GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("TopDoor").position, other.transform.Find("TopDoor").rotation);
            //                    newDoor4.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("TopDoor").gameObject);
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //        Destroy(transform.parent.Find("BottomDoor").gameObject);
            //    }
            //    else if (openingDirection == 3)
            //    {
            //        GameObject newDoor1 = Instantiate(templates.doorFix[2], transform.parent.Find("RightDoor").position, transform.parent.Find("RightDoor").rotation);
            //        newDoor1.transform.parent = transform.parent;
            //        switch (other.GetComponent<RoomSpawner>().openingDirection)
            //        {
            //            case 4:
            //                if (other.transform.Find("LeftDoor") != null)
            //                {
            //                    GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("LeftDoor").position, other.transform.Find("LeftDoor").rotation);
            //                    newDoor.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("LeftDoor").gameObject);
            //                }
            //                break;
            //            case 3:
            //                if (other.transform.Find("RightDoor") != null)
            //                {
            //                    GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("RightDoor").position, other.transform.Find("RightDoor").rotation);
            //                    newDoor2.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("RightDoor").gameObject);
            //                }
            //                break;
            //            case 2:
            //                if (other.transform.Find("BottomDoor") != null)
            //                {
            //                    GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("BottomDoor").position, other.transform.Find("BottomDoor").rotation);
            //                    newDoor3.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("BottomDoor").gameObject);
            //                }
            //                break;
            //            case 1:
            //                if (other.transform.Find("TopDoor"))
            //                {
            //                    GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("TopDoor").position, other.transform.Find("TopDoor").rotation);
            //                    newDoor4.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("TopDoor").gameObject);
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //        Destroy(transform.parent.Find("RightDoor").gameObject);
            //    }
            //    else if (openingDirection == 4)
            //    {
            //        GameObject newDoor1 = Instantiate(templates.doorFix[3], transform.parent.Find("LeftDoor").position, transform.parent.Find("LeftDoor").rotation);
            //        newDoor1.transform.parent = transform.parent;
            //        switch (other.GetComponent<RoomSpawner>().openingDirection)
            //        {
            //            case 4:
            //                if (other.transform.Find("LeftDoor") != null)
            //                {
            //                    GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("LeftDoor").position, other.transform.Find("LeftDoor").rotation);
            //                    newDoor.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("LeftDoor").gameObject);
            //                }
            //                break;
            //            case 3:
            //                if (other.transform.Find("RightDoor") != null)
            //                {
            //                    GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("RightDoor").position, other.transform.Find("RightDoor").rotation);
            //                    newDoor2.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("RightDoor").gameObject);
            //                }
            //                break;
            //            case 2:
            //                if (other.transform.Find("BottomDoor") != null)
            //                {
            //                    GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("BottomDoor").position, other.transform.Find("BottomDoor").rotation);
            //                    newDoor3.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("BottomDoor").gameObject);
            //                }
            //                break;
            //            case 1:
            //                if (other.transform.Find("TopDoor"))
            //                {
            //                    GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("TopDoor").position, other.transform.Find("TopDoor").rotation);
            //                    newDoor4.transform.parent = other.transform.parent.transform;
            //                    Destroy(other.transform.parent.Find("TopDoor").gameObject);
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //        Destroy(transform.parent.Find("LeftDoor").gameObject);
            //    }
            //    spawned = true;
            //}
            //else if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
            //{
            //    otherSpawned = true;
            //}
            //else if (other.GetComponent<RoomSpawner>().spawned == false && spawned == true)
            //{
            //    switch (other.GetComponent<RoomSpawner>().openingDirection)
            //    {
            //        case 4:
            //            if (other.transform.Find("LeftDoor") != null)
            //            {
            //                GameObject newDoor = Instantiate(templates.doorFix[3], other.transform.Find("LeftDoor").position, other.transform.Find("LeftDoor").rotation);
            //                newDoor.transform.parent = other.transform.parent.transform;
            //                Destroy(other.transform.parent.Find("LeftDoor").gameObject);
            //            }
            //            break;
            //        case 3:
            //            if (other.transform.Find("RightDoor") != null)
            //            {
            //                GameObject newDoor2 = Instantiate(templates.doorFix[2], other.transform.Find("RightDoor").position, other.transform.Find("RightDoor").rotation);
            //                newDoor2.transform.parent = other.transform.parent.transform;
            //                Destroy(other.transform.parent.Find("RightDoor").gameObject);
            //            }
            //            break;
            //        case 2:
            //            if (other.transform.Find("BottomDoor") != null)
            //            {
            //                GameObject newDoor3 = Instantiate(templates.doorFix[1], other.transform.Find("BottomDoor").position, other.transform.Find("BottomDoor").rotation);
            //                newDoor3.transform.parent = other.transform.parent.transform;
            //                Destroy(other.transform.parent.Find("BottomDoor").gameObject);
            //            }
            //            break;
            //        case 1:
            //            if (other.transform.Find("TopDoor"))
            //            {
            //                GameObject newDoor4 = Instantiate(templates.doorFix[0], other.transform.Find("TopDoor").position, other.transform.Find("TopDoor").rotation);
            //                newDoor4.transform.parent = other.transform.parent.transform;
            //                Destroy(other.transform.parent.Find("TopDoor").gameObject);
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //    other.GetComponent<RoomSpawner>().spawned = true;
            //}
        }
    }
}
