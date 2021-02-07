using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] private float gridSize = 10f;

    private TextMesh textMesh;
    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {
        var snapPosition = new Vector3
        {
            x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
            y = 0f,
            z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize
        };
        transform.position = snapPosition;
        var positionText = $"{snapPosition.x / gridSize},{snapPosition.z / gridSize}";
        transform.name = positionText;
        textMesh.text = positionText;
    }
}
