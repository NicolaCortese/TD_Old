﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float secondsBetweenSpawns = 2f;
    public GameObject enemyUnit;
    public int wave = 10;
    
    void Start()
    {
        Invoke("DelayedSpawn", 2f);
    }

    void DelayedSpawn()
    {
        StartCoroutine(SpawnsEnemies());
    }

    IEnumerator SpawnsEnemies()
    {
        while (wave > 0)
        {
            print("spawning");
            
            Instantiate(enemyUnit, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(secondsBetweenSpawns);
            
            wave--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
