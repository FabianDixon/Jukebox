using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : MonoBehaviour
{
    public EnemyStats dmgStat;

    public GameObject hitEffect;
    public int damage;
    public int bulletSize;

    public GameObject BulletPrefab;

    public bool shot = false;

    private int rand;

    void Start()
    {
        damage = dmgStat.rangedDamage;
    }

    void Update()
    {
        rand = Random.Range(0, 10);
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player" && shot == false)
        {
            if (rand == 7)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        shot = true;
        GameObject projectile = Instantiate(BulletPrefab, transform.position, transform.rotation);
        projectile.transform.parent = this.gameObject.transform;
    }
}
