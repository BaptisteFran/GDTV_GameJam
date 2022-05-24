using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rb;
    Transform player;
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform wand;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
    }

    void Update()
    {
        Run();
        FlipSprite();
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
        if (value.isPressed)
        {
            Instantiate(fireBall, wand.position, transform.rotation);
        }
    }
}
