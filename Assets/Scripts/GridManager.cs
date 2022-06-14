using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private List<GridObject> drawnGrids = new List<GridObject>();

    private void CheckGrid(Vector2 pos)
    {
        var levelData = LevelManager.instance.currentLevelData;
        var grids = levelData.gridObjects;

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
        if(drawnGrids.Count < 5) return;
        var requiredGrids = drawnGrids.Where(x => x.isRequired).ToList();
        //var unRequiredGrids = drawnGrids.Where(x => !x.isRequired).ToList();

        var percentage = (float)requiredGrids.Count / drawnGrids.Count;
        Debug.Log(percentage);
        
        drawnGrids.Clear();
        
        if(percentage > 0.5)
            EventManager.LevelPassed?.Invoke();
    }


    private void OnEnable()
    {
        EventManager.CheckDrawing = CheckDrawing;
        EventManager.CheckGrid = CheckGrid;
    }
}