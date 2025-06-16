using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public ProjectileStats stats;

    public GameObject hitEffect;

    private int damage;
    private float speed;
    private int size;

    private Vector2 target;
    private Boss_Shoot boss_Shoot;
    private Enemy_Ranged_dmg shoot;
    [SerializeField]
    private bool isBoss = false;

    private Transform enemy;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = transform.parent.transform.parent.GetComponent<Transform>();
        if (enemy.gameObject.tag == "Boss")
        {
            isBoss = true;
            boss_Shoot = transform.parent.GetComponent<Boss_Shoot>();
        }
        else
        {
            isBoss = false;
            shoot = transform.parent.GetComponent<Enemy_Ranged_dmg>();
        }
        
        Physics2D.IgnoreCollision(enemy.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        speed = stats.speed;

        if (isBoss == true)
        {
            damage = boss_Shoot.damage;
            size = boss_Shoot.bulletSize;
            transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, transform.localScale.z);
        }
        else
        {
            damage = shoot.damage;
        }
         

        target = new Vector2(player.position.x, player.position.y);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); ;
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.6f);
            }
            Destroy(gameObject);
            if (isBoss == true)
            {
                boss_Shoot.shot = false;
            }
            else
            {
                shoot.shot = false;
            }
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
        if (isBoss == true)
        {
            boss_Shoot.shot = false;
        }
        else
        {
            shoot.shot = false;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Hurt_Box")
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.6f);
            }
            Destroy(gameObject);
            if (isBoss == true)
            {
                boss_Shoot.shot = false;
            }
            else
            {
                shoot.shot = false;
            }

            HP hp = trigger.GetComponent<HP>();
            if (hp != null) { hp.TakeDamage(damage); }
        }
    }
}
