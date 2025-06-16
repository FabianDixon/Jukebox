using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgUp : MonoBehaviour
{
    [SerializeField] private StatModifier dmg;

    private float dmgUp;
    // Start is called before the first frame update
    void Start()
    {
        dmgUp = dmg.damage;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            Shooting shoot = trigger.GetComponent<Shooting>();
            if (shoot != null) { shoot.GainDmg(dmgUp); }
            Destroy(this.gameObject);
        }
    }
}
