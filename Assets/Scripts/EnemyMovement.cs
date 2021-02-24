using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("FX prefab on Enemies")] [SerializeField] GameObject EnemyDeathFX;
    [SerializeField] int health = 100;
    [SerializeField] Transform parent;

    private void Start()
    {
        AddBoxCollider();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

   

    IEnumerator FollowPath(List<Waypoint> path)
    {
        
       
        foreach (Waypoint waypoint in path)
        {

            transform.position = waypoint.transform.position;
            

            yield return new WaitForSeconds(1f);
        }
        
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
