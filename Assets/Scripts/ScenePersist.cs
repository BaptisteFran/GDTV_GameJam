using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public int maxHealth;
    public int score;
    public int playerDamage;
    public int gold;

    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
