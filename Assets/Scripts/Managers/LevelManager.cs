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

    private void Start()
    {
        currentLevelData = levelManagerData.GetLevel();
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
        currentLevelData = levelManagerData.GetLevel();
        LoadLevel();
    }

    public void LoadLevel()
    {
        EventManager.UpdateLevelText?.Invoke(levelManagerData.GetCurrentLevel());
        EventManager.StartLevel?.Invoke();
        var levelGameObject = Instantiate(currentLevelData.levelPrefab, Vector3.zero, quaternion.identity);
        levelSc = levelGameObject.GetComponent<LevelGameObject>();

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