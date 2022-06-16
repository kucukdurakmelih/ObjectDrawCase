using System;
using DG.Tweening;
using UnityEngine;


public class LevelGameObject : MonoBehaviour
{
    [SerializeField] private GameObject mainImage;
    [SerializeField] private GameObject firstRevealImage;
    [SerializeField] private GameObject secondRevealImage;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject hint;

    public void Init()
    {
        mainImage.SetActive(true);
        firstRevealImage.SetActive(false);
        secondRevealImage.SetActive(false);
        hint.SetActive(false);
        EventManager.ShowHint = ShowHint;
    }

    public void LevelPassed()
    {
        var particle = Instantiate(particlePrefab, firstRevealImage.transform.position, Quaternion.identity);
        Destroy(particle, 5);

        firstRevealImage.SetActive(true);
        var scale = firstRevealImage.transform.localScale;

        var biggerScale = scale * 1.3f;
        firstRevealImage.transform.DOScale(biggerScale, 0.3f).OnComplete(() =>
        {
            firstRevealImage.transform.DOScale(scale, 0.2f);
        });
        
        hint.SetActive(false);
    }

    public void ShowHiddenObject()
    {
        var particle = Instantiate(particlePrefab, secondRevealImage.transform.position, Quaternion.identity);
        Destroy(particle, 5);
        secondRevealImage.SetActive(true);

        var scale = secondRevealImage.transform.localScale;
        var biggerScale = scale * 1.3f;
        secondRevealImage.transform.DOScale(biggerScale, 0.3f).OnComplete(() =>
        {
            secondRevealImage.transform.DOScale(scale, 0.2f);
        });
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void ShowHint()
    {
        Debug.Log(firstRevealImage.activeSelf);
        if (firstRevealImage.activeSelf) return;

        Debug.Log(firstRevealImage.activeSelf);

        var canEnableHint = LevelManager.instance.levelManagerData.SpendStar(5);

        Debug.Log(firstRevealImage.activeSelf);

        if (!canEnableHint) return;

        EventManager.ShowHint = null;
        hint.SetActive(true);
        EventManager.UpdateStarText?.Invoke();
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