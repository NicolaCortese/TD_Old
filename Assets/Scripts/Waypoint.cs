using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    //ok as it's a data class
    public bool isExplored = false; 
    public Waypoint exploredFrom;
    Vector2Int gridPos;
    const int gridSize = 10;

    public bool isPlaceable = true;
    public GameObject towerPrefab;


    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)&&isPlaceable)
        {
            Instantiate(towerPrefab, gameObject.transform.position, Quaternion.identity);
            isPlaceable = false;          
        }
        if (Input.GetMouseButtonDown(0) && !isPlaceable)
        {
            Debug.Log("Error - not placeable over " + gameObject);
        }
    }
}