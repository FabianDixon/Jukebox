using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalker_movement : MonoBehaviour
{

    public EnemyStats speedStat;

    private float Initalspeed;
    private float speed = 2f;

    public Animator animator;
    public Rigidbody2D rb;

    private Transform player;
    private SpriteRenderer _renderer;

    private bool m_FacingRight = true;
    private bool isPlayer = false;

    private float relPos;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = rb.GetComponent<SpriteRenderer>();

        Initalspeed = speedStat.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer && player != null)
        {
            movement.x = (player.position.x - rb.position.x);
            movement.y = (player.position.y - rb.position.y);
            movement = movement.normalized;
            relPos = player.position.x - transform.position.x;
            if (m_FacingRight && relPos < 0) { flip(); }
            else if (!m_FacingRight && relPos > 0) { flip(); }
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayer = true;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            relPos = player.position.x - transform.position.x;
            if (relPos > 0) { m_FacingRight = true; }
            else if (relPos < 0) { m_FacingRight = false; }
            speed = Initalspeed;
            animator.SetFloat("Speed", speed);
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isPlayer = false;
    //        speed = 0f;
    //        animator.SetFloat("Speed", speed);
    //    }
    //}

    private void flip()
    {
        m_FacingRight = !m_FacingRight;

        _renderer.flipX = !_renderer.flipX;
    }
}
