using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TurretTowerWeapon : TowerWeapon
{
    [SerializeField] private TurretWeaponTargetTrigger targetTrigger;
    [SerializeField] private Transform shootingPoint;

    private Transform _currentTarget;
    
    public override void Attack(Vector3 attackPoint)
    {
        _currentTarget = targetTrigger.CheckForTargets() ? targetTrigger.FindNearest() : null;
        
        if (_currentTarget == null) return;

        transform.DOPunchScale(-transform.localScale * 0.1f, 0.25f, 10, 1);
        
        CreateProjectile(transform.forward);
    }

    private void CreateProjectile(Vector3 dir)
    {
        Debug.DrawRay(shootingPoint.position, dir * (_tower.TowerSettings.AttackRange + 2), Color.red, 1f);
        var bulletGo = PoolManager.Instance.Spawn(bulletPoolType, shootingPoint.position, Quaternion.LookRotation(dir));
        var projectile = bulletGo.GetComponent<TurretProjectile>();
    }
    
    public override void StopAttack()
    {
        
    }

    public void FollowTarget(Transform target)
    {
        transform.LookAt(target.position + target.forward);
    }
}
