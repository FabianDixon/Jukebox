using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public ProjectileStats stats;

    public GameObject hitEffect;

    private float damage;
    private float speed;
    private float distance;
    private float size;

    private Vector2 target;
    private Shooting shooting;

    private Transform player;
    private GameObject enemyBullets;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();

        damage = stats.damage * shooting.force;
        speed = stats.speed * shooting.speed;
        distance = stats.distance * shooting.distance;
        size = shooting.bulletSize;

        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, transform.localScale.z);

        if (shooting.direction == 1)
        {
            target = new Vector2(transform.position.x + distance, transform.position.y);
        }
        else if (shooting.direction == 2)
        {
            target = new Vector2(transform.position.x, transform.position.y - distance);
        }
        else if (shooting.direction == 3)
        {
            target = new Vector2(transform.position.x - distance, transform.position.y);
        }
        else if (shooting.direction == 4)
        {
            target = new Vector2(transform.position.x, transform.position.y + distance);
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if ((transform.position.x == target.x) && (transform.position.y == target.y))
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.6f);
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (hitEffect != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.6f);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Enemy_Hurt_Box")
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.6f);
            }
            Destroy(gameObject);

            Enemy_HP hp = trigger.GetComponent<Enemy_HP>();
            if (hp != null) { hp.TakeDamage(damage); }
        }
    }
}
