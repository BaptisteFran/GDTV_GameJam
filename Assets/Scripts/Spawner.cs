using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject skeleton;
    [SerializeField] Transform box;

    float x;
    float y;
    public float enemyRate;
    bool continueCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        enemyRate = 1;
        continueCoroutine = true;
        StartCoroutine(EnemySpawner());
        StartCoroutine(IncreaseEnemyRate());
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
            enemyRate += enemyRate + 1.0f;
            yield return new WaitForSeconds(10.0f);
        }
    }
}
