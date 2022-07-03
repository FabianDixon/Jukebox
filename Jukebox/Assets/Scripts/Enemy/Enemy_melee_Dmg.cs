using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_melee_Dmg : MonoBehaviour
{
    public EnemyStats dmgStat;

    public GameObject hitEffect;
    private int damage;
    public Rigidbody2D rb;
    public Animator animator;

    private Transform player;
    private Rigidbody2D rb_player;
    Vector2 movement;
    Vector2 movementPlayer;

    void Start()
    {
        damage = dmgStat.meleeDamage;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Hurt_Box")
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.6f);
            }
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }

            HP hp = trigger.GetComponent<HP>();
            if (hp != null) { hp.TakeDamage(damage); }


            player = trigger.GetComponent<Transform>();
            movement.x = (-player.position.x + rb.position.x);
            movement.y = (-player.position.y + rb.position.y);
            movement = movement.normalized;

            rb.AddForce(movement * 20000);

            rb_player = trigger.GetComponentInParent<Rigidbody2D>();

            movementPlayer.x = (player.position.x - rb.position.x);
            movementPlayer.y = (player.position.y - rb.position.y);
            movementPlayer = movementPlayer.normalized;

            rb_player.AddForce(movementPlayer * 40000);
        }
    }
}
