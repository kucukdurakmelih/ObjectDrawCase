using System;
using UnityEngine;


public class LevelGameObject : MonoBehaviour
{
    [SerializeField] private GameObject mainImage;
    [SerializeField] private GameObject firstRevealImage;
    [SerializeField] private GameObject secondRevealImage;
    [SerializeField] private GameObject particlePrefab;

    public void Init()
    {
        mainImage.SetActive(true);
        firstRevealImage.SetActive(false);
        secondRevealImage.SetActive(false);
    }

    public void LevelPassed()
    {
        var particle = Instantiate(particlePrefab, firstRevealImage.transform.position, Quaternion.identity);
        firstRevealImage.SetActive(true);
    }

    public void ShowHiddenObject()
    {
        var particle = Instantiate(particlePrefab, secondRevealImage.transform.position, Quaternion.identity);
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