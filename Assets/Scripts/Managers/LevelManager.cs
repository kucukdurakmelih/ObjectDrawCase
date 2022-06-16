using System;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelManagerData levelManagerData;
    public LevelData currentLevelData { get; private set; }
    private LevelGameObject levelSc;

    private void Awake()
    {
        instance = this;
        levelManagerData.RestoreLevelIndex();
    }

    private void LevelPassed()
    {
        levelSc.LevelPassed();
        EventManager.EndLevel?.Invoke();
    }

    public void ShowHiddenObject()
    {
        levelSc.ShowHiddenObject();
    }

    private void LoadNextLevel()
    {
        levelManagerData.IncreaseLevelCounter();
        LoadLevel();
    }

    public void LoadLevel()
    {
        EventManager.UpdateLevelText?.Invoke(levelManagerData.GetCurrentLevel());
        currentLevelData = levelManagerData.GetLevel();

        var levelGameObject = Instantiate(currentLevelData.levelPrefab, Vector3.zero, Quaternion.identity);
        levelSc = levelGameObject.GetComponent<LevelGameObject>();
        
        EventManager.StartLevel?.Invoke();
        levelSc.Init();
    }


    private void OnEnable()
    {
        EventManager.LevelPassed += LevelPassed;
        EventManager.LoadNextLevel += LoadNextLevel;
    }

    private void OnDisable()
    {
        EventManager.LevelPassed -= LevelPassed;
        EventManager.LoadNextLevel -= LoadNextLevel;
    }
}