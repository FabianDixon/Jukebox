using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    public EnemyStats Stat;

    [SerializeField]
    private float health;
    private float chargeAmount;

    public GameObject Self;
    public GameObject DeathEffect;

    private GameObject player;

    void Start()
    {
        health = Stat.health;
        chargeAmount = Stat.chargeAmount;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0) { Die(); }
    }

    void Die()
    {
        if (DeathEffect != null)
        {
            GameObject effect = Instantiate(DeathEffect, Self.transform.position, Quaternion.identity);
            Destroy(effect, 0.2f);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        musicalDynamics playerDynamics = player.GetComponent<musicalDynamics>();
        if (playerDynamics != null) { playerDynamics.Charge(chargeAmount); }

        AddEnemy enemy = transform.parent.GetComponent<AddEnemy>();
        if (enemy != null) { enemy.delete(); }
        Destroy(Self);
    }
}
