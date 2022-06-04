using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredRoom : MonoBehaviour
{
    public bool isEntered = false;
    public Transform Parent;

    private Transform doorUp;
    private Transform doorDown;
    private Transform doorRight;
    private Transform doorLeft;

    void Start()
    {
        Parent = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            isEntered = true; 
            int numChild = Parent.childCount;
            doorUp = Parent.GetChild(numChild - 1).Find("DoorUp");
            doorDown = Parent.GetChild(numChild - 1).Find("DoorDown");
            doorLeft = Parent.GetChild(numChild - 1).Find("DoorLeft");
            doorRight = Parent.GetChild(numChild - 1).Find("DoorRight");

            if(doorUp != null) 
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
