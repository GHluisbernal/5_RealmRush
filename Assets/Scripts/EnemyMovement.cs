using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private void Start()
    {
        var pathFinder = FindObjectOfType<PathFinder>();
        StartCoroutine(FollowPath(pathFinder.GetPath()));
    }

    private IEnumerator FollowPath(IEnumerable<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(2f);
        }
    }
}
