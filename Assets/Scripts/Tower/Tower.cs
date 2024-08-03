using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerType towerType;

    private bool _isPlaced;
    private Vector3 _despawnPosition;
    
    public TowerType TowerType => towerType;
    public bool IsPlaced => _isPlaced;
    
    public void SetPlaced()
    {
        _isPlaced = true;
    }

    public void SetDespawnPosition(Vector3 position)
    {
        _despawnPosition = position;
    }

    public void Despawn()
    {
        transform.DOMove(_despawnPosition, 0.4f).OnComplete(() =>
        {
            PoolManager.Instance.Despawn(Enum.Parse<Pools.Types>(Enum.GetName(typeof(TowerType), towerType)),
                gameObject);
        });
    }
}