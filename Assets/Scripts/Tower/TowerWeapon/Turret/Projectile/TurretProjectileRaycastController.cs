using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretProjectile))]
public class TurretProjectileRaycastController : MonoBehaviour
{
    [SerializeField] private LayerMask projectileHitLayers;

    private TurretProjectile _projectile;
    
    private void Awake()
    {
        _projectile = GetComponent<TurretProjectile>();
    }
    
    private void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position, 0.4f, transform.forward, out var hit,0.25f, projectileHitLayers))
        {
            hit.collider.GetComponent<EnemyHealthController>().ReceiveDamage(_projectile.TowerSettings.DamageValue);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(Vector3.zero), 0.4f);
    }
}
