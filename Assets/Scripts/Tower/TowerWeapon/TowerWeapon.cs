using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerWeapon : MonoBehaviour
{
    [SerializeField] protected TowerType towerType;
    [SerializeField] protected Pools.Types bulletPoolType;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float shootInterval;
        
    private bool _isWeaponActive;
    private float _shootingTimer;

    protected GridPlacableTower _towerDraggable;
    protected Tower _tower;
    
    public LayerMask TargetLayer => targetLayer;
    
    public abstract void Attack(Vector3 attackPoint);
    
    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerDraggable = GetComponent<GridPlacableTower>();
    }

    protected virtual void OnEnable()
    {
        _towerDraggable.OnTowerPlaced += ChangeActivation;
    }

    protected virtual void OnDisable()
    {
        _towerDraggable.OnTowerPlaced -= ChangeActivation;
    }

    protected virtual void Update()
    {
        if (!_isWeaponActive)
        {
            _shootingTimer = 0f;
            return;
        }

        _shootingTimer += Time.deltaTime;
        if (_shootingTimer >= (shootInterval / (GameBoosterController.Instance.HaveBoost() ? 2 : 1)))
        {
            _shootingTimer = 0f;
            Attack(Vector3.zero);
        }
    }

    private void ChangeActivation(bool active, TowerGrid towerGrid)
    {
        _isWeaponActive = active;
    }
}
