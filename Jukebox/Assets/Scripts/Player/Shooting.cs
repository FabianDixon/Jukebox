using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private PlayerStats Stat;

    public Transform firePoint;
    public GameObject currentWeapon;
    public GameObject BulletPrefab;
    private GameObject projectile;

    private SpriteRenderer weaponSprite;
    private Image weaponUI;

    public float force;
    public float distance;
    public float speed;
    public float bulletSize;

    private Transform bullet;
    private Animator animator;
    private musicalDynamics dynamics;
    public int direction = 0;
    private float shoot = 0f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        dynamics = gameObject.GetComponent<musicalDynamics>();
        force = Stat.damage * dynamics.dmgModifier;
        distance = Stat.projectileDistance * dynamics.rangeModifier;
        speed = Stat.projectileSpeed * dynamics.speedModifier;
        bulletSize = dynamics.sizeModifier;

        currentWeapon.transform.parent = null;

        weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();

        weaponUI = GameObject.FindGameObjectWithTag("Weapon_UI").GetComponent<Image>();
        weaponUI.sprite = weaponSprite.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
            direction = 1;
            Shoot(1);
            shoot = 0.1f;
        }
        else if (Input.GetKeyDown("down"))
        {
            direction = 2;
            Shoot(2);
            shoot = 0.6f;
        }
        else if (Input.GetKeyDown("left"))
        {
            direction = 3;
            Shoot(3);
            shoot = 0.85f;
        }
        else if (Input.GetKeyDown("up"))
        {
            direction = 4;
            Shoot(4);
            shoot = 1.1f;
        }

        animator.SetFloat("Shoot", shoot);
    }

    void Shoot(int FacingDir)
        //1 facing right, 2 facing down, 3 facing left, 4 facing up  
    {
        force = Stat.damage * dynamics.dmgModifier;
        distance = Stat.projectileDistance * dynamics.rangeModifier;
        speed = Stat.projectileSpeed * dynamics.speedModifier;
        bulletSize = dynamics.sizeModifier;      

        animator.SetTrigger("isShot");
        Vector3 firePointPos = firePoint.position;
        if (FacingDir == 3)
        {
            firePointPos.x -= 10f; 
            projectile = Instantiate(BulletPrefab, firePointPos, firePoint.rotation);
        }
        else if (FacingDir == 1)
        {
            firePointPos.x += 10f;
            projectile = Instantiate(BulletPrefab, firePointPos, firePoint.rotation);
        }
        else if (FacingDir == 2)
        {
            firePointPos.y -= 4f;
            projectile = Instantiate(BulletPrefab, firePointPos, firePoint.rotation);
        }
        else if (FacingDir == 4)
        {
            firePointPos.y += 4f; 
            projectile = Instantiate(BulletPrefab, firePointPos, firePoint.rotation);
        }
    }

    public void GainDmg(float extraDmg)
    {
        force += extraDmg;
    }

    public void SwitchProjectile(GameObject NewWeapon, GameObject newProjectile)
    {
        BulletPrefab = newProjectile;
        currentWeapon = NewWeapon;

        weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        weaponUI.sprite = weaponSprite.sprite;
    }
}
