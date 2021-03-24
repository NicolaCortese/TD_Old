using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 0.5f;
    [SerializeField] ParticleSystem EnemySelfDestructFX;

    private void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }


    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(enemySpeed);
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        ParticleSystem fx = Instantiate(EnemySelfDestructFX, transform.position, Quaternion.identity);
        Destroy(fx.gameObject, fx.main.duration);
        Destroy(gameObject);
    }
}
