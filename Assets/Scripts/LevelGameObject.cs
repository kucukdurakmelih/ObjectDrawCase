using System;
using UnityEngine;


public class LevelGameObject : MonoBehaviour
{
    [SerializeField] private GameObject mainImage;
    [SerializeField] private GameObject firstRevealImage;

    public void Init()
    {
        mainImage.SetActive(true);
        firstRevealImage.SetActive(false);
    }

    public void LevelPassed()
    {
        firstRevealImage.SetActive(true);
    }
}