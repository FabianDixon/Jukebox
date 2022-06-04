using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Text;
using Random = UnityEngine.Random;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
	public GameObject[] bossRooms;
	public GameObject[] roomEnablers;
	public GameObject[] doorFix;

	[SerializeField]
	private int counter = 0;
	private bool count = true;

	public List<GameObject> rooms;
    public int MaxRooms;
	private GameObject room;
	public GameObject blockedRoom;
	public GameObject secretRoom;
	public GameObject boss;

	private bool HaveSpawnedR;
	private bool HaveSpawnedL;
	private bool HaveSpawnedT;
	private bool HaveSpawnedB;

	public bool bossRoom = false;
	private bool fixedRooms = false;
	public bool disableRooms = false;

	public List<GameObject> listTreasure;
	private bool treasureRoom = false;
	private int randRoom;

	private GameObject[] UItoDestroy;

	void Update()
    {
        if (count == true)
        {
			counter += 1;
		}
		
        if (rooms.Count >= MaxRooms)
        {
			count = false;
			FixRooms();
			TreasureRoom();
			DisableRooms();
		}
		else if (rooms.Count < MaxRooms && counter >= 120)
        {
			UItoDestroy = GameObject.FindGameObjectsWithTag("UI");
			foreach (GameObject UI in UItoDestroy) { Destroy(UI); }

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

    }
	
	void FixRooms()
	{
		if (fixedRooms == false && bossRoom == true)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
				room = rooms[i];
                if (room.transform.Find("RoomSpawnPointR") != null)
                {
					HaveSpawnedR = room.transform.Find("RoomSpawnPointR").GetComponent<RoomSpawner>().spawned;
					if (HaveSpawnedR == false && room.transform.Find("DoorRight") != null)
					{
						GameObject newDoor = Instantiate(doorFix[2], room.transform.Find("DoorRight").position, room.transform.Find("DoorRight").rotation);
						newDoor.transform.parent = room.transform;
						Destroy(room.transform.Find("DoorRight").gameObject);
					}
				}

				if (room.transform.Find("RoomSpawnPointL") != null)
				{
					HaveSpawnedL = room.transform.Find("RoomSpawnPointL").GetComponent<RoomSpawner>().spawned;
					if (HaveSpawnedL == false && room.transform.Find("DoorLeft") != null)
					{
						GameObject newDoor = Instantiate(doorFix[3], room.transform.Find("DoorLeft").position, room.transform.Find("DoorLeft").rotation);
						newDoor.transform.parent = room.transform;
						Destroy(room.transform.Find("DoorLeft").gameObject);
					}
				}

				if (room.transform.Find("RoomSpawnPointT") != null)
				{
					HaveSpawnedT = room.transform.Find("RoomSpawnPointT").GetComponent<RoomSpawner>().spawned;
					if (HaveSpawnedT == false && room.transform.Find("DoorUp") != null)
					{
						GameObject newDoor = Instantiate(doorFix[0], room.transform.Find("DoorUp").position, room.transform.Find("DoorUp").rotation);
						newDoor.transform.parent = room.transform;
						Destroy(room.transform.Find("DoorUp").gameObject);
					}
				}

				if (room.transform.Find("RoomSpawnPointB") != null)
				{
					HaveSpawnedB = room.transform.Find("RoomSpawnPointB").GetComponent<RoomSpawner>().spawned;
					if (HaveSpawnedB == false && room.transform.Find("DoorDown") != null)
					{
						GameObject newDoor = Instantiate(doorFix[1], room.transform.Find("DoorDown").position, room.transform.Find("DoorDown").rotation);
						newDoor.transform.parent = room.transform;
						Destroy(room.transform.Find("DoorDown").gameObject);
					}
				}	
			}
			fixedRooms = true;			
        }
	}

	void TreasureRoom()
    {
		int children;
		int randTreasure;
        if (fixedRooms == true && treasureRoom == false)
        {
			randRoom = Random.Range(1, rooms.Count);
			//Debug.Log(randRoom);
			for (int i = 1; i < rooms.Count; i++)
            {
                if (i == randRoom)
                {
					room = rooms[i];
					children = room.transform.childCount;

					for (int j = 2; j < children; j++)
                    {
                        switch (room.transform.GetChild(j).name)
                        {
							case "DoorUp":
								break;
							case "DoorFixUp":
								break;
							case "DoorDown":
								break;
							case "DoorFixDown":
								break;
							case "DoorRight":
								break;
							case "DoorFixRight":
								break;
							case "DoorLeft":
								break;
							case "DoorFixLeft":
								break;
							case "Grid":
								break;
							case "Background":
								break;
                            default:
								Destroy(room.transform.GetChild(j).gameObject);
                                break;
                        }
                    }
					randTreasure = Random.Range(0, listTreasure.Count);
					GameObject Treasure = Instantiate(listTreasure[randTreasure], room.transform.position, room.transform.rotation);
					Treasure.transform.parent = room.transform;
					treasureRoom = true;
				}
			}
		}
    }

	void DisableRooms()
    {
        //After rooms are created disable all rooms except the first one and then enable them as the player goes in, while disabling them when the player moves out.
        if (disableRooms == false && treasureRoom == true)
        {
            for (int i = 1; i < rooms.Count; i++)
            {
				room = rooms[i];
                room.SetActive(false);
			}
			disableRooms = true;
			EventManager.levelConstructed();
		}
    }

	public void RemoveFromlistTreasure(string itemName)
    {
		StringBuilder sb = new StringBuilder();
		string newName;
		if (itemName.Contains("(Clone)"))
		{
			int removeIndex = itemName.IndexOf("(");
			for (int n = 0; n < removeIndex; n++)
			{
				sb.Append(itemName[n]);
			}
			newName = sb.ToString();
		}
        else
        {
			for (int n = 0; n < itemName.Length; n++)
			{
				sb.Append(itemName[n]);
			}
			newName = sb.ToString();
		}

		for (int i = 0; i < listTreasure.Count; i++)
        {
            if (newName == listTreasure[i].name)
            {
				listTreasure.Remove(listTreasure[i].gameObject);
			}
        }
	}

}
