using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : BaseSingleton<TowerManager>
{
    [SerializeField] private DragController dragController;
    [SerializeField] private TowersLibrary towersLibrary;

    private List<Tower> _placedTowers = new();
    private Tower _currentDragTower;

    public TowersLibrary TowersLibrary => towersLibrary;

    private void OnEnable()
    {
        EventManager.TowerCardSelected.AddListener(TowerSelected);
        EventManager.TowerDragCanceled.AddListener(DespawnTower);
        EventManager.TowerPlaced.AddListener(TowerPlaced);
    }

    private void OnDisable()
    {
        EventManager.TowerCardSelected.RemoveListener(TowerSelected);
        EventManager.TowerDragCanceled.RemoveListener(DespawnTower);
        EventManager.TowerPlaced.AddListener(TowerPlaced);
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
        {
            _currentDragTower = null;
            tower.Despawn();
        }
    }

    private void TowerPlaced(GridPlacableTower gridPlacableTower, TowerGrid towerGrid)
    {
        _placedTowers.Add(gridPlacableTower.GetComponent<Tower>());
    }
    
    public int GetCurrentTowerPrice(TowerType towerType)
    {
        TowerSettings towerSettings = towersLibrary.GetTowerDataByType(towerType);
        int selectedTypePlacedTowerCount = _placedTowers.FindAll(x=> x.TowerType == towerType).Count;

        return towerSettings.TowerPricesByCount[selectedTypePlacedTowerCount];
    }
}