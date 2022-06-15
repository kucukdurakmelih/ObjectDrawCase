using System;
using UnityEngine;


public class LevelGameObject : MonoBehaviour
{
    [SerializeField] private GameObject mainImage;
    [SerializeField] private GameObject firstRevealImage;
    [SerializeField] private GameObject secondRevealImage;

    public void Init()
    {
        mainImage.SetActive(true);
        firstRevealImage.SetActive(false);
        secondRevealImage.SetActive(false);
    }

    public void LevelPassed()
    {
        firstRevealImage.SetActive(true);
    }

    public void ShowHiddenObject()
    {
        secondRevealImage.SetActive(true);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        EventManager.LoadNextLevel += DestroyGameObject;
    }

    private void OnDisable()
    {
        EventManager.LoadNextLevel -= DestroyGameObject;
    }
}