using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject player;

    private GameObject currentWeapon;
    private RoomTemplates templates;

    //Start is called before the first frame update
    void Start()
    {
        EventManager.spawnPlayerEvent += Spawn;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    void Spawn()
    {
        GameObject playerCharacter = Instantiate(player, transform.position, Quaternion.identity);

        currentWeapon = playerCharacter.GetComponent<Shooting>().currentWeapon;

        templates.RemoveFromlistTreasure(currentWeapon.name);
    }

    void OnDisable()
    {
        EventManager.spawnPlayerEvent -= Spawn;
    }

    void OnDestroy()
    {
        EventManager.spawnPlayerEvent -= Spawn;
    }
}
