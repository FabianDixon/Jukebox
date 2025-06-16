using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    public EnemyStats Stat;

    [SerializeField]
    private float health;
    private float chargeAmount;

    private stalker_movement moveSpeed;

    public GameObject Self;
    public GameObject DeathEffect;

    private GameObject player;

    void Start()
    {
        health = Stat.health;
        chargeAmount = Stat.chargeAmount;
        moveSpeed = Self.GetComponent<stalker_movement>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        StartCoroutine(slowOnHit());

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

    IEnumerator slowOnHit()
    {
        float currentSpeed = moveSpeed.speed;
        moveSpeed.speed -= 10f;
        yield return new WaitForSeconds(1f);
        moveSpeed.speed = currentSpeed;
    }
}
