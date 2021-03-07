using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject EnemyDeathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] int health = 10; 
    

    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
    }

    private void AddBoxCollider()
    {
        Collider bc = gameObject.AddComponent<BoxCollider>();
        bc.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {

        health--;
        GameObject hitfx = Instantiate(hitFX, transform.position, Quaternion.identity);

        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(EnemyDeathFX, transform.position, Quaternion.identity);
       
        Destroy(gameObject);
    }
   
}
