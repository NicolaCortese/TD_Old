using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int TowerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform parent;
    Queue<Tower> towerQueue = new Queue<Tower>();
    

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towerQueue.Count < TowerLimit)
        {
            InstatiateNewTower(baseWaypoint);
        }
        else
            MoveExistingTower(baseWaypoint);
    }
    private void InstatiateNewTower(Waypoint baseWaypoint)
    {
        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = parent;
        towerQueue.Enqueue(newTower);
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;
    }
    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        print("Max towers reached");
        var oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        oldTower.transform.position = newBaseWaypoint.transform.position;
        newBaseWaypoint.isPlaceable = false;
        towerQueue.Enqueue(oldTower);

    }

   
}
