using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private Waypoint waypoint;
    private TextMesh textMesh;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        textMesh = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        SnapToPosition();
        UpdateLabelAndName();
    }

    private void SnapToPosition()
    {
        var waypointGridPosition = waypoint.GetGridPosition();
        var snapPosition = new Vector3(
            waypointGridPosition.x * waypoint.GridSize,
            0f,
            waypointGridPosition.y * waypoint.GridSize
        );
        transform.position = snapPosition;
    }

    private void UpdateLabelAndName()
    {
        var waypointGridPosition = waypoint.GetGridPosition();
        var positionText = $"{waypointGridPosition.x},{waypointGridPosition.y}";
        transform.name = positionText;
        textMesh.text = positionText;
    }
}
