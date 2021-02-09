using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Vector2Int[] directions =
    {
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.down
    };

    private void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        BreadFirstSearch();
    }

    private void BreadFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0) {
            var currentWaypoint = queue.Dequeue();
            currentWaypoint.IsExplored = true;
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
                        queue.Enqueue(futureWaypoint);
                        futureWaypoint.SetTopColor(Color.blue);
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
