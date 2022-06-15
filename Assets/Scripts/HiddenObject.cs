using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class HiddenObject 
{
    public int verticalCount, horizontalCount;
    public float gridSize;
    public List<GridObject> gridObjects;
    public List<GridObject> requiredGridObjects;

    public HiddenObject(HiddenObject hiddenObject)
    {
        verticalCount = hiddenObject.verticalCount;
        horizontalCount = hiddenObject.horizontalCount;
        gridObjects = hiddenObject.gridObjects;
        requiredGridObjects = hiddenObject.requiredGridObjects;
    }

    public void Save(HiddenObject hiddenObject)
    {
        verticalCount = hiddenObject.verticalCount;
        horizontalCount = hiddenObject.horizontalCount;
        gridObjects = hiddenObject.gridObjects;
        requiredGridObjects = hiddenObject.requiredGridObjects;
    }
}

