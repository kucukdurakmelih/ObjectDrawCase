using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerData", menuName = "LevelManagerData", order = 0)]
public class LevelManagerData : ScriptableObject
{
    [SerializeField] private List<LevelData> allLevels;
    [SerializeField] private int currentLevelIndex;


    public void RestoreLevelIndex()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex");
    }
    
    public LevelData GetLevel()
    {
        var maxLevelCount = allLevels.Count;

        var levelIndex = currentLevelIndex % maxLevelCount;

        var levelToReturn = allLevels[levelIndex];

        return levelToReturn;
    }

    public void IncreaseLevelCounter()
    {
        currentLevelIndex++;
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
    }
}