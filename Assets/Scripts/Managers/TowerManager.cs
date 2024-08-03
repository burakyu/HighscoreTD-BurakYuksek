using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private DragController dragController;

    private Tower _currentDragTower;

    private void OnEnable()
    {
        EventManager.TowerCardSelected.AddListener(TowerSelected);
        EventManager.TowerDragCanceled.AddListener(DespawnTower);
    }

    private void OnDisable()
    {
        EventManager.TowerCardSelected.RemoveListener(TowerSelected);
        EventManager.TowerDragCanceled.RemoveListener(DespawnTower);
    }

    private void TowerSelected(TowerType towerType)
    {
        if (_currentDragTower != null)
            DespawnTower(_currentDragTower);

        SpawnTower(towerType);
    }

    private void SpawnTower(TowerType towerType)
    {
        GameObject towerGo =
            PoolManager.Instance.Spawn(Enum.Parse<Pools.Types>(Enum.GetName(typeof(TowerType), towerType)));
        DraggableObject towerDraggable = towerGo.GetComponent<DraggableObject>();
        _currentDragTower = towerDraggable.GetComponent<Tower>();
        dragController.SetDraggable(towerDraggable);
        _currentDragTower.SetDespawnPosition(dragController.GetMousePosWorldPoint());
    }

    private void DespawnTower(Tower tower)
    {
        if (!tower.IsPlaced)
            tower.Despawn();
    }
}