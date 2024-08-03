using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacableTower : MonoBehaviour
{
    [SerializeField] private LayerMask gridLayerMask;
    [SerializeField] private float offsetY = 2f;
    [SerializeField] private float checkDistanceForGrid = 0.1f;

    private bool _isDragging;
    private Collider _lastGridCollider;
    private Collider _currentGridCollider;
    private Tower _tower;
    
    private void OnEnable()
    {
        _tower = GetComponent<Tower>();
        _isDragging = true;
    }

    private void Update()
    {
        if (!_isDragging) return;

        Vector3 rayPos = transform.position + transform.up * offsetY;
        RaycastHit hit;

        if (Physics.Raycast(rayPos, -transform.up, out hit, checkDistanceForGrid + offsetY, gridLayerMask))
        {
            if (_lastGridCollider != hit.collider)
            {
                OnGridChanged(_lastGridCollider, hit.collider);
                _currentGridCollider = hit.collider;
                _lastGridCollider = _currentGridCollider;
            }
        }
        else
        {
            _currentGridCollider = null;
            if (_lastGridCollider != null)
            {
                TowerGrid previousTowerGrid = _lastGridCollider?.GetComponent<TowerGrid>();
                previousTowerGrid?.SelectPreview(false);
                _lastGridCollider = null;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentGridCollider != null)
                PlaceToGrid();
            else
                EventManager.TowerDragCanceled.Invoke(_tower);
        }
    }

    private void PlaceToGrid()
    {
        if (_currentGridCollider.GetComponent<TowerGrid>().PlaceToGrid(this))
        {
            _isDragging = false;
            _tower.SetPlaced();
        }
        else
        {
            EventManager.TowerDragCanceled.Invoke(_tower);
        }
    }
    
    private void OnGridChanged(Collider previousGrid, Collider newGrid)
    {
        TowerGrid previousTowerGrid = previousGrid?.GetComponent<TowerGrid>();
        TowerGrid newTowerGrid = newGrid.GetComponent<TowerGrid>();

        if (newTowerGrid != null)
        {
            previousTowerGrid?.SelectPreview(false);
            newTowerGrid.SelectPreview(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.up * offsetY,
            -transform.up * (checkDistanceForGrid + offsetY));
    }
}