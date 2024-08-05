using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoosterController : BaseSingleton<GameBoosterController>
{
    private int _currentBoostValue;
    private float _haveBoostDuration;
    
    public int CurrentBoosterValue => _currentBoostValue;
    
    private void OnEnable()
    {
        EventManager.AddBoost.AddListener(AddBoost);
        EventManager.BoostUsed.AddListener(BoostUsed);
    }

    private void OnDisable()
    {
        EventManager.AddBoost.RemoveListener(AddBoost);
        EventManager.BoostUsed.RemoveListener(BoostUsed);
    }

    private void Update()
    {
        if (_haveBoostDuration > 0)
            _haveBoostDuration -= Time.deltaTime;

    }

    private void AddBoost(int boostValue)
    {
        _currentBoostValue += boostValue;
        CheckBoostCompleted();
    }

    private void CheckBoostCompleted()
    {
        if (_currentBoostValue >= 100)
        {
            EventManager.BoostValueCompleted.Invoke();
        }
    }

    private void BoostUsed()
    {
        _haveBoostDuration += 5;
        _currentBoostValue = 0;
    }

    public bool HaveBoost()
    {
        return _haveBoostDuration > 0;
    }
}
