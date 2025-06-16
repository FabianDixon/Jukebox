using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] private StatModifier speed;

    private float speedUp;
    // Start is called before the first frame update
    void Start()
    {
        speedUp = speed.speed;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            PlayerMovement movement = trigger.GetComponent<PlayerMovement>();
            if (movement != null) { movement.GainMovementSpeed(speedUp); }
            Destroy(this.gameObject);
        }
    }
}