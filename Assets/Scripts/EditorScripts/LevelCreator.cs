using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;

[ExecuteInEditMode]
public class LevelCreator : MonoBehaviour
{
#if UNITY_EDITOR
    public static LevelCreator instance;
    [HideInInspector] public HiddenObject currentHiddenObject;

    private Camera mainCamera;
    private GameObject currentLevelGameObject;
    private LevelData _levelData;
    private HiddenObjectTypes currentHiddenObjectType;

    public void ChangeCurrentHiddenObject(HiddenObject hiddenObject, LevelData levelData,
        HiddenObjectTypes hiddenObjectType)
    {
        currentHiddenObject = hiddenObject;
        currentHiddenObjectType = hiddenObjectType;
        if (currentLevelGameObject != null)
            DestroyImmediate(currentLevelGameObject);

        _levelData = levelData;

        currentLevelGameObject = Instantiate(levelData.levelPrefab, Vector3.zero, quaternion.identity);
    }

    private void Awake()
    {
        instance = this;
        ResetLevelCreator();
    }

    private void ResetLevelCreator()
    {
        currentHiddenObject = null;
        _levelData = null;

        if (currentLevelGameObject != null)
            DestroyImmediate(currentLevelGameObject);
        currentLevelGameObject = null;
    }


    public void ResetGrid()
    {
        var gridList = new List<GridObject>();

        var verticalMidPos = 0f;
        if (currentHiddenObject.verticalCount % 2 == 0)
            verticalMidPos = (float)currentHiddenObject.verticalCount / 2 - 0.5f;
        else
            verticalMidPos = (float)currentHiddenObject.verticalCount / 2 - 0.5f;

        var horizontalMidPos = 0f;
        if (currentHiddenObject.horizontalCount % 2 == 0)
            horizontalMidPos = (float)currentHiddenObject.horizontalCount / 2 - 0.5f;
        else
            horizontalMidPos = (float)currentHiddenObject.horizontalCount / 2 - 0.5f;

        for (int i = 0; i < currentHiddenObject.horizontalCount; i++)
        {
            for (int j = 0; j < currentHiddenObject.verticalCount; j++)
            {
                var verticalDifference = j - verticalMidPos;
                var verticalPos = verticalDifference * currentHiddenObject.gridSize;

                var horizontalDifference = i - horizontalMidPos;
                var horizontalPos = horizontalDifference * currentHiddenObject.gridSize;

                var gridPos = new Vector2(horizontalPos, verticalPos);
                var newGrid = new GridObject
                {
                    isRequired = false,
                    pos = gridPos
                };
                gridList.Add(newGrid);
            }
        }

        currentHiddenObject.gridObjects = gridList;
        SaveGrid();
    }

    private void SaveGrid()
    {
        currentHiddenObject.requiredGridObjects.Clear();
        foreach (var gridObject in currentHiddenObject.gridObjects.Where(gridObject => gridObject.isRequired))
        {
            currentHiddenObject.requiredGridObjects.Add(gridObject);
        }

        _levelData.SaveHiddenObject(currentHiddenObject, currentHiddenObjectType);
        EditorUtility.SetDirty(_levelData);
    }

    private GridObject FindClosestGrid(Vector2 pos)
    {
        GridObject gridObject = null;
        var minDistance = Mathf.Infinity;

        foreach (var grid in currentHiddenObject.gridObjects)
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
        if (currentHiddenObject == null) return;
        if (currentHiddenObject.gridObjects == null) return;
        var size = new Vector3(currentHiddenObject.gridSize - 0.05f, currentHiddenObject.gridSize - 0.05f,
            currentHiddenObject.gridSize);
        foreach (var grid in currentHiddenObject.gridObjects)
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


    private void OnGUI()
    {
        if (Event.current.button != 0) return;
        if (Event.current.type != EventType.MouseDown) return;
        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var origin = ray.origin;
        origin.z = 0;
        var pos = (Vector2)origin;

        var closestGrid = FindClosestGrid(pos);

        closestGrid.isRequired = !closestGrid.isRequired;
        SaveGrid();
    }

#endif
}