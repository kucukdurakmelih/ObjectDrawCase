using System;
using Unity.Mathematics;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelData currentLevelData;
    private LevelGameObject levelSc;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadLevel();
    }
    
    

    private void LoadLevel()
    {
        var levelGameObject = Instantiate(currentLevelData.levelPrefab, Vector3.zero, quaternion.identity);
        levelSc = levelGameObject.GetComponent<LevelGameObject>();
        
        levelSc.Init();

    }


    private void LevelPassed()
    {
        levelSc.LevelPassed();
    }


    private void OnEnable()
    {
        EventManager.LevelPassed = LevelPassed;
    }
}