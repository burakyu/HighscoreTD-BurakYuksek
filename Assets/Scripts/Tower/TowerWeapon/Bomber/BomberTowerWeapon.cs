using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomberTowerWeapon : TowerWeapon
{
    [SerializeField] private BomberWeaponTargetTrigger targetTrigger;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform weaponHead;

    private Transform _currentTarget;

    public override void Attack(Vector3 attackPoint)
    {
        _currentTarget = targetTrigger.GetFirstTarget();
        
        if (_currentTarget == null) return;

        SetLookAtBeforeShoot(_currentTarget);
        transform.DOPunchScale(-transform.localScale * 0.1f, 0.25f, 10, 1);
        
        CreateProjectile(_currentTarget.position);
    }

    private void CreateProjectile(Vector3 targetPos)
    {
        var bulletGo = PoolManager.Instance.Spawn(bulletPoolType, shootingPoint.position, Quaternion.LookRotation(weaponHead.forward));
        var projectile = bulletGo.GetComponent<BomberProjectile>();
        projectile.SendBomb(_currentTarget.position);
    }

    private void SetLookAtBeforeShoot(Transform target)
    {
        weaponHead.transform.DOLookAt(target.position + (target.transform.up * 10), 0.5f);
    }
}
