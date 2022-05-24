using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlameProjectile : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D rb;
    float xSpeed;
    // ref to the player
    PlayerMovements player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovements>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0f);
        FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }

    void FlipSprite()
    {
        bool bulletHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (bulletHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
