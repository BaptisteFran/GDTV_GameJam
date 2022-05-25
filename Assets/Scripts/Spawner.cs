using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject skeleton;
    [SerializeField] Transform box;
    [SerializeField] Enemy enemy;

    float x;
    float y;
    public float enemyRate;
    bool continueCoroutine;

    // ref to the player
    PlayerMovements player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovements>();
        enemyRate = 1;
        continueCoroutine = true;
        StartCoroutine(EnemySpawner());
        StartCoroutine(IncreaseEnemyRate());
        StartCoroutine(IncreaseEnemyHealth());
    }

    private void Update()
    {
        CheckPlayerHealth();
    }


    IEnumerator EnemySpawner()
    {
        while(continueCoroutine)
        {
            int i = 0;
            while(i < enemyRate)
            {
                x = Random.Range(-9.0f, 10.0f);
                y = Random.Range(-4.0f, 4.0f);
                Instantiate(skeleton, new Vector3(x, y, 0), Quaternion.identity);
                i++;
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator IncreaseEnemyRate()
    {
        while(continueCoroutine)
        {
            enemyRate = enemyRate * 1.2f;
            yield return new WaitForSeconds(10.0f);
        }
    }

    IEnumerator IncreaseEnemyHealth()
    {
        while(continueCoroutine)
        {
            enemy.health += 1;
            enemy.damages += 1;
            yield return new WaitForSeconds(60.0f);
        }
    }

    void CheckPlayerHealth()
    {
        if(player.health <= 0)
        {
            continueCoroutine = false;
            enemy.health = 1;
            enemy.damages = 1;
        }
    }
}
