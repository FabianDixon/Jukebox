using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalker_movement : MonoBehaviour
{

    public EnemyStats speedStat;

    private float Initalspeed;
    public float speed;

    public Animator animator;
    public Rigidbody2D rb;

    private Transform player;
    private SpriteRenderer _renderer;

    private bool isPlayer = false;
    private float idle = 0f;

    private float relPos;
    Vector2 movement;

    private bool isPlayerInRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = rb.GetComponent<SpriteRenderer>();

        Initalspeed = speedStat.speed;

        EventManager.enteredRoomEvent += startMoving;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRoom == true)
        {
            if (isPlayer && player != null)
            {
                movement.x = (player.position.x - rb.position.x);
                movement.y = (player.position.y - rb.position.y);
                movement = movement.normalized;
                relPos = player.position.x - transform.position.x;
            }

            animator.SetFloat("Horizontal", movement.x);
            if (movement.x > 0)
            {
                idle = 0.1f;
            }
            else if (movement.x < 0)
            {
                idle = 0.6f;
            }

            animator.SetFloat("Vertical", movement.y);
            if (movement.y > 0)
            {
                idle = 0.85f;
            }
            else if (movement.y < 0)
            {
                idle = 1.1f;
            }
        }

        animator.SetFloat("idle", idle);

        animator.SetFloat("Speed", movement.sqrMagnitude);
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
            speed = Initalspeed;
        }
    }
    void OnEnable()
    {
        EventManager.enteredRoomEvent += startMoving;
    }

    void OnDisable()
    {
        EventManager.enteredRoomEvent -= startMoving;
    }
    private void startMoving()
    {
        isPlayerInRoom = true;
        EventManager.enteredRoomEvent -= startMoving;
    }

}
