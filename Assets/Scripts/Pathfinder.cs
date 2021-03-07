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
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
   

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
            return path;
        
    }

    private void CalculatePath()
    {
        LoadBlocks();
       
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(ending);
        Waypoint previous = ending.exploredFrom;
        while (previous != starting)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(starting);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
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
