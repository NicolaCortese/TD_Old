using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Parameters
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem bulletParticle;
    [SerializeField] AudioClip Shooting;

    //State
    Transform targetEnemy;
    private void Start()
    {
        

    }

    public Waypoint baseWaypoint; //What the tower is standing on

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();

        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
            Shoot(false);
    }

    void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyHealth>();
        if(sceneEnemies.Length == 0) { return; }
        
        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(EnemyHealth testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;

    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distanceClosest = Vector3.Distance(transformA.transform.position,gameObject.transform.position);
        float distanceTestEnemy = Vector3.Distance(transformB.transform.position, gameObject.transform.position);
        if (distanceClosest > distanceTestEnemy)
        {
            return transformB;
        }
        
            return transformA;
    }

    void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            
            Shoot(true);            
        }
        else
            Shoot(false);
    }
    void Shoot(bool isActive)
    {
        //GetComponent<AudioSource>().PlayOneShot(Shooting);
        var emissionModule = bulletParticle.emission;
        emissionModule.enabled = isActive;
        
    }

    
}
