using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovements : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rb;
    Transform player;
    ScenePersist scenePersist;

    [SerializeField] float speed = 2f;
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform wand;
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] TextMeshProUGUI damageUI;

    public bool isDead;
    bool displayShop;
    
    public int health;
    public int maxPlayerHealth;
    public HealthBar healthBar;

    private void Awake()
    {
        scenePersist = FindObjectOfType<ScenePersist>();
        if (scenePersist.playerDamage > 0 && scenePersist.maxHealth > 0)
        {
            UpdateUI();
        }
    }


    void Start()
    {
        deathUI.GetComponent<Canvas>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();

        maxPlayerHealth = scenePersist.maxHealth;
        health = scenePersist.maxHealth;
        healthBar.SetMaxHealth(maxPlayerHealth);
        deathUI.SetActive(false);
        shopUI.SetActive(false);
        displayShop = false;
        isDead = false;
    }

    void Update()
    {
        IsPlayerDead();
        if (!isDead)
        {
            Run();
            FlipSprite();
        }
        UpdateUI();
        DisplayShop();
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
        if(collision.tag == "Enemy" && !isDead)
        {
            health -= 1;
            healthBar.SetHealth(health);
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

    void DisplayShop()
    {
        if (isDead == true && displayShop == true)
        {
            deathUI.SetActive(false);
            shopUI.SetActive(true);
        }
    }

    public void ActivateShop()
    {
        displayShop = true;
    }

    void UpdateUI()
    {
        scoreUI.text = scenePersist.score.ToString();
        goldUI.text = scenePersist.gold.ToString();
        healthUI.text = scenePersist.maxHealth.ToString();
        damageUI.text = scenePersist.playerDamage.ToString();
    }


    public void UpgradeHealth()
    {
        int amountRequired = scenePersist.maxHealth * 10;
        if (scenePersist.maxHealth >= 10) {
            amountRequired = scenePersist.maxHealth * 5;
        }
        if (scenePersist.gold >= amountRequired)
        {
            scenePersist.maxHealth += 1;
            scenePersist.gold -= amountRequired;
        }
    }

    public void UpgradeDamage()
    {
        int amountRequired = scenePersist.playerDamage * 30;
        if(scenePersist.playerDamage >= 10)
        {
            amountRequired = scenePersist.playerDamage * 10;
        }
        if (scenePersist.gold >= amountRequired)
        {
            scenePersist.playerDamage += 1;
            scenePersist.gold -= amountRequired;
        }
    }

    public void RevivePlayer()
    {
        SceneManager.LoadScene("GameScene");
    }
}
