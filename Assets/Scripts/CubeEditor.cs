using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]

public class CubeEditor : MonoBehaviour
{

    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        LabelsGrid();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
       
        transform.position = new Vector3(
            waypoint.GetGridPos().x, 
            0f,
            waypoint.GetGridPos().y) * gridSize;
    }

    private void LabelsGrid()
    {
        int gridsize = waypoint.GetGridSize();
        TextMesh textmesh = GetComponentInChildren<TextMesh>();
        string labeltext = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        textmesh.text = labeltext;
        gameObject.name = labeltext;
        
    }
}
