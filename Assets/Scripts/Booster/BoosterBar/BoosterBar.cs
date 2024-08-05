using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoosterBar : MonoBehaviour
{
    [SerializeField] private Image boosterFill;
    [SerializeField] private List<GameObject> enableOnBoostAvailable;
    [SerializeField] private Button boostButton;

    private Tween _fillTween;

    private void Awake()
    {
        ResetBoostBar();
    }

    private void OnEnable()
    {
        EventManager.AddBoost.AddListener(UpdateBoostBar);
        EventManager.BoostValueCompleted.AddListener(ActivateBoostButton);
        boostButton.onClick.AddListener(UseBoost);
    }

    private void OnDisable()
    {
        EventManager.AddBoost.RemoveListener(UpdateBoostBar);
        EventManager.BoostValueCompleted.RemoveListener(ActivateBoostButton);
        boostButton.onClick.RemoveListener(UseBoost);
    }

    private void UpdateBoostBar(int value = 0)
    {
        _fillTween?.Kill();
        float fillValue = (float)GameBoosterController.Instance.CurrentBoosterValue / 100;
        _fillTween = boosterFill.DOFillAmount(fillValue, 0.2f);
    }
    
    private void ActivateBoostButton()
    {
        foreach (var availableObject in enableOnBoostAvailable)
            availableObject.gameObject.SetActive(true);

        boostButton.enabled = true;
    }

    private void ResetBoostBar()
    {
        UpdateBoostBar();
        
        foreach (var availableObject in enableOnBoostAvailable)
            availableObject.gameObject.SetActive(false);
        
        boostButton.enabled = false;
    }

    private void UseBoost()
    {
        EventManager.BoostUsed.Invoke();
        ResetBoostBar();
    }
}
