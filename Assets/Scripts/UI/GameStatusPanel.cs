using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private RectTransform holder;
    [SerializeField] private RectTransform gameOverPopup;
    [SerializeField] private Button restartButton;

    private Tween _popupScaleTween;
    
    private void OnEnable()
    {
        EventManager.MainTowerLosed.AddListener(ShowGameOverPanel);
        restartButton.onClick.AddListener(RestartButtonClicked);
    }

    private void OnDisable()
    {
        EventManager.MainTowerLosed.AddListener(ShowGameOverPanel);
        restartButton.onClick.AddListener(RestartButtonClicked);
    }

    private void ShowGameOverPanel()
    {
        holder.gameObject.SetActive(true);
        gameOverPopup.transform.localScale = Vector3.one * 0.5f;
        _popupScaleTween = gameOverPopup.transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        _popupScaleTween.timeScale = 1;
    }

    private void RestartButtonClicked()
    {
        EventManager.RestartGame.Invoke();
    }
}
