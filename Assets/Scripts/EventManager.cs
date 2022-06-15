using System;
using UnityEngine;


public class EventManager
{
    public static Action CheckDrawing;
    public static Action<Vector2> CheckGrid;


    public static Action LevelPassed;

    public static Action ShowHiddenObject;

    public static Action StartLevel;
    public static Action EndLevel;

    public static Action<int> UpdateLevelText;

    public static Action LoadNextLevel;
    
    public static Action PlayerFoundSecondObject;

}