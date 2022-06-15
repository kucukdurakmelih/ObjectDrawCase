using System;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private LevelManagerData levelManagerData;
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
        LoadLevel();
    }

    private void LevelPassed()
    {
        levelSc.LevelPassed();
        EventManager.EndLevel?.Invoke();
    }

    private void ShowHiddenObject()
    {
        levelSc.ShowHiddenObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        levelManagerData.IncreaseLevelCounter();
        currentLevelData = levelManagerData.GetLevel();
        LoadLevel();
    }

    private void LoadLevel()
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
        EventManager.ShowHiddenObject = ShowHiddenObject;
        EventManager.LoadNextLevel += LoadNextLevel;
    }

    private void OnDisable()
    {
        EventManager.LevelPassed -= LevelPassed;
        EventManager.LoadNextLevel -= LoadNextLevel;
    }
}