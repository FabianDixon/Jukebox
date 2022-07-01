using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private Transform player;
    private Vector3 resetPosition = new Vector3(0,0,0);

    [SerializeField] private PlayerStats speedStat;

    private float moveSpeed;
    [SerializeField] private float speed;

    private musicalDynamics dynamics;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    //private float animatorIdle;
    private float idle = 0f;

    void Start()
    {
        dynamics = gameObject.GetComponent<musicalDynamics>();
        moveSpeed = speedStat.speed;
        speed = speedStat.speed;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

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

        animator.SetFloat("idle", idle);

        animator.SetFloat("Speed", movement.sqrMagnitude);

        moveSpeed = speed * dynamics.moveSpeedModifier;
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void GainMovementSpeed(float speedGained)
    {
        speed += speedGained;
    }

    public void LoseMovementSpeed(float speedLost)
    {
        speed -= speedLost;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 1)
        {
            this.gameObject.GetComponent<Transform>().position = resetPosition;
        }
    }
}
