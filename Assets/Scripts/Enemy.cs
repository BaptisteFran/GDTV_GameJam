using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damages;
    PlayerMovements player;


    private void Start()
    {
        player = FindObjectOfType<PlayerMovements>();
    }

    // Update is called once per frame
    void Update()
    {
        IsEnemyDead();
        // Debug.Log("Player's damages : " + player.playerDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "fireBall")
        {
            health -= player.playerDamage;
        }
    }

    void IsEnemyDead()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
