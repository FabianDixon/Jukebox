using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredRoom : MonoBehaviour
{
    public bool isEntered = false;
    private Transform Parent;

    private Transform doorUp;
    private Transform doorDown;
    private Transform doorRight;
    private Transform doorLeft;
    private Transform doorUp2;
    private Transform doorDown2;
    private Transform doorRight2;
    private Transform doorLeft2;

    void Start()
    {
        Parent = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            isEntered = true;
            if (this.transform.parent.gameObject.tag != "initialRoom")
            {
                EventManager.PlayerEnteredRoom();
            }
            int numChild = Parent.childCount;
            doorUp = Parent.Find("TopDoor(Clone)");
            doorDown = Parent.Find("BottomDoor(Clone)");
            doorLeft = Parent.Find("LeftDoor(Clone)");
            doorRight = Parent.Find("RightDoor(Clone)");
            doorUp2 = Parent.Find("TopDoor");
            doorDown2 = Parent.Find("BottomDoor");
            doorLeft2 = Parent.Find("LeftDoor");
            doorRight2 = Parent.Find("RightDoor");

            if (doorUp != null) 
            {
                DoorScript door = doorUp.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorDown != null)
            {
                DoorScript door = doorDown.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorRight != null)
            {
                DoorScript door = doorRight.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorLeft != null)
            {
                DoorScript door = doorLeft.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorUp2 != null)
            {
                DoorScript door = doorUp2.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorDown2 != null)
            {
                DoorScript door = doorDown2.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorRight2 != null)
            {
                DoorScript door = doorRight2.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
            if (doorLeft2 != null)
            {
                DoorScript door = doorLeft2.gameObject.GetComponent<DoorScript>();
                door.triggerCollider.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            isEntered = false;
        }
    }
}
