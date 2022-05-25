using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damages;
    ScenePersist player;


    private void Start()
    {
        player = FindObjectOfType<ScenePersist>();
    }

    // Update is called once per frame
    void Update()
    {
        IsEnemyDead();
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
            player.score += 10;
            player.gold += Random.Range(1, 10);
        }
    }
}
