using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<GridObject> gridObjects = new List<GridObject>();
    public int verticalCount, horizontalCount;
    public float gridSize;
    public Sprite mainImage;
    public Sprite secondaryImage;
    public List<GridObject> requiredGridObjects;
    public GameObject levelPrefab;
}