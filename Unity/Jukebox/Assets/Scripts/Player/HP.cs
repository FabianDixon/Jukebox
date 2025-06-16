using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public PlayerStats hp;

    [SerializeField] private int health;
    [SerializeField] private int numOfHearts;
    
    public GameObject Self;
    public GameObject DeathEffect;

    private HeartTemplate template;
    private Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        health = hp.health;
        numOfHearts = health;

        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.1f);
        template = GameObject.FindGameObjectWithTag("Hearts").GetComponent<HeartTemplate>();
        hearts = template.hearts;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }   
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        if (health <= 0) { 
            Die();
            EventManager.OnPlayerDeath();
        }

        StartCoroutine(invincibilityAfterHit());
    }

    IEnumerator invincibilityAfterHit()
    {
        Collider2D hurtBox = this.gameObject.GetComponent<Collider2D>();
        hurtBox.enabled = false;
        SpriteRenderer selfImage = Self.GetComponent<SpriteRenderer>();
        var tempColor = selfImage.color;
        tempColor.a = 0f;
        selfImage.color = tempColor;
        yield return new WaitForSeconds(0.25f);
        tempColor.a = 255f;
        selfImage.color = tempColor;
        yield return new WaitForSeconds(0.25f);
        tempColor.a = 0f;
        selfImage.color = tempColor;
        yield return new WaitForSeconds(0.25f);
        tempColor.a = 255f;
        selfImage.color = tempColor;
        yield return new WaitForSeconds(0.25f);
        hurtBox.enabled = true;
    }

    void Die() 
    {
        if (DeathEffect != null)
        {
            GameObject effect = Instantiate(DeathEffect, Self.transform.position, Quaternion.identity);
            Destroy(effect, 0.2f);
        }
        Destroy(Self);
    }

    public void RecoverHP(int HPtoRecover)
    {
        health += HPtoRecover;
    }

    public void GainHP(int HeartGained)
    {
        if (numOfHearts < hearts.Length)
        {
            numOfHearts += HeartGained;
        }
        health += HeartGained;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
