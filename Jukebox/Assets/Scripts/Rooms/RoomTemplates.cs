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

	[SerializeField]
	private int counter = 0;
	private bool count = true;

	public List<GameObject> rooms;
    public int MaxRooms;
	private GameObject room;
	private GameObject lastRoom;
	private GameObject BossR;
	private GameObject BossR_Enabler;
	public GameObject blockedRoom;
	public GameObject secretRoom;

	private bool HaveSpawnedR;
	private bool HaveSpawnedL;
	private bool HaveSpawnedT;
	private bool HaveSpawnedB;

	public bool bossRoom = false;
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
			Invoke("BossRoom", 0.8f);
		}
		//else if (rooms.Count < MaxRooms && counter >= 120)
  //      {
		//	UItoDestroy = GameObject.FindGameObjectsWithTag("UI");
		//	foreach (GameObject UI in UItoDestroy) { Destroy(UI); }

		//	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		//}

    }
	
	void BossRoom()
    {
        if (bossRoom == false)
        {
			lastRoom = rooms[rooms.Count-1];

			if (lastRoom.name == "RoomB(Clone)")
			{
				BossR = Instantiate(bossRooms[0], lastRoom.transform.position, lastRoom.transform.rotation);
			}
			else if (lastRoom.name == "RoomT(Clone)")
			{
				BossR = Instantiate(bossRooms[1], lastRoom.transform.position, lastRoom.transform.rotation);
			}
			else if (lastRoom.name == "RoomL(Clone)")
			{
				BossR = Instantiate(bossRooms[2], lastRoom.transform.position, lastRoom.transform.rotation);
			}
			else if (lastRoom.name == "RoomR(Clone)")
			{
				BossR = Instantiate(bossRooms[3], lastRoom.transform.position, lastRoom.transform.rotation);
			}
			BossR.transform.parent = null;
			
			//BossR.transform.FindGameObjectWithTag("BossRoom").SetActive(false);
			rooms.Remove(lastRoom);
			Destroy(lastRoom.gameObject);
			bossRoom = true;
		}
		TreasureRoom();
    }

	void TreasureRoom()
    {
		int children;
		int randTreasure;
        if (bossRoom == true && treasureRoom == false)
        {
			randRoom = Random.Range(1, rooms.Count);
			for (int i = 1; i < rooms.Count; i++)
            {
                if (i == randRoom)
                {
					room = rooms[i];
					children = room.transform.childCount;

					for (int j = 0; j < children; j++)
                    {
                        switch (room.transform.GetChild(j).name)
                        {
							case "TopDoor(Clone)":
								break;
							case "TopDoorFix(Clone)":
								break;
							case "BottomDoor(Clone)":
								break;
							case "BottomDoorFix(Clone)":
								break;
							case "RightDoor(Clone)":
								break;
							case "RightDoorFix(Clone)":
								break;
							case "LeftDoor(Clone)":
								break;
							case "LeftDoorFix(Clone)":
								break;
							case "TopDoor":
								break;
							case "BottomDoor":
								break;
							case "RightDoor":
								break;
							case "LeftDoor":
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
					DisableRooms();
					break;
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
			//EventManager.finishedLoadingLevel += DisableBossRoom;
			disableRooms = true;
            EventManager.levelConstructed();
		}
    }

  //  void DisableBossRoom()
  //  {
		//BossR.transform.GetChild(BossR.transform.childCount - 1).gameObject.SetActive(false);
  //      EventManager.finishedLoadingLevel -= DisableBossRoom;
  //  }

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
