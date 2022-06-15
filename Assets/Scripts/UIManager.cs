using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject thereIsAnotherHiddenObjectText;
    [SerializeField] private GameObject youFoundIt;
    private void StartLevel()
    {
        nextLevelButton.SetActive(false);
        thereIsAnotherHiddenObjectText.SetActive(false);
        youFoundIt.SetActive(false);
    }

    private void EndLevel()
    {
        nextLevelButton.SetActive(true);
        thereIsAnotherHiddenObjectText.SetActive(true);
    }

    private void PlayerFoundSecondObject()
    {
        youFoundIt.SetActive(true);
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level;
    }

    public void NextLevel() => EventManager.LoadNextLevel?.Invoke();
    private void OnEnable()
    {
        EventManager.StartLevel += StartLevel;
        EventManager.EndLevel += EndLevel;

        EventManager.UpdateLevelText = UpdateLevelText;
        EventManager.PlayerFoundSecondObject += PlayerFoundSecondObject;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= StartLevel;
        EventManager.EndLevel -= EndLevel;
        EventManager.PlayerFoundSecondObject -= PlayerFoundSecondObject;

    }
}