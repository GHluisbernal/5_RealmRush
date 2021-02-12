using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Waypoint startWaypoint, endWaypoint;

    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> queue = new Queue<Waypoint>();
    private Vector2Int[] directions =
    {
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.down
    };
    private List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            ColorStartAndEnd();
            BreadFirstSearch();
            CreatePath(endWaypoint);
        }
        return path;
    }

    private void CreatePath(Waypoint currentWaypoint)
    {
        if (currentWaypoint != startWaypoint)
        {
            CreatePath(currentWaypoint.ExploredFrom);
        }
        path.Add(currentWaypoint);
    }

    private void BreadFirstSearch()
    {
        startWaypoint.IsExplored = true;
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0) {
            var currentWaypoint = queue.Dequeue();
            if (currentWaypoint == endWaypoint)
            {
                break;
            }

            var currentPosition = currentWaypoint.GetGridPosition();
            foreach (var direction in directions)
            {
                var futurePosition = currentPosition + direction;
                if (grid.ContainsKey(futurePosition))
                {
                    var futureWaypoint = grid[futurePosition];
                    if (!futureWaypoint.IsExplored)
                    {
                        futureWaypoint.IsExplored = true;
                        futureWaypoint.ExploredFrom = currentWaypoint;
                        queue.Enqueue(futureWaypoint);
                    }
                }
            }
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.white);
        endWaypoint.SetTopColor(Color.black);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (var waypoint in waypoints)
        {
            var gridPosition = waypoint.GetGridPosition();
            if (grid.ContainsKey(gridPosition))
            {
                Debug.LogWarning("Skipping overlapping block", waypoint);
            }
            else
            {
                grid.Add(gridPosition, waypoint);
            }
        }
    }
}
