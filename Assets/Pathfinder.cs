using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Waypoint starting, ending;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isRunning = true;

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorStartingAndEnding();
        Pathfind();
    }


    private void Pathfind()
    {
        queue.Enqueue(starting);
        starting.isQueued = true;
        while(queue.Count>0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            print("Searching from " + searchCenter); // remove log later
            StartAndEndAreSame(searchCenter);
            ExploreThyNeighbour(searchCenter);


        }
        print("finished pathfinding?");
    }

    private void StartAndEndAreSame(Waypoint searchCenter)
    {
        if (searchCenter == ending)
        {
            print("Start and End are the same"); // remove log later
            isRunning = false;
        }
        
    }

    private void ExploreThyNeighbour(Waypoint from)
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoord = from.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoord);
            }
            catch
            {
                //Do nothing
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoord)
    {
        Waypoint neighbour = grid[neighbourCoord];
        if (neighbour.isExplored || neighbour.isQueued)
        {
            //Do nothing
        }
        else
        {
            neighbour.SetTopColor(Color.blue); //move later
            queue.Enqueue(neighbour);
            neighbour.isQueued = true;
            print("Queueing " + neighbour);
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
