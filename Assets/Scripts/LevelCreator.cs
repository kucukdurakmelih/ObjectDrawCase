using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;


public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelData currentLevelData;

    private List<Vector2> grids = new List<Vector2>();
    private Camera mainCamera;
    
    
    
    private void UpdateGrid()
    {
        var gridList = new List<GridObject>();

        var verticalMidPos = 0f;
        if (currentLevelData.verticalCount % 2 == 0)
            verticalMidPos = (float)currentLevelData.verticalCount / 2 - 0.5f;
        else
            verticalMidPos = (float)currentLevelData.verticalCount / 2 - 0.5f;

        var horizontalMidPos = 0f;
        if (currentLevelData.horizontalCount % 2 == 0)
            horizontalMidPos = (float)currentLevelData.horizontalCount / 2 - 0.5f;
        else
            horizontalMidPos = (float)currentLevelData.horizontalCount / 2 - 0.5f;

        for (int i = 0; i < currentLevelData.horizontalCount; i++)
        {
            for (int j = 0; j < currentLevelData.verticalCount; j++)
            {
                var verticalDifference = j - verticalMidPos;
                var verticalPos = verticalDifference * currentLevelData.gridSize;

                var horizontalDifference = i - horizontalMidPos;
                var horizontalPos = horizontalDifference * currentLevelData.gridSize;

                var gridPos = new Vector2(horizontalPos, verticalPos);
                var newGrid = new GridObject
                {
                    isRequired = false,
                    pos = gridPos
                };
                gridList.Add(newGrid);
            }
        }

        currentLevelData.gridObjects = gridList;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateGrid();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(mainCamera == null) mainCamera = Camera.main;
            var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            var closestGrid = FindClosestGrid(mousePos);

            closestGrid.isRequired = !closestGrid.isRequired;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGrid();
        }
    }

    private void SaveGrid()
    {
        currentLevelData.requiredGridObjects.Clear();
        foreach (var gridObject in currentLevelData.gridObjects.Where(gridObject => gridObject.isRequired))
        {
            currentLevelData.requiredGridObjects.Add(gridObject);
        }
    }

    private GridObject FindClosestGrid(Vector2 pos)
    {
        GridObject gridObject = null;
        var minDistance = Mathf.Infinity;

        foreach (var grid in currentLevelData.gridObjects)
        {
            var distance = Vector2.Distance(grid.pos, pos);

            if (minDistance > distance)
            {
                minDistance = distance;
                gridObject = grid;
            }
        }

        return gridObject;
    }

    private void SimulateGrid()
    {
        
        var size = new Vector3(currentLevelData.gridSize - 0.01f, currentLevelData.gridSize - 0.01f,
            currentLevelData.gridSize);
        foreach (var grid in currentLevelData.gridObjects)
        {
            if (grid.isRequired)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(grid.pos, size);
                Gizmos.color = Color.white;
            }

            else
            {
                Gizmos.DrawWireCube(grid.pos, size);
            }
        }
    }

    private void OnDrawGizmos()
    {
        SimulateGrid();
    }
}