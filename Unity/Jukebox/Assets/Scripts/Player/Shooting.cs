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
    public float power;
    public float distance;
    public float range;
    public float speed;
    public float bulletSize;

    public float fireRate;
    public float fRate;

    private Transform bullet;
    private Animator animator;
    private musicalDynamics dynamics;
    public int direction = 0;
    private float shoot = 0f;

    private bool readyToFire = true;

    private RoomTemplates templates;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        dynamics = gameObject.GetComponent<musicalDynamics>();
        force = Stat.damage * dynamics.dmgModifier;
        power = Stat.damage;
        distance = Stat.projectileDistance * dynamics.rangeModifier;
        range = Stat.projectileDistance;
        speed = Stat.projectileSpeed * dynamics.speedModifier;
        bulletSize = dynamics.sizeModifier;
       
        fireRate = currentWeapon.GetComponent<Stats>()._FireRate + dynamics.fireRateModifier;
        currentWeapon.transform.parent = null;

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.RemoveFromlistTreasure(currentWeapon.name);

        weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();

        weaponUI = GameObject.FindGameObjectWithTag("Weapon_UI").GetComponent<Image>();
        weaponUI.sprite = weaponSprite.sprite;

        fRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (BulletPrefab != null)
        {
            if (Input.GetKey("right") && readyToFire == true)
            {
                direction = 1;
                Shoot(1);
                shoot = 0.1f;
                readyToFire = false;
                StartCoroutine(waitToShoot(fireRate));
            }
            else if (Input.GetKey("down") && readyToFire == true)
            {
                direction = 2;
                Shoot(2);
                shoot = 0.6f;
                readyToFire = false;
                StartCoroutine(waitToShoot(fireRate));
            }
            else if (Input.GetKey("left") && readyToFire == true)
            {
                direction = 3;
                Shoot(3);
                shoot = 0.85f;
                readyToFire = false;
                StartCoroutine(waitToShoot(fireRate));
            }
            else if (Input.GetKey("up") && readyToFire == true)
            {
                direction = 4;
                Shoot(4);
                shoot = 1.1f;
                readyToFire = false;
                StartCoroutine(waitToShoot(fireRate));
            }
        }

        fireRate = fRate + dynamics.fireRateModifier;
        animator.SetFloat("Shoot", shoot);
    }

    IEnumerator waitToShoot(float rateOfFire)
    {
        float secondsToShoot = 0.5f / rateOfFire;
        yield return new WaitForSeconds(secondsToShoot);
        readyToFire = true;
    }

    void Shoot(int FacingDir)
        //1 facing right, 2 facing down, 3 facing left, 4 facing up  
    {
        force = power * dynamics.dmgModifier;
        distance = range * dynamics.rangeModifier;
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
        power += extraDmg;
    }

    public void LoseDmg(float DmgLoss)
    {
        power -= DmgLoss;
    }

    public void GainFireRate(float fireRateGain)
    {
        fRate += fireRateGain;
    }

    public void LooseFireRate(float fireRateLoss)
    {
        fRate -= fireRateLoss;
    }

    public void GainRange(float extraRange)
    {
        range += extraRange;
    }

    public void LooseRange(float RangeLoss)
    {
        range -= RangeLoss;
    }

    public void SwitchProjectile(GameObject NewWeapon, GameObject newProjectile)
    {
        currentWeapon.transform.parent = null;

        BulletPrefab = newProjectile;
        currentWeapon = NewWeapon;
        currentWeapon.transform.parent = transform;

        weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        weaponUI.sprite = weaponSprite.sprite;

        fireRate = currentWeapon.GetComponent<Stats>()._FireRate + dynamics.fireRateModifier;
        fRate = fireRate;
    }
}
