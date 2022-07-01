using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    private RoomTemplates templates;

    private Image itemUI;
    private SpriteRenderer itemSprite;

    private string itemType;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        itemSprite = this.gameObject.GetComponent<SpriteRenderer>();

        if (this.gameObject.tag == "ActiveItem")
        {
            itemUI = GameObject.FindGameObjectWithTag("activeItemUI").GetComponent<Image>();
            itemType = "Active";
        }
        else if (this.gameObject.tag == "Consumable")
        {
            itemUI = GameObject.FindGameObjectWithTag("consumableUI").GetComponent<Image>();
            itemType = "Consumable";
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            Item itemScript = trigger.transform.GetComponent<Item>();
            if (itemScript != null)
            {
                if (itemType == "Active")
                {
                    if (itemScript.isActiveFull == false)
                    {
                        itemScript.isActiveFull = true;
                        itemScript.currentActiveItem = this.gameObject;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.position = new Vector3(15000, 100, 0);
                    }
                    else
                    {
                        Vector2 targetCircleCenter = new Vector2(transform.position.x, transform.position.y);
                        itemScript.currentActiveItem.transform.position = targetCircleCenter + Random.insideUnitCircle * Random.Range(2, 4);
                        itemScript.currentActiveItem.transform.GetChild(0).gameObject.SetActive(false);
                        transform.position = new Vector3(15000, 100, 0);
                        itemScript.currentActiveItem = this.gameObject;
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                else if (itemType == "Consumable")
                {
                    if (itemScript.isConsumableFull == false)
                    {
                        itemScript.isConsumableFull = true;
                        itemScript.currentConsumable = this.gameObject;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.position = new Vector3(15000, 100, 0);
                    }
                    else
                    {
                        Vector2 targetCircleCenter = new Vector2(transform.position.x, transform.position.y);
                        itemScript.currentConsumable.transform.position = targetCircleCenter + Random.insideUnitCircle * Random.Range(2, 4);
                        itemScript.currentConsumable.transform.GetChild(0).gameObject.SetActive(false);
                        transform.position = new Vector3(15000, 100, 0);
                        itemScript.currentConsumable = this.gameObject;
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
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
