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
        
    private GridPlacableTower _towerDraggable;
    private bool _isWeaponActive;
    private float _shootingTimer;

    protected Tower _tower;
    
    public LayerMask TargetLayer => targetLayer;
    
    public abstract void Attack(Vector3 attackPoint);
    public abstract void StopAttack();

    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerDraggable = GetComponent<GridPlacableTower>();
    }

    private void OnEnable()
    {
        _towerDraggable.OnTowerPlaced += ChangeActivation;
    }

    private void OnDisable()
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
        if (_shootingTimer >= shootInterval)
        {
            _shootingTimer = 0f;
            Attack(Vector3.zero);
        }
    }

    private void ChangeActivation(bool active)
    {
        _isWeaponActive = active;
    }
}
