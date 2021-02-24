using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Waypoint starting, ending;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;

    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartingAndEnding();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }


    private void CreatePath()
    {
        path.Add(ending);

        Waypoint previous = ending.exploredFrom;
        while (previous!=starting)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }
        path.Add(starting);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(starting);
        while(queue.Count>0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            StartAndEndAreSame();
            ExploreThyNeighbour();

        }
        
    }

    private void StartAndEndAreSame()
    {
        if (searchCenter == ending)
        {
            isRunning = false;
        }
        
    }

    private void ExploreThyNeighbour()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoord = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoord))
            {            
                QueueNewNeighbours(neighbourCoord);
            }
           
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoord)
    {
        Waypoint neighbour = grid[neighbourCoord];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //Do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void ColorStartingAndEnding()
    {
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
