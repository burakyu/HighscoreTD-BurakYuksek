using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private TowerSettings _ownerTowerSettings;
    
    private Vector3 _initialPosition;
    private bool _distancePassed;

    private void OnEnable()
    {
        _distancePassed = false;
        _initialPosition = transform.position;
    }
    
    private void Update()
    {
        if (_distancePassed) return;

        var dist = Vector3.Distance(_initialPosition, transform.position);
        if (dist >= _ownerTowerSettings.AttackRange + 2)
        {
            ResetAndDespawn();
            _distancePassed = true;
        }
    }
    
    public void ResetAndDespawn()
    {
        //ResetProjectile();
        PoolManager.Instance.Despawn(Pools.Types.TurretBullet, this.gameObject);
    }
}