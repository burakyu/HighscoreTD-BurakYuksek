using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretProjectile))]
public class TurretProjectileRaycastController : MonoBehaviour
{
    [SerializeField] private LayerMask projectileHitLayers;

    private TurretProjectile _projectile;
    private List<CharacterHealthController> _hittedCharacters = new();
    
    private void Awake()
    {
        _projectile = GetComponent<TurretProjectile>();
    }

    private void OnEnable()
    {
        _hittedCharacters.Clear();
    }

    private void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position, 0.4f, transform.forward, out var hit,0.25f, projectileHitLayers))
        {
            hit.collider.GetComponent<CharacterHealthController>().ReceiveDamage(20);

            // CharacterHealthController characterHealthController = hit.collider.GetComponent<CharacterHealthController>();
            // if (!_hittedCharacters.Contains(characterHealthController))
            // {
            //     _hittedCharacters.Add(characterHealthController);
            // }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = 0.3f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.TransformPoint(Vector3.zero), 0.4f);
    }
}
