using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TowerWeapon))]
public class TurretWeaponTargetTrigger : MonoBehaviour
{
    private Collider[] _targetsOnRangeColliders = new Collider[10];
    private Tower _tower;
    private TurretTowerWeapon _weapon;
    private GridPlacableTower _towerDraggable;
    private bool _availableForTargetCheck;
    private int _hitCount;

    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _weapon = GetComponent<TurretTowerWeapon>();
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

    private void Update()
    {
        if (!_availableForTargetCheck) return;
        if (CheckForTargets())
        {
            Transform nearestTarget = FindNearest();
            if(nearestTarget!= null) _weapon.FollowTarget(FindNearest());
        }
    }

    public bool CheckForTargets()
    {
        _hitCount = Physics.OverlapSphereNonAlloc(transform.TransformPoint(Vector3.zero), _tower.TowerSettings.AttackRange, _targetsOnRangeColliders,
            _weapon.TargetLayer);
        return _hitCount > 0;
    }

    public Transform FindNearest()
    {
        Transform nearestObjectTransform = null;
        var nearestTargetDistance = _tower.TowerSettings.AttackRange + 1f;
        
        for (int i = 0; i < _hitCount; i++)
        {
            var target = _targetsOnRangeColliders[i];
            var distBetweenTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distBetweenTarget < nearestTargetDistance)
            {
                nearestObjectTransform = target.transform;
                nearestTargetDistance = distBetweenTarget;
            }
        }

        return nearestObjectTransform;
    }
    
    private void ChangeActivation(bool active)
    {
        _availableForTargetCheck = active;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.TransformPoint(Vector3.zero), _tower.TowerSettings.AttackRange);
        }
    }
}