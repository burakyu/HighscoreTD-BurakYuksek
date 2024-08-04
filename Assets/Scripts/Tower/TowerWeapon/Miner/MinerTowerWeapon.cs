using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MinerTowerWeapon : TowerWeapon
{
    [SerializeField] private Transform shootingPoint;

    private MineExplodeController _currentMineExplodeController;
    private Vector3 _minePlacePos;

    protected override void OnEnable()
    {
        base.OnEnable();
        _towerDraggable.OnTowerPlaced += SetMinePointPosition;
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        _towerDraggable.OnTowerPlaced -= SetMinePointPosition;
    }

    private void SetMinePointPosition(bool active, TowerGrid towerGrid)
    {
        _minePlacePos = new Vector3(0, 0.2f, towerGrid.transform.position.z);
    }
    
    public override void Attack(Vector3 attackPoint)
    {
        if (_currentMineExplodeController != null) return;
        
        transform.DOPunchScale(-transform.localScale * 0.1f, 0.25f, 10, 1);
        
        CreateProjectile(_minePlacePos);
    }

    private void CreateProjectile(Vector3 targetPos)
    {
        var bulletGo = PoolManager.Instance.Spawn(bulletPoolType, shootingPoint.position, Quaternion.identity);
        var projectile = bulletGo.GetComponent<MinerProjectile>();
        _currentMineExplodeController = projectile.GetComponent<MineExplodeController>();
        projectile.SendMine(targetPos);

        _currentMineExplodeController.MineExplode += ClearCurrentMine;
    }

    private void ClearCurrentMine()
    {
        if (_currentMineExplodeController == null) return;

        _currentMineExplodeController.MineExplode -= ClearCurrentMine;
        _currentMineExplodeController = null;
    }
}
