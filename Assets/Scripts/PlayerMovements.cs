using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rb;
    Transform player;
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform wand;
    public int health;
    public int playerDamage;
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject[] hearts;
    bool isDead;
    //Enemy enemy;

    // INSTANCE
    public static PlayerMovements Instance;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        //enemy = Enemy.Instance;
        deathUI.GetComponent<Canvas>();
        deathUI.SetActive(false);
        health = 3;
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
    }

    void Update()
    {
        IsPlayerDead();
        if (!isDead)
        {
            Run();
            FlipSprite();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x, moveInput.y) * speed;
        rb.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed && !isDead)
        {
            Instantiate(fireBall, wand.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            health -= 1;
            hearts[health].SetActive(false);
        }
    }

    void IsPlayerDead()
    {
        if(health <= 0)
        {
            isDead = true;
            mainUI.SetActive(false);
            deathUI.SetActive(true);
        } else
        {
            isDead = false;
        }
    }
}
