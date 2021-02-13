using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private int towerLimit = 3;
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private Transform towerParentTransform;

    private Queue<Tower> queue = new Queue<Tower>();

    public void AddTower(Waypoint waypoint)
    {
        if (queue.Count < towerLimit)
        {
            InstantiateNewTower(waypoint);
        }
        else
        {
            MoveExistingTower(waypoint);
        }
    }

    private void InstantiateNewTower(Waypoint waypoint)
    {
        var tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);
        tower.transform.parent = towerParentTransform;
        tower.baseWaypoint = waypoint;
        queue.Enqueue(tower);
        waypoint.canHaveTower = false;
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        var oldTower = queue.Dequeue();
        oldTower.baseWaypoint.canHaveTower = true;
        oldTower.baseWaypoint = waypoint;
        oldTower.transform.position = waypoint.transform.position;

        queue.Enqueue(oldTower);
    }
}
