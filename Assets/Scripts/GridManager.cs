using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private List<GridObject> drawnGrids = new List<GridObject>();
    private HiddenObject currentHiddenObject;
    private LevelData levelData;


    private void StartLevel()
    {
        levelData = LevelManager.instance.currentLevelData;
        currentHiddenObject = levelData.hiddenObject1;
    }

    private void CheckGrid(Vector2 pos)
    {
        var grids = currentHiddenObject.gridObjects;

        GridObject closestGrid = null;
        var minDistance = Mathf.Infinity;

        foreach (var gridObject in grids)
        {
            var distance = Vector2.Distance(gridObject.pos, pos);

            if (!(distance < minDistance)) continue;
            closestGrid = gridObject;
            minDistance = distance;
        }

        if (!drawnGrids.Contains(closestGrid))
            drawnGrids.Add(closestGrid);
    }


    private void CheckDrawing()
    {
        if (drawnGrids.Count < 5)
        {
            drawnGrids.Clear();
            return;
        }

        var requiredGrids = drawnGrids.Where(x => x.isRequired).ToList();
        //var unRequiredGrids = drawnGrids.Where(x => !x.isRequired).ToList();

        var percentage = (float)requiredGrids.Count / drawnGrids.Count;

        var requiredGridRatio = (float) requiredGrids.Count / currentHiddenObject.requiredGridObjects.Count;


        drawnGrids.Clear();


        if (requiredGridRatio < currentHiddenObject.requiredGridRatio) return;
        if (percentage <= 0.5f) return;

        if (currentHiddenObject == levelData.hiddenObject1)
        {
            EventManager.LevelPassed?.Invoke();
            currentHiddenObject = levelData.hiddenObject2;
        }
        else
            EventManager.ShowHiddenObject?.Invoke();
    }


    private void OnEnable()
    {
        EventManager.CheckDrawing = CheckDrawing;
        EventManager.CheckGrid = CheckGrid;
        EventManager.StartLevel += StartLevel;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= StartLevel;
    }
}