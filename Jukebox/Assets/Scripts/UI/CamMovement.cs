using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 camChange;
    [SerializeField]
    private Vector3 playerChange;

    [SerializeField]
    private Transform player;

    void Start()
    {
        EventManager.camMoveEvent += OnCrossedDoor;
        EventManager.camMoveEvent += Move;
        EventManager.finishedLoadingLevel += findPlayer;
    }

    void findPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EventManager.finishedLoadingLevel -= findPlayer;
    }

    void OnCrossedDoor(int ID)
    {
        int doorType = ID;
        EventManager.camMoveEvent += Move;
    }

    void Move(int ID)
    {
        switch (ID)
        {
            case 1:
                camChange = new Vector3(0, 360, 0);
                transform.Translate(camChange);
                playerChange = new Vector3(0,100,0);
                break;
            case 4:
                camChange = new Vector3(640, 0, 0);
                transform.Translate(camChange);
                playerChange = new Vector3(120, 0, 0);
                break;
            case 2:
                camChange = new Vector3(0, -360, 0);
                transform.Translate(camChange);
                playerChange = new Vector3(0, -100, 0);
                break;
            case 3:
                camChange = new Vector3(-640, 0, 0);
                transform.Translate(camChange);
                playerChange = new Vector3(-120, 0, 0);
                break;
            default:
                break;
        }
        player.position += playerChange;

        EventManager.camMoveEvent -= Move;
    }

    void OnDestroy()
    {
        EventManager.camMoveEvent -= Move;
        EventManager.camMoveEvent -= OnCrossedDoor;
        EventManager.finishedLoadingLevel -= findPlayer;
    }

    void OnDisable()
    {
        EventManager.camMoveEvent -= Move;
        EventManager.camMoveEvent -= OnCrossedDoor;
        EventManager.finishedLoadingLevel -= findPlayer;
    }
}