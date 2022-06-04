using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    private RoomTemplates templates;

    private Image itemUI;
    private SpriteRenderer itemSprite;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        itemSprite = this.gameObject.GetComponent<SpriteRenderer>();

        itemUI = GameObject.FindGameObjectWithTag("Item_Ui").GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            Item itemScript = trigger.transform.GetComponent<Item>();
            if (itemScript != null)
            {
                if (itemScript.isFull == false)
                {
                    itemScript.isFull = true;
                    itemScript.currentItem = this.gameObject;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.position = new Vector3(1500, 100, 0);
                }
                else
                {
                    Vector2 targetCircleCenter = new Vector2(transform.position.x, transform.position.y);
                    itemScript.currentItem.transform.position = targetCircleCenter + Random.insideUnitCircle * Random.Range(2, 4);
                    itemScript.currentItem.transform.GetChild(0).gameObject.SetActive(false);
                    transform.position = new Vector3(1500, 100, 0);
                    itemScript.currentItem = this.gameObject;
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                itemUI.sprite = itemSprite.sprite;
                var tempColor = itemUI.color;
                tempColor.a = 255f;
                itemUI.color = tempColor;
            }
            templates.RemoveFromlistTreasure(this.gameObject.name);

            EventManager.roomCompleted();
        }
    }
}
