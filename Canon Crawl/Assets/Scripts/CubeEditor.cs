using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]

public class CubeEditor : MonoBehaviour
{

    TextMesh textMesh;
    Waypoint waypoint;
    Pathfinder pathfinder;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        pathfinder = GetComponentInParent<Pathfinder>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
        //pathfinder.PaintEntryExitPoints();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize, 
            0, 
            waypoint.GetGridPos().y * gridSize
        );
    }

    private void UpdateLabel()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        string labeltext = 
            waypoint.GetGridPos().x + 
            "," + 
            waypoint.GetGridPos().y;
        textMesh.text = labeltext;
        gameObject.name = labeltext;
    }
}
