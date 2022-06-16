using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject thereIsAnotherHiddenObjectText;
    [SerializeField] private GameObject youFoundIt;
    [SerializeField] private GameObject starAnimation;
    [SerializeField] private TextMeshProUGUI starText;

    private bool starCountIncreased;
    private bool playerFoundSecondObject;

    private void StartLevel()
    {
        nextLevelButton.SetActive(false);
        thereIsAnotherHiddenObjectText.SetActive(false);
        youFoundIt.SetActive(false);
        starAnimation.SetActive(false);

        starCountIncreased = false;
        playerFoundSecondObject = false;

        UpdateStarText();

        EventManager.ShowHint += ShowHint;
    }

    private void EndLevel()
    {
        nextLevelButton.SetActive(true);
        thereIsAnotherHiddenObjectText.SetActive(true);
        EventManager.ShowHint = null;
    }

    private void PlayerFoundSecondObject()
    {
        playerFoundSecondObject = true;
        youFoundIt.SetActive(true);
        StartCoroutine(StarAnimation());
    }

    private IEnumerator StarAnimation()
    {
        yield return new WaitForSeconds(1);
        starAnimation.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        if (starCountIncreased) yield break;
        LevelManager.instance.levelManagerData.IncreaseStarCount();
        UpdateStarText();
        starCountIncreased = true;
    }
    

    private void UpdateStarText()
    {
        var starCount = LevelManager.instance.levelManagerData.GetStarCount();
        starText.text = "" + starCount;
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level;
    }

    public void NextLevel()
    {
        if (playerFoundSecondObject && !starCountIncreased)
        {
            StopAllCoroutines();
            starAnimation.SetActive(false);
            LevelManager.instance.levelManagerData.IncreaseStarCount();
            UpdateStarText();
        }
        EventManager.LoadNextLevel?.Invoke();
    }

    public void ShowHint() => EventManager.ShowHint?.Invoke();


    private void OnEnable()
    {
        EventManager.StartLevel += StartLevel;
        EventManager.EndLevel += EndLevel;

        EventManager.UpdateLevelText = UpdateLevelText;
        EventManager.PlayerFoundSecondObject += PlayerFoundSecondObject;

        EventManager.UpdateStarText = UpdateStarText;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= StartLevel;
        EventManager.EndLevel -= EndLevel;
        EventManager.PlayerFoundSecondObject -= PlayerFoundSecondObject;
    }
}