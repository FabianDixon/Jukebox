using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    public static Action roomCompletedEvent;
    public static Action spawnPlayerEvent;
    public static Action finishedLoadingLevel;
    public static Action<int> camMoveEvent;
    public static Action bossRoomClearEvent;
    public static Action GameOverEvent;
    public static Action useActiveItemEvent;
    public static Action useConsumableEvent;
    public static Action enteredRoomEvent;

    public static void PlayerEnteredRoom()
    {
        enteredRoomEvent?.Invoke();
    }

    public static void consumableUse()
    {
        useConsumableEvent?.Invoke();
    }

    public static void activeItemUse()
    {
        useActiveItemEvent?.Invoke();
    }

    public static void OnPlayerDeath()
    {
        GameOverEvent?.Invoke();
    }

    public static void bossRoomCompleted()
    {
        bossRoomClearEvent?.Invoke();
    }

    public static void crossedDoor(int doorId)
    {
        camMoveEvent?.Invoke(doorId);
    }

    public static void levelConstructed()
    {
        spawnPlayerEvent?.Invoke();
        finishedLoadingLevel?.Invoke();
    }

    public static void roomCompleted()
    {
        roomCompletedEvent?.Invoke();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
