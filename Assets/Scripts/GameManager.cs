using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelData currentLevelData;

    private void Awake()
    {
        instance = this;
    }
    
}