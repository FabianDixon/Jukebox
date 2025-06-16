using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged_dmg : MonoBehaviour
{
    public EnemyStats dmgStat;

    public GameObject hitEffect;
    public int damage;

    public GameObject BulletPrefab;

    public bool shot = false;

    void Start()
    {
        damage = dmgStat.rangedDamage;
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player" && shot == false)
        {
            Shoot();
        }
    }

    void Shoot()  
    {
        shot = true;
        GameObject projectile = Instantiate(BulletPrefab, transform.position, transform.rotation);
        projectile.transform.parent = this.gameObject.transform;
    }
}
