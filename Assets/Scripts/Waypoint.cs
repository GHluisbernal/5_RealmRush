using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int GridSize { get; private set; } = 10;
    public bool IsExplored { get; set; } = false;
    public Waypoint ExploredFrom { get; set; }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GridSize),
            Mathf.RoundToInt(transform.position.z / GridSize)
        );
    }

    internal void SetTopColor(Color color)
    {
        var topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}
