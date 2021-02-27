using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("FX prefab on Enemies")] [SerializeField] GameObject EnemyDeathFX;
    [SerializeField] int health = 30; 
    [SerializeField] Transform parent;

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

        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(EnemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
   
}
