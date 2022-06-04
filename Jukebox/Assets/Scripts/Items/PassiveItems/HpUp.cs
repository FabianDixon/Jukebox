using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : MonoBehaviour
{
    [SerializeField] private StatModifier hp;

    private int Hp_Up;
    // Start is called before the first frame update
    void Start()
    {
        Hp_Up = hp.health;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            HP Health = trigger.transform.Find("Hurt_Box").GetComponent<HP>();
            if (Health != null) { Health.GainHP(Hp_Up); }
            Destroy(this.gameObject);
        }
    }
}
