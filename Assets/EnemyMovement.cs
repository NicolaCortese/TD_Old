﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    

    private void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }



    IEnumerator FollowPath(List<Waypoint> path)
    {
        
        print("Starting patrol..");
        foreach (Waypoint waypoint in path)
        {

            transform.position = waypoint.transform.position;
            print("Visiting block" + waypoint);

            yield return new WaitForSeconds(1f);
        }
        print("Ending patrol..");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
