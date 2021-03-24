using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 2f;
    public GameObject enemyUnit;
    public int wave = 10;
    [SerializeField] Transform parent;
    [SerializeField] AudioClip enemySpawning;

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

            GetComponent<AudioSource>().PlayOneShot(enemySpawning);
            var newEnemy = Instantiate(enemyUnit, transform.position, Quaternion.identity);
            newEnemy.transform.parent = parent;
            yield return new WaitForSeconds(secondsBetweenSpawns);
            
            wave--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
