using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerData", menuName = "LevelManagerData", order = 0)]
public class LevelManagerData : ScriptableObject
{
    [SerializeField] private List<LevelData> allLevels;
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private int startCount;

    public void RestoreLevelIndex()
    {
        var index = PlayerPrefs.GetInt("CurrentLevelIndex");
        if (index == 0)
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", 1);
            index = 1;
        }

        currentLevelIndex = index;

        startCount = PlayerPrefs.GetInt("StarCount");
    }

    public LevelData GetLevel()
    {
        var maxLevelCount = allLevels.Count;

        var levelIndex = currentLevelIndex % maxLevelCount;

        var levelToReturn = levelIndex == 0 ? allLevels[maxLevelCount - 1] : allLevels[levelIndex - 1];

        return levelToReturn;
    }

    public void IncreaseLevelCounter()
    {
        currentLevelIndex++;
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
    }

    public int GetCurrentLevel()
    {
        return currentLevelIndex;
    }

    public int GetStarCount()
    {
        return startCount;
    }

    public bool SpendStar(int count)
    {
        if (startCount < count) return false;
        
        startCount -= count;
        PlayerPrefs.SetInt("StarCount", startCount);
        return true;
    }

    public void IncreaseStarCount()
    {
        startCount++;
        PlayerPrefs.SetInt("StarCount", startCount);
    }
}