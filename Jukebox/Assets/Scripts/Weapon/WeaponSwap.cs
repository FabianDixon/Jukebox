using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;

    private RoomTemplates templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        EventManager.bossRoomClearEvent += destroy;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            Shooting shooting = trigger.transform.GetComponent<Shooting>();
            if (shooting != null)
            {
                Vector2 targetCircleCenter = new Vector2(transform.position.x, transform.position.y);
                shooting.currentWeapon.transform.position = targetCircleCenter + Random.insideUnitCircle * Random.Range(2, 4);
                transform.position = new Vector3(1500, 0, 0);
                shooting.SwitchProjectile(this.gameObject, ProjectilePrefab);
            }
            templates.RemoveFromlistTreasure(this.gameObject.name);

            EventManager.roomCompleted();
        }
    }

    void destroy()
    {
        Destroy(this.gameObject);
        EventManager.bossRoomClearEvent -= destroy;
    }
}