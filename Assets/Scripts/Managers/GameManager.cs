using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private void Start()
    {
        LevelManager.instance.LoadLevel();
    }
}