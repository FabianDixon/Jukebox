using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private int weaponLayer;
    [SerializeField] private int weaponType;

    private GameObject disposedWeapon;

    public bool readyToPickUp = true;
    private Shooting shooting;

    private string[] weapons = {"Mic", "Guitar"};

    private RoomTemplates templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        transform.parent = null;

        EventManager.bossRoomClearEvent += destroy;
        StartCoroutine(waitToPickUp());
    }

    void OnCollisionEnter2D(Collision2D trigger)
    {
        if (trigger.gameObject.tag == "Player" && readyToPickUp == true)
        {
            readyToPickUp = false;
            shooting = trigger.transform.GetComponent<Shooting>();
            if (shooting != null)
            {
                disposedWeapon = shooting.currentWeapon;
                if (disposedWeapon != null)
                {
                    Vector2 targetCircleCenter = new Vector2(transform.position.x, transform.position.y);
                    shooting.currentWeapon.transform.position = targetCircleCenter + Random.insideUnitCircle * Random.Range(2, 20);

                    StartCoroutine(waitToPickUp());
                    transform.position = new Vector3(15000, 0, 0);
                }
                
                shooting.SwitchProjectile(this.gameObject, ProjectilePrefab);
            }
            templates.RemoveFromlistTreasure(this.gameObject.name);

            if (trigger.gameObject.GetComponent<Animator>() != null)
            {
                Animator playerAnimator = trigger.gameObject.GetComponent<Animator>();
                weaponLayer = playerAnimator.GetLayerIndex(weapons[weaponType]);
                for (int i = 0; i < playerAnimator.layerCount; i++)
                {
                    playerAnimator.SetLayerWeight(i, 0);
                }
                playerAnimator.SetLayerWeight(weaponLayer, 1);
            }

            EventManager.roomCompleted();
        }
    }

    IEnumerator waitToPickUp()
    {
        yield return new WaitForSeconds(1f);
        if (disposedWeapon != null)
        {
            disposedWeapon.GetComponent<WeaponSwap>().readyToPickUp = true;
        }
    }

    void destroy()
    {
        if (transform.parent != null)
        {
            if (transform.parent.tag != "Player")
            {
                Destroy(this.gameObject);
                EventManager.bossRoomClearEvent -= destroy;
            }
        }
    }
}