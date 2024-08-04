using System;
using UnityEngine;

[RequireComponent(typeof(TowerWeapon))]
public class BomberWeaponTargetTrigger : MonoBehaviour
{
    private Collider[] _targetsOnRangeColliders = new Collider[10];
    private Tower _tower;
    private BomberTowerWeapon _weapon;
    private GridPlacableTower _towerDraggable;
    private bool _availableForTargetCheck;
    private int _hitCount;

    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _weapon = GetComponent<BomberTowerWeapon>();
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

    private bool CheckForTargets()
    {
        Array.Clear(_targetsOnRangeColliders, 0, 10);
        _hitCount = Physics.OverlapSphereNonAlloc(transform.TransformPoint(Vector3.zero), _tower.TowerSettings.AttackRange, _targetsOnRangeColliders,
            _weapon.TargetLayer);
        return _hitCount > 0;
    }

    public Transform GetFirstTarget()
    {
        if (CheckForTargets())
        {
            return _targetsOnRangeColliders[0].transform;
        }

        return null;
    }
    
    private void ChangeActivation(bool active, TowerGrid towerGrid)
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