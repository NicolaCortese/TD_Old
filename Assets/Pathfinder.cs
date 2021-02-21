using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    public Waypoint starting, ending;

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        print("Loaded " + grid.Count + " blocks");

        starting.SetTopColor(Color.green);
        ending.SetTopColor(Color.red);

    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block:" + waypoint);
            }

            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);

            }               
            
        }
    }

}
